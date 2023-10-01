using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Player Fly Variables
    [SerializeField] private float thurstForce;
    [SerializeField] private float forwardThrust;
    [SerializeField] private float glideEffect;
    [SerializeField] private float standardPlayerGravity;
    private bool jetpackActive;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    [SerializeField] private ParticleSystem myParticleSystem;
    public bool end;
    [SerializeField] float heightLimit;
    [SerializeField] GameObject explosionPrefab;
    private bool exploded = false;
    #endregion

    #region UI Canvas Variables
    [SerializeField] private RawImage image;
    [SerializeField] private float x = 0.01f;
    private float playerAltitude;
    [SerializeField] TMP_Text altitudeText;
    [SerializeField] Slider overheatSlider;
    public float jetpackOverheat;
    [SerializeField] float overheatRate;
    [SerializeField] float cooldownRate;
    #endregion

    #region Audio_Variables
    [SerializeField] AudioSource jetpackAudio;
    [SerializeField] AudioSource explodeAudio;
    [SerializeField] float audioFadeSpeed;
    
    #endregion


    #region Unity Functions
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        // added when commented out glide
        myRigidbody.gravityScale = standardPlayerGravity;
        myAnimator = GetComponent<Animator>();
        jetpackOverheat = 0;
        end = false;
        StartCoroutine(UpdateAltitude());
    }

    private IEnumerator UpdateAltitude()
    {
        while (!end)
        {


            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 5), Vector2.down);
            if (hit.collider != null && hit.collider.transform.tag == "Ground")
            {
                playerAltitude = Mathf.Abs(hit.point.y - transform.position.y);
                altitudeText.text = "Altitude: \n   " + Mathf.Round(playerAltitude);
            }
            yield return new WaitForSeconds(0f);
        }
    }

    void FixedUpdate()
    {
        if (!end)
        {
            JetpackThurst();
            //Glide();
            MoveBackground();
            Overheat();
        }
        else
        {
            Stop();
        }
    }
    #endregion

    #region Player Functions

    private void MoveBackground()
    {
        if (myRigidbody.velocity.x > 0) 
        {
            image.uvRect = new Rect(image.uvRect.position + new Vector2(x, 0) * Time.deltaTime, image.uvRect.size);
        }
    }

    private void TrackAltitude()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2 (transform.position.x, transform.position.y - 5), Vector2.down);
        if (hit.collider != null && hit.collider.transform.tag == "Ground")
        {
            playerAltitude = Mathf.Abs(hit.point.y - transform.position.y);
            altitudeText.text = "Altitude: \n   " + Mathf.Round(playerAltitude);
        }
    }

    private void JetpackThurst()
    {
        //if Space Bar is being pressed then cause jetpack thrust (& set jetpackActive to true)
        if (Input.GetKey(KeyCode.Space))
        {
            if (playerAltitude > heightLimit)
            {
                jetpackOverheat = Mathf.Lerp(jetpackOverheat, 1, 0.05f);
            }

            jetpackAudio.volume = Mathf.Lerp(jetpackAudio.volume, 1.0f, Time.deltaTime * audioFadeSpeed);
            myRigidbody.AddForce(new Vector3 (forwardThrust, thurstForce,0));
            jetpackActive = true;
            myAnimator.SetBool("IsFlying", true);
            myParticleSystem.Play();
            jetpackOverheat += overheatRate;
            if (!jetpackAudio.isPlaying)
            {
                jetpackAudio.Play();
            }
        }
        else
        {
        //if Space Bar is not being pressed then jetpack is not active.
            jetpackActive = false;
            myAnimator.SetBool("IsFlying", false);
            myParticleSystem.Stop();
            jetpackAudio.volume = Mathf.Lerp(jetpackAudio.volume, 0.0f, Time.deltaTime * audioFadeSpeed);
            if (jetpackAudio.volume < 0.1f )
            {
                jetpackAudio.Stop();
            }
            if (jetpackOverheat >= cooldownRate)
            {
                jetpackOverheat -= cooldownRate;
            }
            else
            {
                jetpackOverheat = 0;
            }
        }
    }

    private void Overheat()
    {
        overheatSlider.value = jetpackOverheat;
        if (jetpackOverheat >= 1)
        {
            //Explode
            Debug.Log("BOOM! You exploded!");
            gameObject.SetActive(false);
        }
    }

    private void Stop()
    {
        Debug.Log("Stopping");
        enabled = false;
        myAnimator.SetBool("IsFlying", false);
        myRigidbody.gravityScale = 10f;
        myRigidbody.velocity = Vector3.zero;
    }


    #endregion

    #region Death_Functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Enemy") && !exploded)
        {
            Explode();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Cloud"))
        {
            cooldownRate *= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Cloud"))
        {
            cooldownRate /= 2;
        }
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explodeAudio.Play();
        enabled = false;
        myRigidbody.freezeRotation = !myRigidbody.freezeRotation;
        exploded = true;
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Lose Scene");
    }
    #endregion
}

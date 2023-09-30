using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    #region 

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
    }

    void FixedUpdate()
    {
        if (!end)
        {
            JetpackThurst();
            //Glide();
            TrackAltitude();
            MoveBackground();
            Overheat();
        }
        else
        {
            Stop();
            TrackAltitude();
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
            myRigidbody.AddForce(new Vector3 (forwardThrust, thurstForce,0));
            jetpackActive = true;
            myAnimator.SetBool("IsFlying", true);
            myParticleSystem.Play();
            jetpackOverheat += overheatRate;
        }
        else
        {
        //if Space Bar is not being pressed then jetpack is not active.
            jetpackActive = false;
            myAnimator.SetBool("IsFlying", false);
            myParticleSystem.Stop();
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

    // private void Glide()
    //{
    //    //If Left Shift is pressed AND Jetpack is NOT active, then lower gravity and glide.
    //    if (Input.GetKey(KeyCode.LeftShift) && !jetpackActive)
    //    {
    //        myRigidbody.gravityScale = glideEffect;
     //       myRigidbody.velocity = new Vector2(forwardThrust, 0);
     //       myAnimator.SetBool("IsGliding", true);
      //  }
      //  else
      //  {
        // if Left-Shift is not being pressed, then gravity is normal
        //    myRigidbody.gravityScale = standardPlayerGravity;
          //  myAnimator.SetBool("IsGliding", false) ;
       // }
    //}
    #endregion
}

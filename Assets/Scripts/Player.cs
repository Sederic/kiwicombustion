using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Player Variables
    [SerializeField] private float thurstForce;
    [SerializeField] private float forwardThrust;
    [SerializeField] private float glideEffect;
    [SerializeField] private float standardPlayerGravity;
    private bool jetpackActive;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    [SerializeField] private ParticleSystem myParticleSystem;
    private float playerAltitude;
    [SerializeField] TMP_Text altitudeText;
    #endregion

    #region Other variables
    [SerializeField] private RawImage image;
    [SerializeField] private float x = 0.01f;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JetpackThurst();
        Glide();
        TrackAltitude();
        MoveBackground();
    }

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
            
        }
        else
        {
        //if Space Bar is not being pressed then jetpack is not active.
            jetpackActive = false;
            myAnimator.SetBool("IsFlying", false);
            myParticleSystem.Stop();
        }
    }

    private void Glide()
    {
        //If Left Shift is pressed AND Jetpack is NOT active, then lower gravity and glide.
        if (Input.GetKey(KeyCode.LeftShift) && !jetpackActive)
        {
            myRigidbody.gravityScale = glideEffect;
            myRigidbody.velocity = new Vector2(forwardThrust, 0);
            myAnimator.SetBool("IsGliding", true);
        }
        else
        {
        // if Left-Shift is not being pressed, then gravity is normal
            myRigidbody.gravityScale = standardPlayerGravity;
            myAnimator.SetBool("IsGliding", false) ;
        }
    }
}

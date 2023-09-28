using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float steerSpeed = 150;
    [SerializeField] float moveSpeed;
    [SerializeField] float slowSpeed = 5f;
    [SerializeField] float boostSpeed = 20f;

    private void Start()
    {
        moveSpeed = 15f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Speed Up")
        {
            moveSpeed = boostSpeed;
        }
        if (other.tag == "Slow Down")
        {
            moveSpeed = slowSpeed;
        }
    }

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float driveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, driveAmount, 0);
    }
}

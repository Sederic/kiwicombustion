using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    #region Variables
    [SerializeField] private float velocity = 15;
    private bool moving = false;
    #endregion

    #region Functions
    private void Update()
    {
        if (moving)
        {
            transform.parent.Translate(Vector3.left * velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moving = true;
        }
    }
    #endregion
}

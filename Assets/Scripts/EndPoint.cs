using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit end");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
               player.end = true;
            }
        }
    }
}

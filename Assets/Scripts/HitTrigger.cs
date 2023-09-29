using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    #region Functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is hit");
            // Explode player or something
            Player player = collision.GetComponent<Player>();
            player.gameObject.SetActive(false);
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    #region Cloud_Variables
    [SerializeField] float cloudCooldown;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit cloud");
            Player player = other.GetComponent<Player>();
            if (player != null )
            {
                if (cloudCooldown < player.jetpackOverheat)
                {
                    player.jetpackOverheat -= cloudCooldown;
                }
                else
                {
                    player.jetpackOverheat = 0;
                }
                Debug.Log("Cooldown");
                gameObject.SetActive(false);
            }
        }
    }
}

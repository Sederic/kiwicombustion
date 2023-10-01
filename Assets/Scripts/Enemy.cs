using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public Transform player;
    bool isChasing = false;
    bool chased = false;
    private float newZScale;
    Rigidbody2D enemyRB;
    private Vector3 initialScale;
    [SerializeField] AudioSource enemyAudio;


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        Vector3 newScale = transform.localScale;
        newScale.z = newZScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (isChasing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            enemyRB.velocity = direction * moveSpeed;

            // Flips object if going left or right
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (!isChasing && chased)
        {
            enemyRB.velocity = Vector2.left;
        }
        else 
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            player = other.transform;
            Debug.Log("Player spotted");
            enemyAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            chased = true;
        }
    }


}

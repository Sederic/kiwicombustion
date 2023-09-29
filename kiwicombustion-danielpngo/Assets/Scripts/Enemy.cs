using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public Transform player;
    bool isChasing = false;
    private float newZScale;
    Rigidbody2D enemyRB;
    private Vector3 initialScale;


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

            if (direction.x > 0) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x < 0) 
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    AudioSource winAudio;

    [SerializeField] string levelTag;


    private void Start()
    {
        winAudio = GetComponent<AudioSource>();
    }

    /*private void OnTriggerEnter2D(Collider2D other)
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
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.attachedRigidbody.gravityScale = 0;
            other.attachedRigidbody.velocity = new Vector3(10f, 0, 0);
            other.GetComponentInChildren<ParticleSystem>().Play();
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<Player>().enabled = false;
            winAudio.Play();
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(levelTag);
    }
}

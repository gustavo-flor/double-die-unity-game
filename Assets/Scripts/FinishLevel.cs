using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private bool finished = false;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (finished) {
            return;
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            finished = true;
            audioSource.Play();
            animator.SetTrigger("reached");
            playerRigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void CompleteLevel()
    {
        animator.SetTrigger("flagged");
        playerRigidbody.bodyType = RigidbodyType2D.Static;
        Invoke(nameof(LoadLevel), 2f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float lowestPosition = -25f;
    [SerializeField] private AudioSource dyingAudioSource;

    private Animator animator;
    new private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>(); 
        rigidbody = GetComponent<Rigidbody2D>();     
    }

    private void Update()
    {
        if (ShouldAvoidDeath())
        {
            return;
        }
        if (transform.position.y < lowestPosition) {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (ShouldAvoidDeath())
        {
            return;
        }
        if (collision.gameObject.CompareTag("Trap")) 
        {
            Die();
        }
    }

    private void Die() 
    {
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        dyingAudioSource.Play();
    }

    private void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool IsDead() {
        return rigidbody.bodyType == RigidbodyType2D.Static;
    }

    private bool ShouldAvoidDeath() 
    {
        return IsDead();
    }

}

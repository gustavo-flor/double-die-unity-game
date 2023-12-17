using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private AudioSource collectAudioSource;

    private int score = 0;
    
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (IsCollectable(collider))
        {
            Destroy(collider.gameObject);
            collectAudioSource.Play();
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    private bool IsCollectable(Collider2D collider)
    {
        return collider.gameObject.CompareTag("Cherry") 
            || collider.gameObject.CompareTag("Kiwi");
    }

}

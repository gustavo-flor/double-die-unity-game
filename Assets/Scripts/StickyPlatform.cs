using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsPlayer(collider))
        {
            collider.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (IsPlayer(collider))
        {
            collider.gameObject.transform.SetParent(null);
        }
    }

    private bool IsPlayer(Collider2D collider) 
    {
        return collider.gameObject.CompareTag("Player");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutePickup : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.hasFlute = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit!");
            //Destroy(gameObject);
        }
        
    }
}

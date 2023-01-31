using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            player.hasHorn = true;
        Destroy(gameObject);
    }
}

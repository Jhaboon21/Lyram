using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTeleporter : MonoBehaviour
{
    [SerializeField] private int NextIndex = 2;
    private bool nextToTeleporter = false;
    public OutsideFades outsideFades;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            nextToTeleporter = true;
            outsideFades.NextLevelIndex = NextIndex;
            outsideFades.playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nextToTeleporter = false;
            outsideFades.playerIsClose = false;
        }
    }
}

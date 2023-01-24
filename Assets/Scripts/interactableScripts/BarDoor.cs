using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarDoor : MonoBehaviour
{
    [SerializeField] private int BarIndex = 0;
    [SerializeField] private bool nextToBar = false;
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
            nextToBar = true;
            outsideFades.NextLevelIndex = BarIndex;
            outsideFades.playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nextToBar = false;
            outsideFades.playerIsClose = false;
        }
    }
}

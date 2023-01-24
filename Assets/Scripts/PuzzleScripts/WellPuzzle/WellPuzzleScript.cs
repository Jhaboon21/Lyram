using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellPuzzleScript : MonoBehaviour
{
    public GameObject risingPlatform;
    private Vector2 targetPos;
    public Transform startPos;
    public Transform endPos;
    public float speed;
    [SerializeField] private bool playerTouching = false;

    private void Start()
    {

        targetPos = endPos.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = risingPlatform.transform.position;
        if(!playerTouching)
        {
            targetPos = startPos.transform.position;
        }
        else if(playerTouching)
        {
            targetPos = endPos.transform.position;
        }
        risingPlatform.transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerTouching = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerTouching = false;
    }
}

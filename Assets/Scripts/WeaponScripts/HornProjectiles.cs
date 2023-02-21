using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornProjectiles : MonoBehaviour
{
    //version 1
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pushForce = 3f;
    private Rigidbody2D rb;
    private Rigidbody2D colRB;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       //grabs this component on start
        rb.velocity = transform.right * speed;  //pushes the object in a direction away from player

        Destroy(gameObject, 1f);    //after 1 second, destroy this object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MoveableObj"))
        {
            Debug.Log("hit boulder");
            colRB = collision.gameObject.GetComponent<Rigidbody2D>();
            colRB.AddForce(transform.right * pushForce);
        }


        //destory this object when hitting something
        Destroy(gameObject);

        //must add properties when hitting physics objects/puzzles
    }

    //version 2
    /*Vector3 minSize;
    public Vector3 maxSize;
    [SerializeField] private float scaleRate;
    [SerializeField] private float duration;

    private void Start()
    {
        minSize = transform.localScale;
        StartCoroutine(Scale(minSize, maxSize, duration)); 

        Destroy(gameObject, 2f);
    }

    IEnumerator Scale(Vector3 a, Vector3 b, float time)
    {
        float timer = 0f;
        float rate = (1f / time) * scaleRate;

        while(timer < 1f)
        {
            timer += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, timer);
            yield return null;
        }
    }*/

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornProjectiles : MonoBehaviour
{
  [SerializeField] private float speed = 5f;
  private Rigidbody2D rb;
  void Start()
  {
/*
    rb = GetComponent<Rigidbody2D>();
    if(side.shootside)
    {
      rb.velocity = transform.right * speed;
    }
    else if(side.shootup)
      rb.velocity = transform.up * speed;
*/
    Destroy(gameObject, 1f);
  }
}

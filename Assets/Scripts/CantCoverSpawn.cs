using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CantCoverSpawn : MonoBehaviour
{

    public Rigidbody2D rb2d;
    public float intialMass;
    public float massModifier;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = (transform.localScale.y * massModifier) + intialMass;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Respawn")
            Destroy(gameObject);
    }
}

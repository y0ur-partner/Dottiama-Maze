using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    private int direction; // four directions 0,1,2,3
                           // down, left, up, right
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = 0;
    }
    private Vector2 dirVector;
    private void FixedUpdate()
    {
        if (direction == 0) dirVector = new Vector2(0.0f, -1.0f);
        if (direction == 1) dirVector = new Vector2(-1.0f, 0.0f);
        if (direction == 2) dirVector = new Vector2(0.0f, 1.0f);
        if (direction == 3) dirVector = new Vector2(1.0f, 0.0f);
        rb.velocity = dirVector.normalized * speed;
        // animator.SetFloat("Speed", rb.velocity.magnitude);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newPosition = new Vector2(
        transform.position.x - dirVector.x * 0.1f,
        transform.position.y - dirVector.y * 0.1f);
        rb.MovePosition(newPosition);
        direction = (direction + 1) % 4;
    }
}
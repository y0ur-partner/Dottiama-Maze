using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public int direction; // four directions 0,1,2,3
                          // down, left, up, right
    AudioSource whoosh;
    AudioSource bounce;

    private float deathTimer;
    private int state; // 0 = alive, 1 = dying
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioSource[] audios = GetComponents<AudioSource>();
        whoosh = audios[0];
        bounce = audios[1];
        whoosh.Play();
        deathTimer = 1.0f;
        state = 0;
    }
    private Vector2 dirVector;
    private void FixedUpdate()
    {
        if (state == 1)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (direction == 0) dirVector = new Vector2(0.0f, -1.0f);
        if (direction == 1) dirVector = new Vector2(-1.0f, 0.0f);
        if (direction == 2) dirVector = new Vector2(0.0f, 1.0f);
        if (direction == 3) dirVector = new Vector2(1.0f, 0.0f);
        rb.velocity = dirVector.normalized * speed;
    }
    private void Update()
    {
        if (state == 1)
        {
            deathTimer -= Time.deltaTime;
        }
        if (deathTimer < 0.0f) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == 1) return;
        if (collision.gameObject.tag == "Player"
        || collision.gameObject.tag == "Arrow")
        {
            Physics2D.IgnoreCollision(
            collision.collider,
            gameObject.GetComponent<Collider2D>()
            );
            return;
        }
        bounce.Play();
        if (collision.gameObject.tag == "Spiker")
        {
            Scoring.gamescore += 50;
            Destroy(collision.gameObject);
        }
        state = 1;
    }
}
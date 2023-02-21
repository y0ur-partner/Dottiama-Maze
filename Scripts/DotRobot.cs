using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DotRobot : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    private int direction; // four directions 0,1,2,3
                           // down, left, up, right
    public AudioClip clip;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.Play("DotRobot_Walk_Down");
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
        //animator.SetFloat("Speed", rb.velocity.magnitude);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        Vector2 newPosition = new Vector2(
        transform.position.x - dirVector.x * 0.1f,
        transform.position.y - dirVector.y * 0.1f);

        rb.MovePosition(newPosition);

        direction = (direction + 1) % 4;
        if (direction == 0) animator.Play("DotRobot_Walk_Down");
        if (direction == 1) animator.Play("DotRobot_Walk_Left");
        if (direction == 2) animator.Play("DotRobot_Walk_Up");
        if (direction == 3) animator.Play("DotRobot_Walk_Right");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestionMark : MonoBehaviour
{
    private AudioSource ding;
    private float deathTimer = 1.0f;
    void Start()
    {
        ding = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (deathTimer < 1.0f)
        {
            deathTimer -= Time.deltaTime;
            float shrink = 1.0f - 1.0f * Time.deltaTime;
            transform.localScale = (
            new Vector3(
            transform.localScale.x * shrink,
            transform.localScale.y * shrink,
            transform.localScale.z));
        }
        if (deathTimer < 0.0f) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (deathTimer < 1.0f)
        {
            Physics2D.IgnoreCollision(
            collision.collider,
            gameObject.GetComponent<Collider2D>());
            return;
        }
        if (collision.gameObject.name == "DottimaFace")
        {
            int randomscore = Random.Range(10, 101);
            Scoring.gamescore += randomscore;
            ding.Play();
            deathTimer -= 0.01f;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DottimaController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject shot;
    private int direction;
    private float zrot;
    private Arrow arrow;
    public int dottimaState = 0; // 0 no bomb, 1 with bomb, 2 dying
    private GameObject bomb;
    private float deathTimer = 1.0f;
    public float levelCompleteTimer = 2.0f;
    public const int lastLevel = 6;
    public AudioClip yikes;
    public AudioClip gameOver;
    public AudioClip levelComplete;
    public AudioClip findExit;

    void Start()
    {
        direction = 0;
        rb = GetComponent<Rigidbody2D>();
        AudioSource.PlayClipAtPoint(findExit, Camera.main.transform.position);
    }
    private void FixedUpdate()
    {
        // At the end of a level, stop updating Dottima
        if (GameState.state == GameState.theEnd) return;
        if (GameState.state == GameState.levelComplete) return;
        if (GameState.state == GameState.gameOver) return;

        Vector2 moveInput = new Vector2(
        Input.GetAxisRaw("Horizontal"),
        Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput.normalized * speed;
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }
    private void Update()
    {
        if (GameState.state == GameState.levelComplete)
        {
            rb.velocity = Vector2.zero;
            levelCompleteTimer -= Time.deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.gamescore += (int)Scoring.levelTimer;
                Scoring.levelTimer = 100.0f;
                Scoring.gamescore += 500;
                GameState.level++;
                SceneManager.LoadScene(GameState.level + GameState.SceneOffset);
            }
        }
        if (GameState.state == GameState.theEnd)
        {
            rb.velocity = Vector2.zero;
            levelCompleteTimer -= Time.deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.lives = 5;
                Scoring.gamescore += 1000;
                GameState.level = 1;
                GameState.state = GameState.gamePlay;
                SceneManager.LoadScene(GameState.MenuScene);
            }
        }
        if (GameState.state == GameState.gameOver)
        {
            rb.velocity = Vector2.zero;
            levelCompleteTimer -= Time.deltaTime;
            if (levelCompleteTimer < 0.0f)
            {
                Scoring.lives = 5;
                GameState.level = 1;
                GameState.state = GameState.gamePlay;
                SceneManager.LoadScene(GameState.MenuScene);
            }
        }

        if (GameState.state == GameState.theEnd) return;
        if (GameState.state == GameState.levelComplete) return;
        if (GameState.state == GameState.gameOver) return;

        float x, y;
        x = rb.velocity.x;
        y = rb.velocity.y;
        if (x != 0 || y != 0)
        {
            if (y < x) if (y < -x) direction = 0;
            if (y > x) if (y < -x) direction = 1;
            if (y > x) if (y > -x) direction = 2;
            if (y < x) if (y > -x) direction = 3;
        }

        if (dottimaState == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                if (direction == 0) zrot = -90f;
                if (direction == 1) zrot = 180f;
                if (direction == 2) zrot = 90f;
                if (direction == 3) zrot = 0f;

                GameObject ar = Instantiate(
                shot,
                new Vector3(
                transform.position.x,
                transform.position.y, 1.0f),
                Quaternion.Euler(0, 0, zrot)
                );

                ar.GetComponent<Arrow>().direction = direction;
                if (x != 0 || y != 0) ar.GetComponent<Arrow>().speed += speed;
            }
        }

        if (dottimaState == 1)
        {
            if (Input.GetKeyDown("space"))
            { 
                bomb.GetComponent<Bomb>().bombState = 1;
                bomb.transform.SetParent(null);
                dottimaState = 0;
            }
        }

        if (dottimaState == 2)
        {
            float shrink = 1.0f - 2.0f * Time.deltaTime;
            float rotspeed = -400.0f * Time.deltaTime;
            rb.rotation += rotspeed;
            transform.localScale = (
            new Vector3(
            transform.localScale.x * shrink,
            transform.localScale.y * shrink,
            transform.localScale.z));

            deathTimer -= Time.deltaTime;
            if (deathTimer < 0.0f)
            {
                deathTimer = 1.0f;
                Scoring.lives--;
                dottimaState = 0;
                rb.rotation = 0.0f;
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                gameObject.transform.position = new Vector3(-7.0f, -3.0f, -2.0f);
                if (Scoring.lives == 0)
                {
                    GameState.state = GameState.gameOver;
                    AudioSource.PlayClipAtPoint(gameOver, Camera.main.transform.position);
                }   
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dottimaState == 2) return;

        if (dottimaState == 0)
        {
            if (collision.gameObject.tag == "Bomb")
            {
                bomb = collision.gameObject;
                bomb.transform.SetParent(gameObject.transform);
                bomb.transform.localPosition =
                new Vector3(0.025f, 0.55f, -1.0f);
                Physics2D.IgnoreCollision(
                collision.collider,
                gameObject.GetComponent<Collider2D>()
                );
                dottimaState = 1;
            }
        }

        if (collision.gameObject.tag == "Robot" || collision.gameObject.tag == "Spiker")
        {
            AudioSource.PlayClipAtPoint(yikes, Camera.main.transform.position);
            // drop the bomb first
            if (dottimaState == 1)
            {
                bomb.GetComponent<Bomb>().bombState = 1;
                bomb.transform.SetParent(null);
            }
            dottimaState = 2;
        }
        if (collision.gameObject.name == "ExitLocation")
        {
            AudioSource.PlayClipAtPoint(levelComplete, Camera.main.transform.position);
            if (GameState.level < lastLevel)
            {
                GameState.state = GameState.levelComplete;
            }
            else
            {
                GameState.state = GameState.theEnd;               
            }
        }
    }
}
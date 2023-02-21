using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static int gamescore = 0;
    public static int lives = 5;
    public static float levelTimer = 100.0f;
    // Start is called before the first frame update
    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (GameState.state == GameState.gamePlay)
        {
            levelTimer -= Time.deltaTime;
            if (levelTimer <= 0.0) levelTimer = 0.0f;
        }
    }
    private void OnGUI()
    {
        GUI.skin.box.fontSize = 30;
        GUI.Box(new Rect(20, 20, 400, 50), "Score: " + gamescore + " Timer: " + (int)levelTimer);
        GUI.Box(new Rect(Screen.width - 220, 20, 200, 50), "Level " + GameState.level);
        GUI.Box(new Rect(Screen.width / 2 - 100, 20, 200, 50), "Lives " + lives);
        if (GameState.state == GameState.gameOver)
        {
            GUI.skin.box.fontSize = 60;
            GUI.Box(new Rect(
            Screen.width / 2 - 200,
            Screen.height / 2 - 50,
            400,
            100),
            "Game Over");
        }
        if (GameState.state == GameState.levelComplete)
        {
            GUI.skin.box.fontSize = 60;
            GUI.Box(new Rect(
            Screen.width / 2 - 250,
            Screen.height / 2 - 50, 500, 100), "Level Complete");
        }
        if (GameState.state == GameState.theEnd)
        {
            GUI.skin.box.fontSize = 60;
            GUI.Box(new Rect(
            Screen.width / 2 - 250,
            Screen.height / 2 - 50,
            500,
            100),
            "T H E E N D");
        }
    }
}

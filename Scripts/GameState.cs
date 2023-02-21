using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameState : MonoBehaviour
{
    public static int state;
    public static int level = 1;
    public const int gamePlay = 1;
    public const int gameOver = 2;
    public const int levelComplete = 3;
    public const int theEnd = 4;

    public const int MenuScene = 1;
    public const int FirstLevelScene = 2;
    public const int SceneOffset = FirstLevelScene - 1;

    /*private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        state = gamePlay;
    }

}

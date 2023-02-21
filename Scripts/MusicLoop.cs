using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameState.state == GameState.gameOver)
        {
            Destroy(gameObject);
        }
        if (GameState.state == GameState.theEnd)
        {
            Destroy(gameObject);
        }
    }
}
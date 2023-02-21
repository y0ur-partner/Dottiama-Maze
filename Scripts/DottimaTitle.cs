using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DottimaTitle : MonoBehaviour
{
    float timer = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        float delta;
        delta = Time.deltaTime;
        gameObject.transform.Translate(new Vector3(delta * 4.0f, 0.0f, 0.0f));
        timer -= delta;
        if (timer < 0)
        {
            SceneManager.LoadScene(GameState.MenuScene);
        }
    }
    private void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        GUI.color = Color.yellow;
        GUI.skin.box.fontSize = (int)(Screen.width / 9.0f);
        GUI.Box(new Rect(
        0.0f,
        Screen.height * 0.1f,
        Screen.width,
        Screen.height * 0.3f),
        "DotGame");
    }
}
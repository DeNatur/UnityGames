using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAdventure : MonoBehaviour
{

    public TMP_Text scoreText;
    public int score;
    public int number;
    GameObject player;


    private void Start()
    {
        score = 0;


    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Knife");
        score = (int)player.transform.position.x/10;
        scoreText.text = score.ToString();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}

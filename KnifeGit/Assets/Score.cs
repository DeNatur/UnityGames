using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public TMP_Text scoreText;
    public int score;
    public int number;

    public GameObject[] Objective = new GameObject[10];


    private void Start()
    {
        score = 0;
    }
    // Update is called once per frame
    void Update () {
        scoreText.text = score.ToString();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ScoreUpdate()
    {
        score ++;
        DestroyAllObjects();
        SpawnObjective();

        
    }
  
    void DestroyAllObjects()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Objective");
        foreach (GameObject enemy in enemies)
           Destroy(enemy);
    }

    void SpawnObjective()
    {
        int generate = Random.Range(0, 5);
        number = generate;
        if (score < 20)
        {
            switch (generate)
            {
                case 0:
                    Instantiate(Objective[0], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Objective[1], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Objective[2], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Objective[3], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(Objective[4], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (generate)
            {
                case 0:
                    Instantiate(Objective[3], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(Objective[4], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Objective[5], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Objective[6], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(Objective[7], new Vector3(0, 0, 0), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        
    }

}

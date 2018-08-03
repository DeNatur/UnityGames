using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject score;
    public GameObject highScore;
    public GameObject restartButton;

    public GameObject knife1;
    public GameObject knife2;
    public GameObject knife3;

    public GameObject knife1dark;
    public GameObject knife2dark;
    public GameObject knife3dark;

    public int knivesAmount = 3;


    void Start () {
        score.SetActive(true);
        highScore.SetActive(false);
        restartButton.SetActive(false);
        knife1.SetActive(true);
        knife2.SetActive(true);
        knife3.SetActive(true);
        knife1dark.SetActive(false);
        knife2dark.SetActive(false);
        knife3dark.SetActive(false);
    }



    public bool KnivesLeft()
    {
        switch (knivesAmount)
        {
            case 1:
                knife1.SetActive(false);
                knife1dark.SetActive(true);
                return true;
            case 2:
                knife2.SetActive(false);
                knife2dark.SetActive(true);
                knivesAmount = 1;
                return false;
                
            case 3:
                knife3.SetActive(false);
                knife3dark.SetActive(true);
                knivesAmount = 2;
                return false;
                
            default:
                return false;
        }
    }


    
	
	
}

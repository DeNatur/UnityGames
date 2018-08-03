using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject knife;
    public GameObject score;
    public TMP_Text highScoreText;
    public GameObject highScore;
    public GameObject restartButton;
    public GameObject UIManager;

    public float force = 20;
    public float torque = 20;

    private Vector2 startSwipe;
    private Vector2 endSwipe;
    public Vector3 knifePosition = new Vector3(-5.56f, -1.14f, -3.5f);

    bool oneSwipe = true;
    bool restart = false;

    private void Awake()
    {
        //score = GameObject.Find("ScoreText");
        //highScore = GameObject.Find("HighScoreText");
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (oneSwipe)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Swipe();
            }

        }

    }

    void Swipe()
    {
        rb.isKinematic = false;
        Vector2 swipe = endSwipe - startSwipe;
        rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
        oneSwipe = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Shield")
        {
            rb.isKinematic = true;
            StartCoroutine("Wait");

        }
        else
        {
            Restart();
        }

    }

    void OnCollisionEnter()
    {
        Restart();
    }
    void Restart()
    {
        restart = UIManager.GetComponent<UIManager>().KnivesLeft();
        if (restart)
        {
            int actualScore = score.GetComponent<Score>().score;
            if (actualScore > PlayerPrefs.GetInt("highScore"))
                PlayerPrefs.SetInt("highScore", actualScore);
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highScore").ToString();
            highScore.SetActive(true);
            restartButton.SetActive(true);
            score.SetActive(false);
        }
        else
        {
            Instantiate(knife, knifePosition, Quaternion.Euler(new Vector3(0, 180, 0)));
            Destroy(gameObject);
        }
        //Instantiate(knife, knifePosition, Quaternion.Euler(new Vector3(0, 180, 0)));
        //Destroy(gameObject);

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        score.GetComponent<Score>().ScoreUpdate();
        Instantiate(knife, knifePosition, Quaternion.Euler(new Vector3(0, 180, 0)));
        Destroy(gameObject);
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5f);
        restart = UIManager.GetComponent<UIManager>().KnivesLeft();
        
        
        
    }
}


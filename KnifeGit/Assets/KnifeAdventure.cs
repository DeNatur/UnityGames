using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KnifeAdventure : MonoBehaviour
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
    private float airTime;
    public Vector3 knifePosition = new Vector3(-5.56f, -1.14f, -3.5f);
    public Vector3 newKnifePosition = new Vector3(-5.56f, -1.14f, -3.5f);
    public Quaternion newKnifeRotation;

    Vector3 startFlyPos;

    bool oneSwipe = true;
    bool restart = false;


    private void Awake()
    {
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
                startFlyPos.x = gameObject.transform.position.x;
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
        airTime = Time.time;
        Vector2 swipe = endSwipe - startSwipe;
        if(swipe.x < 0)
        {
            torque = 20;
        }
        else
        {
            torque = -20;
        }
        rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
        oneSwipe = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Shield")
        {
            rb.isKinematic = true;
            newKnifePosition = knife.transform.position;
            newKnifeRotation = knife.transform.rotation;
            oneSwipe = true;
        }
        else
        {
            Restart();
        }


    }

    void OnCollisionEnter()
    {
        float timeInAir = Time.time - airTime;
        if (!rb.isKinematic && timeInAir >= .05f)
        {
            Restart();
        }
    }
    void Restart()
    {
        restart = UIManager.GetComponent<UIManager>().KnivesLeft();
        if (restart)
        {
            int actualScore = score.GetComponent<ScoreAdventure>().score;
            if (actualScore > PlayerPrefs.GetInt("highScoreAdventure"))
                PlayerPrefs.SetInt("highScoreAdventure", actualScore);
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highScore").ToString();
            highScore.SetActive(true);
            restartButton.SetActive(true);
            score.SetActive(false);
        }
        else
        {

            Instantiate(knife, newKnifePosition, newKnifeRotation);
            Destroy(gameObject);
        }
    }


    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5f);
        restart = UIManager.GetComponent<UIManager>().KnivesLeft();
    }
}



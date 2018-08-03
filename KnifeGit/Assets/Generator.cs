using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    private GameObject Platform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth = 40;

    public GameObject[] Tile = new GameObject[10];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.x < generationPoint.position.x)
        {
            PlatformRandomier();
            transform.position = new Vector3(transform.position.x + platformWidth,transform.position.y,transform.position.z);

            Instantiate(Platform, transform.position, transform.rotation);
        }
	}

    void PlatformRandomier()
    {
        int generate = Random.Range(0, 5);
        switch (generate)
        {
            case 0:
                Platform = Tile[0];
                break;
            case 1:
                Platform = Tile[1];
                break;
            case 2:
                Platform = Tile[2];
                break;
            case 3:
                Platform = Tile[3];
                break;
            case 4:
                Platform = Tile[4];
                break;
            default:
                break;
        }
    }
}

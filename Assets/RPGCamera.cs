using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour {

    float playerPosY;

	// Use this for initialization
	void Start () {
        playerPosY = GameObject.Find("Player").transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        playerPosY = GameObject.Find("Player").transform.position.y;
        transform.position = new Vector3(0, playerPosY, -10);
	}
}

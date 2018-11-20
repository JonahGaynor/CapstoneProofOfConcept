using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicatorScript : MonoBehaviour {

    Transform enemyPos;
    Transform playerPos;

	// Use this for initialization
	void Start () {
        enemyPos = GameObject.Find ("enemyPortrait").GetComponent<Transform>();
        playerPos = GameObject.Find("playerPortrait").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn){
            transform.position = playerPos.position;
        } else {
            transform.position = enemyPos.position;
        }
	}
}

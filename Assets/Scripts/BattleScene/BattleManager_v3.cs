using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager_v3 : MonoBehaviour {

    public int currentBidAmount;
    public int currentBidFace;

    public bool myTurn;

    float coroutinePause = 1f;
    

	// Use this for initialization
	void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) {
            myTurn = true;
        } else if (rand == 1) {
            myTurn = false;
            Roll();
            //StartCoroutine(BecomeOpponentTurn());
        }

    }

    public void EnemyTurn() {


    }

    public void Roll() {

    }

    IEnumerator BecomeOpponentTurn() {
        yield return new WaitForSeconds(coroutinePause);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAiEnemy : MonoBehaviour {

    int counter = 0;
    int currentBidNumber;
    int currentBidAmount;

    public GameObject[] myDice;
    public Transform boxPos;


	// Use this for initialization
	void Start () {
        boxPos = GameObject.Find ("teacup").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void Turn (){
        currentBidAmount = GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int face = Random.Range(1, 7);
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, face);
            for (int i = 0; i < currentBidAmount; i++)
            {
                Debug.Log("made another thing");
                Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            }
        } else if (currentBidAmount > 3){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyCall();
        } else if (GameObject.Find ("BattleManager").GetComponent<BattleManager>().counter > 0){
            StartCoroutine(Pause());
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
            currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
            currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
            for (int i = 0; i < currentBidAmount; i++) {
                Debug.Log("made another thing");
                Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            }
        } else {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, currentBidNumber + 1);
            StartCoroutine (PauseToSpawn());
        }

    }

    IEnumerator Pause(){
        yield return new WaitForSeconds (2f);
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn = true;
        yield return new WaitForSeconds (1f);
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().destroyOthers = false;
    }

    IEnumerator PauseToSpawn (){
        Debug.Log ("Hit coroutine");
        yield return new WaitForSeconds (0.4f);
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        for (int i = 0; i < currentBidAmount; i++) {
            Debug.Log("made a thing");
            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
        }
    }
}

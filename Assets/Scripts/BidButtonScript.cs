using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BidButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick () {
        GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount = GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid;
        GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber = GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber;
        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 0;
        GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid = 0;
        GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        StartCoroutine(Pause());
    }

    IEnumerator Pause(){
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn = false;
        yield return new WaitForSeconds(2f);
        //GameObject.Find("BaseEnemy").GetComponent<BaseEnemyScript>().EnemyCall();
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyTurn();
    }
}

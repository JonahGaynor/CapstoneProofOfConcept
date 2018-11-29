using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBidText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "CURRENT BID: " + GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidAmount + " " + GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidNumber + "s.";
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick () {
        if (GameObject.Find("BattleManager").GetComponent<BattleManager>().myTurn)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().PlayerCall();
            GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRulePlayer : MonoBehaviour {

    GameObject myManager;

	// Use this for initialization
	void Start () {
        myManager = GameObject.Find("PuzzleManager");
	}

    void Check(int myNumb)
    {
        myManager.GetComponent<HiddenRuleManager>().Check(myNumb);
    }
}

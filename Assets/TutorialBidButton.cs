using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBidButton : MonoBehaviour {

    GameObject teacup;
    GameObject biddingCounter;
    GameObject tutorialManager;

	// Use this for initialization
	void Start () {
        teacup = GameObject.Find("teacup");
        biddingCounter = GameObject.Find("DiceGens");
        tutorialManager = GameObject.Find("TutorialManager");
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void OnClick()
    {
        if (biddingCounter.GetComponent<DiceGensScript>().amountBid > 2)
        {
            tutorialManager.GetComponent<TutorialManager>().Clear();
            this.gameObject.SetActive(false);
        }
        else if (biddingCounter.GetComponent<DiceGensScript>().amountBid == 2)
        {
            if (teacup.GetComponent<TeaCupScript>().myNumber > 5)
            {
                tutorialManager.GetComponent<TutorialManager>().Clear();
                this.gameObject.SetActive(false);
            }
        }
    }
}

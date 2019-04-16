using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BidButtonScript : MonoBehaviour {

    GameObject eventSystem;

    GameObject[] biddingDice;
    int tempNumb;

    public AudioClip wrong;
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        eventSystem = GameObject.Find("EventSystem");
        myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void OnClick () {
        if (GameObject.Find("BattleManager").GetComponent<BattleManager>().myTurn)
        {
            if (GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber == 0)
            {
                biddingDice = GameObject.FindGameObjectsWithTag("BiddingDice");
                foreach (GameObject d in biddingDice)
                {
                    if (d.name == "1Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 1;
                    }
                    else if (d.name == "2Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 2;
                    }
                    else if (d.name == "3Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 3;
                    }
                    else if (d.name == "4Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 4;
                    }
                    else if (d.name == "5Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 5;
                    }
                    else if (d.name == "6Dice 1(Clone)")
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 6;
                    }
                    else
                    {
                        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 8;
                        myAudio.PlayOneShot(wrong);
                    }
                }
            }
            if (GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount < GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount = GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid;
                GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber = GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber;
                GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 0;
                GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid = 0;
                GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
                GameObject.Find("teacup").GetComponent<TeaCupScript>().haveHit = false;
                GameObject.Find("CurrentBidSlot").GetComponent<CurrentBidSlotScript>().shouldDestroy = true;
                StartCoroutine(Pause());
            }
            else if (GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount == GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid)
            {
                if (GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber < GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount = GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid;
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber = GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber;
                    GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 0;
                    GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid = 0;
                    GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
                    GameObject.Find("teacup").GetComponent<TeaCupScript>().haveHit = false;
                    GameObject.Find("CurrentBidSlot").GetComponent<CurrentBidSlotScript>().shouldDestroy = true;
                    StartCoroutine(Pause());
                }
                else
                {
                    myAudio.PlayOneShot(wrong);
                }
            }
            else
            {
                myAudio.PlayOneShot(wrong);
            }
        }
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    IEnumerator Pause(){
        Debug.Log("THIS SHOULD WORK");
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().myNumber = 0;
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn = false;
        yield return new WaitForSeconds(2f);
        //GameObject.Find("BaseEnemy").GetComponent<BaseEnemyScript>().EnemyCall();
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyTurn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButtonScript : MonoBehaviour {

    GameObject[] activeDice;

    public void OnClick()
    {
        GameObject.Find("DiceGens").GetComponent<DiceGensScript>().amountBid = 0;
        GameObject.Find("teacup").GetComponent<TeaCupScript>().myNumber = 0;
        activeDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach (GameObject dice in activeDice)
        {
            Destroy(dice);
        }
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}

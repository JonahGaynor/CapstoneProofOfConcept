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
        //this.GetComponent<Text>().text = "CURRENT BID: " + GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidAmount + " " + GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidNumber + "s.";
        //for (int i = 0; i < currentBidAmount; i++)
        //{
        //    this.GetComponent<Text>().text += currentBidNumb;
        //}
	}

    public void ChangeText(int amount, int number)
    {
        this.GetComponent<Text>().text = "CURRENT BID: ";
        for (int i = 0; i < amount; i++)
        {
            if (number == 1)
            {
                this.GetComponent<Text>().text += ("< ");
            }
            else if (number == 2)
            {
                this.GetComponent<Text>().text += ("> ");

            }
            else if (number == 3)
            {
                this.GetComponent<Text>().text += ("@ ");
            }
            else if (number == 4)
            {
                this.GetComponent<Text>().text += ("[ ");
            }
            else if (number == 5)
            {
                this.GetComponent<Text>().text += ("] ");
            }
            else if (number == 6)
            {
                this.GetComponent<Text>().text += ("~ ");
            }
        }
    }
}

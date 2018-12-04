using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene4BidButtonScript : MonoBehaviour {

    GameObject[] biddingDice;

    public GameObject fakeDice;
    Vector3 myTransform;

	// Use this for initialization
	void Start () {
        myTransform = new Vector3 (GameObject.Find ("teacup").GetComponent<Transform>().position.x, GameObject.Find ("teacup").GetComponent<Transform>().position.y + 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick (){
        biddingDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach (GameObject b in biddingDice){
            Destroy(b);
        }
        GameObject.Find("1DiceGen").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("6DiceGen").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("3DiceGen").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("4DiceGen").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("5DiceGen").GetComponent<SpriteRenderer>().color = Color.white;

        GameObject.Find("EnterButton").GetComponent<SpriteRenderer>().enabled = true;
        
        GameObject.Find ("Scene4Text").GetComponent<Text>().text = "Excellent, now it’s my turn. I can only bid higher than you, meaning I have to increase the dice face of your bid or increase the amount of your bid. Here is an incorrect bid.";

        GameObject.Find("BiddingFist").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("IncorrectText").GetComponent<Text>().enabled = true;

        GameObject.Find ("Scene4Text").GetComponent<Scene4TextScript>().StartCoroutine("OpenFist");

        Instantiate(fakeDice, myTransform, Quaternion.identity);
        this.gameObject.SetActive(false);
        GameObject.Find("BiddingAreaBoarder").GetComponent<SpriteRenderer>().enabled = false; ;
        GameObject.Find("BidButtonBoarder").GetComponent<SpriteRenderer>().enabled = false;
        //Instantiate(fakeDice, GameObject.Find("teacup").GetComponent<Transform>().position, Quaternion.identity);
    }
}

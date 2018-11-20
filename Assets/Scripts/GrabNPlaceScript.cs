using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNPlaceScript : MonoBehaviour {

	//int myNumber = 1;
	public int currentBidNumber = 0;
	public int currentBidAmount = 0;
	public GameObject myDice;
	public Transform boxPos;
	public int diceCreated = 0;
	bool clicked = false;
	int currentNumb;
	public int myNumber;

    public GameObject[] currentDice;

	// Use this for initialization
	void Start () {
        currentDice = GameObject.FindGameObjectsWithTag("Dice");
		boxPos = GameObject.Find ("teacup").GetComponent<Transform> ();
		if (this.name == "1DiceGen") {
			myNumber = 1;
		} else if (this.name == "2DiceGen") {
			myNumber = 2;
		} else if (this.name == "3DiceGen") {
			myNumber = 3;
		} else if (this.name == "4DiceGen") {
			myNumber = 4;
		} else if (this.name == "5DiceGen") {
			myNumber = 5;
		} else if (this.name == "6DiceGen") {
			myNumber = 6;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("BattleManager") != null) {
			currentBidAmount = GameObject.Find ("BattleManager").GetComponent<BattleManager> ().currentBidAmount;
		}
		currentNumb = GameObject.Find ("teacup").GetComponent<TeaCupScript> ().myNumber;
		if (currentNumb != 0 && this.name != ("" + currentNumb + "DiceGen")) {
//			Debug.Log (this.name);
//			Debug.Log (currentNumb);
			this.GetComponent<BoxCollider2D> ().enabled = false;
		} else {
			this.GetComponent<BoxCollider2D> ().enabled = true;
		}

//		if (clicked) {
//			for (int i = 0; i < 5; i++) {
//				GameObject.FindGameObjectWithTag ("DiceGen").GetComponent<GrabNPlaceScript> ().enabled = false;
//			}
//			clicked = false;
//		}
	}

	void OnMouseOver () {
        foreach (GameObject d in currentDice){
            if (d.GetComponent<BaseDiceScript>().myFace != myNumber){
                d.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
		//if (GameObject.Find ("MyDice0").GetComponent<MyDiceScript> ().myNumber != myNumber) {
		//	GameObject.Find ("MyDice0").GetComponent<SpriteRenderer> ().color = Color.gray;
		//} 
		//if (GameObject.Find ("MyDice1").GetComponent<MyDiceScript> ().myNumber != myNumber) {
		//	GameObject.Find ("MyDice1").GetComponent<SpriteRenderer> ().color = Color.gray;
		//}
		//if (GameObject.Find ("MyDice2").GetComponent<MyDiceScript> ().myNumber != myNumber) {
		//	GameObject.Find ("MyDice2").GetComponent<SpriteRenderer> ().color = Color.gray;
		//}
		//if (GameObject.Find ("MyDice3").GetComponent<MyDiceScript> ().myNumber != myNumber) {
		//	GameObject.Find ("MyDice3").GetComponent<SpriteRenderer> ().color = Color.gray;
		//}
		//if (GameObject.Find ("MyDice4").GetComponent<MyDiceScript> ().myNumber != myNumber) {
		//	GameObject.Find ("MyDice4").GetComponent<SpriteRenderer> ().color = Color.gray;
		//}


	}

	void OnMouseExit (){
        foreach (GameObject d in currentDice){
            d.GetComponent<SpriteRenderer>().color = Color.white;
        }
		//GameObject.Find ("MyDice0").GetComponent<SpriteRenderer> ().color = Color.white;
		//GameObject.Find ("MyDice1").GetComponent<SpriteRenderer> ().color = Color.white;
		//GameObject.Find ("MyDice2").GetComponent<SpriteRenderer> ().color = Color.white;
		//GameObject.Find ("MyDice3").GetComponent<SpriteRenderer> ().color = Color.white;
		//GameObject.Find ("MyDice4").GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void OnMouseDown () {
		clicked = true;
		Debug.Log ("click");
//		if (currentBidNumber > GameObject.Find ("BattleManager").GetComponent<BattleManagerScript> ().currentBidNumber) {
//			for (int i = 0; i < currentBidAmount + 1; i++) {
				Debug.Log ("instantiate please");
				//Instantiate (myDice, boxPos);
				Instantiate (myDice, boxPos.position, Quaternion.identity);
				GameObject.Find ("DiceGens").GetComponent<DiceGensScript> ().amountBid++;
//			}
//		} else {
//			for (int i = 0; i < currentBidAmount + 1; i++) {
//				Debug.Log ("instantiate please");
//				//Instantiate (myDice, boxPos);
//				Instantiate (myDice, boxPos.position, Quaternion.identity);
//				GameObject.Find ("DiceGens").GetComponent<DiceGensScript> ().amountBid++;
//			}
//		}
//		currentBidAmount++;
	}
}

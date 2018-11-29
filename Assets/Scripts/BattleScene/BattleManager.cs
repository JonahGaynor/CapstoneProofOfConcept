using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public int myDice;
    public int enemyDice;
    public List<int> myCurrentDice;
    public List<int> enemyCurrentDice;
    public int currentBidNumber;
    public int currentBidAmount;
    int actualAmount;
    public bool myTurn = true;
    public int counter = 0;
    public bool enemyToCall = false;

    public GameObject[] battleDice;
    public GameObject[] enemyBattleDice;
    public GameObject[] currentBidDice;

    public Transform boxPos;

	// Use this for initialization
	void Start () {
        myDice = 5;
        enemyDice = 5;
        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        boxPos = GameObject.Find ("teacup").GetComponent<Transform>();
        Roll ();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void EnemyTurn (){
        for (int i = 0; i < enemyCurrentDice.Count; i++){
            if (enemyCurrentDice[i] == currentBidNumber){
                counter++;
            }
        }
        if (counter > currentBidAmount + 1){
            EnemyCall();
        } else {
            GameObject.Find ("Enemy").GetComponent<BaseAiEnemy>().Turn();
        }
    }

    void Roll () {
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().myNumber = 0;
        myCurrentDice.Clear();
        enemyCurrentDice.Clear();
        actualAmount = 0;
        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        currentBidDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach (GameObject d in battleDice){
            d.GetComponent<BaseDiceScript>().Roll();
        }
        foreach (GameObject e in enemyBattleDice){
            e.GetComponent<EnemyDiceScript>().Roll();
        }
        foreach (GameObject b in currentBidDice){
            Destroy(b);
        }
        GameObject.Find ("CurrentBidSlot").GetComponent<CurrentBidSlotScript>().shouldDestroy = true;
        for (int i = 0; i < myDice; i++){
            int temp = Random.Range(0, 6);
          //  myCurrentDice.Add(temp);
        }
        for (int j = 0; j < enemyDice; j++){
            int temp = Random.Range(0, 6);
            //enemyCurrentDice.Add(temp);
        }
    }

    public void PlayerCall () {
        foreach (GameObject e in enemyBattleDice){
            e.GetComponent<EnemyDiceScript>().shouldShow = true;
        }

        for (int i = 0; i < myCurrentDice.Count; i++){
            if (myCurrentDice[i] == currentBidNumber){
                actualAmount++;
            }
        }
        for (int j = 0; j < enemyCurrentDice.Count; j++){
            if (enemyCurrentDice[j] == currentBidNumber){
                actualAmount++;
            }
        }
        if (actualAmount >= currentBidAmount){
            PlayerLoses();
        } else {
            EnemyLoses();
        }
    }

    public void EnemyCall () {
        foreach (GameObject e in enemyBattleDice) {
            e.GetComponent<EnemyDiceScript>().shouldShow = true;
        }

        for (int i = 0; i < myCurrentDice.Count; i++) {
            if (myCurrentDice[i] == currentBidNumber) {
                actualAmount++;
            }
        }
        for (int j = 0; j < enemyCurrentDice.Count; j++){
            if (enemyCurrentDice[j] == currentBidNumber){
                actualAmount++;
            }
        }
        if (actualAmount >= currentBidAmount) {
            EnemyLoses();
        } else {
            PlayerLoses();
        }
    }

    public void EnemyBid (int amount, int number) {
        if (number > 6){
            number = 1;
            amount += 1;
        }
        currentBidAmount = amount;
        currentBidNumber = number;
      //  myTurn = true;
    }

    void PlayerLoses () {
        myDice--;
        if (myDice <= 0){
            GameOver();
        } else {

            StartCoroutine(PauseForRoll());
           //Roll();
        }
    }

    void EnemyLoses () {
        enemyDice--;
        if (enemyDice <= 0){
            EnemyGameOver();
        } else {

            StartCoroutine(PauseForRoll());
          //  Roll ();
        }

    }
    
    IEnumerator PauseForRoll () {
        Debug.Log("ok clear please");
        myCurrentDice.Clear();
        enemyCurrentDice.Clear();
        actualAmount = 0;
        currentBidAmount = 0;
        currentBidNumber = 0;
        yield return new WaitForSeconds (1f);
        foreach (GameObject e in enemyBattleDice) {
            e.GetComponent<EnemyDiceScript>().shouldShow = false;
        }
        yield return new WaitForSeconds (0.5f);
        if (enemyDice < 1){
            GameObject.Find("EnemyDice0").SetActive(false);
            GameObject.Find ("enemyGreenWire1").GetComponent<SpriteRenderer>().sortingOrder = 0;
        } else if (enemyDice < 2){
            GameObject.Find("EnemyDice1").SetActive(false);
            GameObject.Find("enemyGreenWire2").GetComponent<SpriteRenderer>().sortingOrder = 0;
        } else if (enemyDice < 3){
            GameObject.Find("EnemyDice2").SetActive(false);
            GameObject.Find("enemyGreenWire3").GetComponent<SpriteRenderer>().sortingOrder = 0;
        } else if (enemyDice < 4){
            GameObject.Find("EnemyDice3").SetActive(false);
            GameObject.Find("enemyGreenWire4").GetComponent<SpriteRenderer>().sortingOrder = 0;
        } else if (enemyDice < 5){
            GameObject.Find("EnemyDice4").SetActive(false);
            GameObject.Find("enemyGreenWire5").GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        if (myDice < 1){
            GameObject.Find("PlayerDice0").SetActive(false);
        } else if (myDice < 2){
            GameObject.Find("PlayerDice1").SetActive(false);
        } else if (myDice < 3){
            GameObject.Find("PlayerDice2").SetActive(false);
        } else if (myDice < 4){
            GameObject.Find("PlayerDice3").SetActive(false);
        } else if (myDice < 5){
            GameObject.Find("PlayerDice4").SetActive(false);
        }
        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        Roll();
    }

    IEnumerator PauseToFlipTurn (){
        yield return new WaitForSeconds (1f);
        myTurn = true;
    }

    void GameOver () {

    }

    void EnemyGameOver () {
        GameObject.Find("EnemyDice0").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("EnemyGreenWire1").GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}

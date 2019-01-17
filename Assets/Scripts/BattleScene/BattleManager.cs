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
    bool playerLose = false;

    public GameObject[] battleDice;
    public GameObject[] enemyBattleDice;
    public GameObject[] currentBidDice;

    public Sprite happyMaya;
    public Sprite sadMaya;
    public Sprite normalMaya;

    public Transform boxPos;

	// Use this for initialization
	void Start () {
       StartCoroutine(BeginBattle());


	}

    IEnumerator BeginBattle() {

        //TODO: THESE ARE NOT NECESARILY TRUE // figure out how to get that info
        myDice = 5;
        enemyDice = 5;

        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");

        //TODO: This naming sucks
        boxPos = GameObject.Find("teacup").GetComponent<Transform>();
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("RollArrow").GetComponent<RollForPlay>().Spin();
        yield return new WaitForSeconds(5f);
        Roll();
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
        //Debug.Log("ROLLING");

        //TODO: This naming sucks
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().myNumber = 0;
        myCurrentDice.Clear();
        enemyCurrentDice.Clear();
        actualAmount = 0;
        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        currentBidDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach (GameObject d in battleDice){
            //TODO: this won't work with special dice right?
            d.GetComponent<BaseDiceScript>().Roll();
        }
        foreach (GameObject e in enemyBattleDice){
           //TODO: gonna have special enemy dice? unlikely but possible
           e.GetComponent<EnemyDiceScript>().Roll();
        }
        foreach (GameObject b in currentBidDice){
            Destroy(b);
        }
        //TODO: necessary?
        GameObject.Find ("CurrentBidSlot").GetComponent<CurrentBidSlotScript>().shouldDestroy = true;

        //TODO: this stuff useless now?
        for (int i = 0; i < myDice; i++){
            int temp = Random.Range(0, 6);
          //  myCurrentDice.Add(temp);
        }
        for (int j = 0; j < enemyDice; j++){
            int temp = Random.Range(0, 6);
            //enemyCurrentDice.Add(temp);
        }

        //TODO: name sucks
        GameObject.Find("playerPortrait").GetComponent<SpriteRenderer>().sprite = normalMaya;
    }

    //This seems perfect
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

        //TODO: I don't think this should be here. Loser starts?
        myTurn = true;

        if (actualAmount >= currentBidAmount) {
            EnemyLoses();
        } else {
            PlayerLoses();
        }
    }

    public void EnemyBid (int amount, int number) {

        //TODO: This work should be done in enemy script
        if (number > 6){
            number = 1;
            amount += 1;
        }

        currentBidAmount = amount;
        currentBidNumber = number;
      //  myTurn = true;
    }

    void PlayerLoses () {
        GameObject.Find("playerPortrait").GetComponent<SpriteRenderer>().sprite = sadMaya;
        playerLose = true;
        myDice--;
        if (myDice <= 0){
            GameOver();
        } else {

            StartCoroutine(PauseForRoll(false));
           //Roll();
        }
    }

    void EnemyLoses () {
        GameObject.Find("playerPortrait").GetComponent<SpriteRenderer>().sprite = happyMaya;
        enemyDice--;
        if (enemyDice <= 0){

            //TODO: this can be done without GameObject.Find every time
            StartCoroutine(GameObject.Find("Spark").GetComponent<SparkScript>().SparkMe());
            EnemyGameOver();
        } else {

            //TODO: same thing about not doing it with GameObject.Find every time
            StartCoroutine(GameObject.Find("Spark").GetComponent<SparkScript>().SparkMe());
            StartCoroutine(PauseForRoll(true));
          //  Roll ();
        }

    }
    
    IEnumerator PauseForRoll (bool shouldPause) {
        if (shouldPause)
        {
            yield return new WaitForSeconds(3f);
        }
        //Debug.Log("ok clear please");
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

        //if (enemyDice < 1){
        //    GameObject.Find("EnemyDice0").SetActive(false);
        //    GameObject.Find ("enemyGreenWire1").GetComponent<SpriteRenderer>().sortingOrder = 0;
        //} else if (enemyDice < 2){
        //    GameObject.Find("EnemyDice1").SetActive(false);
        //    GameObject.Find("enemyGreenWire2").GetComponent<SpriteRenderer>().sortingOrder = 0;
        //} else if (enemyDice < 3){
        //    GameObject.Find("EnemyDice2").SetActive(false);
        //    GameObject.Find("enemyGreenWire3").GetComponent<SpriteRenderer>().sortingOrder = 0;
        //} else if (enemyDice < 4){
        //    GameObject.Find("EnemyDice3").SetActive(false);
        //    GameObject.Find("enemyGreenWire4").GetComponent<SpriteRenderer>().sortingOrder = 0;
        //} else if (enemyDice < 5){
        //    GameObject.Find("EnemyDice4").SetActive(false);
        //    GameObject.Find("enemyGreenWire5").GetComponent<SpriteRenderer>().sortingOrder = 0;
        //}

        //TODO: gotta be a better way to do this. Might be messed up when entering battle with fewer dice?
        if (playerLose)
        {
            if (myDice < 1)
            {
                GameObject.Find("PlayerDice0").SetActive(false);
            }
            else if (myDice < 2)
            {
                GameObject.Find("PlayerDice1").SetActive(false);
            }
            else if (myDice < 3)
            {
                GameObject.Find("PlayerDice2").SetActive(false);
            }
            else if (myDice < 4)
            {
                GameObject.Find("PlayerDice3").SetActive(false);
            }
            else if (myDice < 5)
            {
                GameObject.Find("PlayerDice4").SetActive(false);
            }
        }
        battleDice = GameObject.FindGameObjectsWithTag("Dice");
        enemyBattleDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        playerLose = false;
        Roll();
    }

    //TODO: is this used?
    IEnumerator PauseToFlipTurn (){
        yield return new WaitForSeconds (1f);
        myTurn = true;
    }

    void GameOver () {
        GameObject.Find("StatTracker").GetComponent<PlayerStatsTracker>().myDice = myDice;
    }

    void EnemyGameOver () {
        GameObject.Find("StatTracker").GetComponent<PlayerStatsTracker>().myDice = myDice;
        //GameObject.Find("EnemyDice0").GetComponent<SpriteRenderer>().enabled = false;
        //GameObject.Find("EnemyGreenWire1").GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}

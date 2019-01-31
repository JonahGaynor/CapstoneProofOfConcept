using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDiceScript : MonoBehaviour {
    public GenDiceScript diceProfile;
    public int myFace;
	// Use this for initialization
	void Start () {
        if (this.name == "PlayerDice0")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile1;
        }
        else if (this.name == "PlayerDice1")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile2;
        }
        else if (this.name == "PlayerDice2")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile3;
        }
        else if (this.name == "PlayerDice3")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile4;
        }
        else if (this.name == "PlayerDice4")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile5;
        }
    }
	
	// Update is called once per frame
    void Update () {
  //     if (this.GetComponent<GenDiceScript>().lifeSpan <= 0){
    //        Destroy(gameObject);
      //  }
	}

    public void Roll () {
        int temp = Random.Range(0, 6);
        myFace = diceProfile.faces[temp];
        if (this.tag == "Dice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().myCurrentDice.Add(myFace);
        } else if (this.tag == "EnemyDice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().enemyCurrentDice.Add(myFace);
        }
        this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[myFace - 1];

    }
}

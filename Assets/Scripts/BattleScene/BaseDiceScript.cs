using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDiceScript : MonoBehaviour {
    public GenDiceScript diceProfile;
    public int myFace;
    int myNumb;
    Vector3 myScale;

	// Use this for initialization
	void Start () {
        myScale = transform.localScale;
        if (this.name == "PlayerDice0")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile1;
            myNumb = 0;
        }
        else if (this.name == "PlayerDice1")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile2;
            myNumb = 1;
        }
        else if (this.name == "PlayerDice2")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile3;
            myNumb = 2;
        }
        else if (this.name == "PlayerDice3")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile4;
            myNumb = 3;
        }
        else if (this.name == "PlayerDice4")
        {
            diceProfile = GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile5;
            myNumb = 4;
        }
    }
	
	//// Update is called once per frame
 //   void Update () {
 // //     if (this.GetComponent<GenDiceScript>().lifeSpan <= 0){
 //   //        Destroy(gameObject);
 //     //  }
	//}

    public void Roll () {
        StartCoroutine(Juice());
        int temp = Random.Range(0, 6);
        myFace = diceProfile.faces[temp];
        if (this.tag == "Dice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().myCurrentDice.Add(myFace);
        } else if (this.tag == "EnemyDice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().enemyCurrentDice.Add(myFace);
        }
        this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[myFace - 1];

    }

    IEnumerator Juice()
    {
        yield return new WaitForSeconds(myNumb/5f);
        transform.localScale = myScale * 1.1f;
        yield return new WaitForSeconds(0.5f);
        transform.localScale = myScale;
    }
}

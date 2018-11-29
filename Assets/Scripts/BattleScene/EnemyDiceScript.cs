using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiceScript : MonoBehaviour {
    public GenDiceScript diceProfile;
    public int myFace;
    public bool shouldShow = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldShow){
            this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[myFace - 1];
        } else {
            this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[6];
        }
	}

    public void Roll()
    {
        int temp = Random.Range(0, 6);
        myFace = diceProfile.faces[temp];
        if (this.tag == "Dice")
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().myCurrentDice.Add(myFace);
        }
        else if (this.tag == "EnemyDice")
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyCurrentDice.Add(myFace);
        }
    }
}

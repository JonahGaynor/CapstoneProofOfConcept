using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiceScript : MonoBehaviour {
    public GenDiceScript diceProfile;
    public int myFace;
    public bool shouldShow = false;
    public Sprite[] rollingSprites;
    public Sprite baseSprite;

	// Update is called once per frame
	void Update () {
        if (shouldShow){
            this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[myFace - 1];
        }
	}

    public void Roll()
    {
        int temp = Random.Range(0, 6);
        StartCoroutine(Juice());
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

    IEnumerator Juice()
    {
        int rollNumb = 0;
        for (int i = 0; i < 73; i++)
        {
            yield return new WaitForSeconds(0.01f);
            this.GetComponent<SpriteRenderer>().sprite = rollingSprites[rollNumb];
            rollNumb++;
            if (rollNumb == 36)
            {
                rollNumb = 0;
            }
        }
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[6];
    }
}

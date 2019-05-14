using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDiceScript : MonoBehaviour {
    public GenDiceScript diceProfile;
    public int myFace;
    int myNumb;
    Vector3 myScale;
    public Sprite[] rollingSprites;
    public AudioClip rollSound;
    AudioSource myAudio;

    // Use this for initialization
    void Start () {
        myAudio = this.GetComponent<AudioSource>();

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
        StartCoroutine(RollNoise());
        StartCoroutine(Juice());
        int temp = Random.Range(0, 6);
        myFace = diceProfile.faces[temp];
        if (this.tag == "Dice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().myCurrentDice.Add(myFace);
        } else if (this.tag == "EnemyDice"){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().enemyCurrentDice.Add(myFace);
        }

    }

    IEnumerator RollNoise()
    {
        if (myNumb == 1)
        {
            yield return new WaitForSeconds(0.02f);
        }
        else if (myNumb == 2)
        {
            yield return new WaitForSeconds(0.04f);
        }
        else if (myNumb == 3)
        {
            yield return new WaitForSeconds(0.06f);
        }
        else if (myNumb == 4)
        {
            yield return new WaitForSeconds(0.08f);
        }
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
        myAudio.PlayOneShot(rollSound);
        yield return new WaitForSeconds(0.15f);
    }

    IEnumerator Juice()
    {
        int rollNumb = 0;
        for (int i = 0; i < 93; i++)
        {
            yield return new WaitForSeconds(0.01f);
            this.GetComponent<SpriteRenderer>().sprite = rollingSprites[rollNumb];
            //if (rollNumb == 0)
            //{
            //    Debug.Log(i);
            //}
            rollNumb++;
            if (rollNumb == 23)
            {
                rollNumb = 0;
            }
        }
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<SpriteRenderer>().sprite = diceProfile.mySprites[myFace - 1];
        myAudio.Stop();
        StopAllCoroutines();
        //yield return new WaitForSeconds(myNumb/5f);
        //transform.localScale = myScale * 1.1f;
        //yield return new WaitForSeconds(0.5f);
        //transform.localScale = myScale;
    }
}

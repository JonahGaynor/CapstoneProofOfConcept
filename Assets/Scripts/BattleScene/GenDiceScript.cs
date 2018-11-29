using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dice")]
public class GenDiceScript : ScriptableObject {

    public int[] faces;
    public Sprite[] mySprites;
    //public int lifeSpan;


    //// Use this for initialization
    //void Start () {
    //EnterBattle ();
    //}

    public int Roll()
    {
        int tempRand = Random.Range(0, 6);
        //this.GetComponent<SpriteRenderer> ().sprite = mySprites [tempRand];
        //lifeSpan--;
        return tempRand;
    }

    public void EnterBattle()
    {
     //   lifeSpan--;
        Roll();
    }
}

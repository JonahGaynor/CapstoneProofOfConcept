using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardNumberScript : MonoBehaviour {

    public Sprite[] mySprites;
    public int myNumber = 0;

    public void NewNumber(int newNumb)
    {
        this.GetComponent<Image>().sprite = mySprites[newNumb];

    }
}

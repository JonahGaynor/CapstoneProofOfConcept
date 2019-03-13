using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardNumberScript : MonoBehaviour {

    public Sprite[] mySprites;
    public int myNumber = 0;
    int zeroCounter = 0;
    int oneCounter = 0;
    int twoCounter = 0;
    int threeCounter = 0;

    public void NewNumber(int newNumb)
    {
        if (newNumb == 0)
        {
            if (zeroCounter == 0)
            {
                this.GetComponent<Image>().sprite = mySprites[0];
                zeroCounter++;
            }
            else if (zeroCounter == 1)
            {
                this.GetComponent<Image>().sprite = mySprites[1];
                zeroCounter++;
            }
            else if (zeroCounter == 2)
            {
                this.GetComponent<Image>().sprite = mySprites[2];
                zeroCounter = 0;
            }
        }
        if (newNumb == 1)
        {
            if (oneCounter == 0)
            {
                this.GetComponent<Image>().sprite = mySprites[3];
                oneCounter++;
            }
            else if (oneCounter == 1)
            {
                this.GetComponent<Image>().sprite = mySprites[4];
                oneCounter = 0;
            }
        }
        if (newNumb == 2)
        {
            if (twoCounter == 0)
            {
                this.GetComponent<Image>().sprite = mySprites[5];
                twoCounter++;
            }
            else if (twoCounter == 1)
            {
                this.GetComponent<Image>().sprite = mySprites[6];
                twoCounter = 0;
            }
        }
        if (newNumb == 3)
        {
            if (threeCounter == 0)
            {
                this.GetComponent<Image>().sprite = mySprites[7];
                threeCounter++;
            }
            else if (threeCounter == 1)
            {
                this.GetComponent<Image>().sprite = mySprites[8];
                threeCounter = 0;
            }
        }

    }
}

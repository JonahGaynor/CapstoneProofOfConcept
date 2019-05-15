using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardNumberScript : MonoBehaviour {

    public Sprite[] mySprites;
    public int myNumber = 0;
    int zeroCounter = 0;
    int oneCounter = 0;
    int twoCounter = 0;
    int threeCounter = 0;
    int puzzleNumber;
    public Sprite[] puzzle3Sprites;
    public int puzzle3Counter = 0;

    public static int completedPuzzles;

    void Start()
    {
        if (completedPuzzles < 1)
        {
            completedPuzzles = 0;
        }
        if (completedPuzzles == 2)
        {
            puzzleNumber = 3;
        }
        else if (completedPuzzles == 3)
        {
            puzzleNumber = 4;
        }
        else
        {
            puzzleNumber = 1;
        }
    }

    public void NewNumber(int newNumb)
    {
        if (puzzleNumber == 1)
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
        else if (puzzleNumber == 3)
        {
            this.GetComponent<Image>().sprite = puzzle3Sprites[puzzle3Counter];
            puzzle3Counter++;
        }
        else if (puzzleNumber == 4)
        {
            this.GetComponent<Image>().sprite = puzzle3Sprites[puzzle3Counter];
            puzzle3Counter++;
        }
    }
}

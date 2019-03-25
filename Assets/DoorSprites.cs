using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprites : MonoBehaviour {

    public int myNumber;
    public Sprite[] mySprites;

    public int requiredNumber;

    public bool isClosed = true;

    public SpriteRenderer sr;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public void Open(int completedPuzzles)
    {
        Debug.Log("open sesame");
        if (completedPuzzles >= requiredNumber)
        {
            myNumber = 1;
            sr.sprite = mySprites[myNumber];
            isClosed = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            myNumber = 0;
            sr.sprite = mySprites[myNumber];
            isClosed = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

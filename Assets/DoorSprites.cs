using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprites : MonoBehaviour {

    public int myNumber;
    public Sprite[] mySprites;

    public int requiredNumber;

    public bool isClosed = true;

    public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

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
    }
}

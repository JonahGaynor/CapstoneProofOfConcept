using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprites : MonoBehaviour {

    public int myNumber;
    public Sprite[] mySprites;

    public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        sr.sprite = mySprites[myNumber];
	}
}

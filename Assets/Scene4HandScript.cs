using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4HandScript : MonoBehaviour {

    public Sprite[] mySprites;
    public int myNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[myNumber];
	}
}

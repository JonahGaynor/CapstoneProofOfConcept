using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3TextFace : MonoBehaviour {

	public Sprite[] mySprites;
    public int myNumber = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[myNumber];
	}
}

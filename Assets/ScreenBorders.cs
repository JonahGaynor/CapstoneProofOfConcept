using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(this.gameObject);
        player = GameObject.Find("Player");
        transform.position = new Vector3(0.5f, player.transform.position.y, 0);
	}

    public void CheckBeforeChange()
    {
        this.transform.position = new Vector3(0, player.transform.position.y, 0);

    }

    public void Check()
    {
        player = GameObject.Find("Player");
        transform.position = Vector3.zero;
    }
}

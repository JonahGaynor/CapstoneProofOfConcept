using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSparkScript : MonoBehaviour {

    GameObject sparker;
    bool haveCoroutine = false;

	// Use this for initialization
	void Start () {
        sparker = GameObject.Find("Spark");
	}
	
	// Update is called once per frame
	void Update () {
        if (sparker.GetComponent<SparkScript>().finishedSparking && !haveCoroutine)
        {
            StartCoroutine(this.GetComponentInParent<SceneTransitionScript>().FadeToBlack());
            haveCoroutine = true;
        }
	}
}

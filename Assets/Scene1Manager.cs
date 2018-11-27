using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(TheEntireScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator TheEntireScene (){
        yield return new WaitForSeconds(0.4f);
        GameObject.Find ("Main Camera").GetComponent<Scene1Camera>().active = true;
        yield return new WaitForSeconds(5f);
        GameObject.Find ("Agent1").GetComponent<Scene1Agent1Script>().active = true;
        GameObject.Find ("Agent2").GetComponent<Scene1Agent2>().active = true;
        yield return new WaitForSeconds (11.1f);
        GameObject.Find ("Player").GetComponent<Scene1MayaScript>().active = true;
    }
}

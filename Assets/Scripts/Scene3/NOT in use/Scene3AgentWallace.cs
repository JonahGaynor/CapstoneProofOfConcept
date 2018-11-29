using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3AgentWallace : MonoBehaviour {

    public bool active = false;
    //bool active1 = false;
    int enterCounter = 0;
   // bool active2 = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool active1(){
        if (enterCounter > 0 && !active){
            return true;
        } else {
            return false;
        }

    }

    public bool active2(){
        if (enterCounter > 1 && !active){
            return true;
        } else {
            return false;
        }
    }

    IEnumerator TheBigOne (){
        //turn on text 1
        yield return new WaitUntil(active1);
        //turn on text 2
        yield return new WaitUntil(active2);
    }
}

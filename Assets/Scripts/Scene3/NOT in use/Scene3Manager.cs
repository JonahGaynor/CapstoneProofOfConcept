using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3Manager : MonoBehaviour {

   // public bool wallaceDone1 = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(TheBigOne());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool wallaceDone1 (){
        if (!GameObject.Find ("WallaceText1").active){
            return true;
        } else {
            return false;
        }
    }

    public bool mayaDone1 (){
        if (!GameObject.Find ("MayaIDCard").active){
            return true;
        } else {
            return false;
        }
    }

    public bool wallaceDone2(){
        if (!GameObject.Find ("WallaceText2").active){
            return true;
        } else {
            return false;
        }
    }


    IEnumerator TheBigOne(){
        yield return new WaitForSeconds(0.5f);
        //Agent Wallace active
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(wallaceDone1);
        //Maya's ID card active
        yield return new WaitUntil(mayaDone1);
        //Agent Wallace active part 2
        yield return new WaitUntil(wallaceDone2);
        SceneManager.LoadScene("ProofofConcept_Scene4");
    }
}

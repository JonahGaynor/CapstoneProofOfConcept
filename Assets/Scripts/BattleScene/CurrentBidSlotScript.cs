using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBidSlotScript : MonoBehaviour {

    public bool shouldDestroy = false;
    bool haveStartedCoroutine = false;
    GameObject[] myDice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
        myDice = GameObject.FindGameObjectsWithTag("DestroyMe");
        if (shouldDestroy && !haveStartedCoroutine){
            foreach (GameObject m in myDice){
                if (m.transform.position.y > 2.3){
                    Destroy(m);
                }
            }
            StartCoroutine(StopDestroy());
            haveStartedCoroutine = true;
        }
        if (shouldDestroy){
            foreach (GameObject m in myDice){
                Destroy(m);
            }
        }
	}

    void OnCollisionEnter2D (Collision2D col){
            col.gameObject.tag = "DestroyMe";
          //  shouldDestroy = false;

    }
    void OnCollisionStay2D (Collision2D col){
        if (shouldDestroy && col.gameObject.tag == "DestroyMe" && col.transform.position.y > 1.5f){
            Destroy(col.gameObject);
        }
    }

    IEnumerator StopDestroy(){
        yield return new WaitForSeconds(0.2f);
        shouldDestroy = false;
        haveStartedCoroutine = false;
    }
}

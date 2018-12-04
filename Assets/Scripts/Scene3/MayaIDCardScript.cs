using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MayaIDCardScript : MonoBehaviour {

    float targetY = 0;
    bool moveDown = false;
    public bool active = false;
    bool startCoroutine = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (active){
            if (moveDown) {
                if (!startCoroutine) {
                    StartCoroutine(Move());
                    startCoroutine = true;
                }
                targetY = -10f;
                transform.position = new Vector3(transform.position.x, ((transform.position.y * 9) + targetY) / 10, 0);
            } else if (transform.position.y > targetY){
                transform.position = new Vector3 (transform.position.x, ((transform.position.y * 9)/10), 0);
            }
            if (transform.position.y < 1 && Input.GetKeyUp(KeyCode.Return)){
                moveDown = true;
            }
            if (this.transform.position.y < -9f){
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Move() {
        Debug.Log("we in");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ProofofConcept_Scene4");
    }
}

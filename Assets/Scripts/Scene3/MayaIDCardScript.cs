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


	// Update is called once per frame
	void Update () {
        //if (FindObjectOfType<DialogueManager>().done && GameObject.Find("TextStarter_Petty").GetComponent<Scene3Petty>().storageCounter == 7)
        //{
        //    active = true;
        //}
        if (active){
            if (!startCoroutine)
            {
                StartCoroutine(Move());
                startCoroutine = true;
            }
            if (moveDown) {

                targetY = -10f;
                transform.position = new Vector3(transform.position.x, ((transform.position.y * 9) + targetY) / 10, 0);
            } else if (transform.position.y > targetY){
                transform.position = new Vector3 (transform.position.x, ((transform.position.y * 9)/10), 0);
            }
            if (transform.position.y < 1 && Input.GetKeyUp(KeyCode.Return)){
                moveDown = true;
            }
            if (this.transform.position.y < -9f){
                //this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Move() {
        Debug.Log("we in");
        yield return new WaitForSeconds(1f);
        //Debug.Log("move scenes now plz");
        SceneManager.LoadScene("ProofofConcept_Scene4");
        yield return new WaitForSeconds(0.1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene3TextScript : MonoBehaviour {

    int enterCounter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Return)){
            enterCounter++;
            if (enterCounter == 1){
                this.GetComponent<Text>().text = "As you know, the Central Computer is vital to our freedom. Our government will collapse without it. When it was set up a decade ago, the scientists and engineers on the job set up a series of obstacles and artificial intelligence robots to ensure that breaking in would be impossible without codeword level clearance that only a handful of people on the planet had.";
            } else if (enterCounter == 2){
                this.GetComponent<Text>().text = "Well, earlier today, some unidentified individuals tried to break into the computer. This isn’t unusual. We get at least a dozen of these every year and once they are rebuked by the computer’s security, we deal with them.";
            } else if (enterCounter == 3){
                this.GetComponent<Text>().text = "Today, however, was different. This latest attempt was successful, and these intruders have made it all the way to the center, putting our freedom in danger.";
            } else if (enterCounter == 4){
                this.GetComponent<Text>().text = "And I’m here for some reason?";
                GameObject.Find ("TextFace").GetComponent<Scene3TextFace>().myNumber = 1;
            } else if (enterCounter == 5){
                this.GetComponent<Text>().text = "We need you to follow them inside the computer and stop them before they destroy the computer. Your remarkable IQ score and similarly remarkable lack of income made you a candidate 3.7% more likely to accept our offer.";
                GameObject.Find ("TextFace").GetComponent<Scene3TextFace>().myNumber = 0;
            } else if (enterCounter == 6){
                this.GetComponent<Text>().text = "Which is?";
                GameObject.Find ("TextFace").GetComponent<Scene3TextFace>().myNumber = 1;
            } else if (enterCounter == 7){
                this.GetComponent<Text>().text = "If you stop these intruders, you will be set for life as a thanks from the government. I’m assuming that entices you.";
                GameObject.Find("TextFace").GetComponent<Scene3TextFace>().myNumber = 0;
            } else if (enterCounter == 8){
                this.GetComponent<Text>().text = "It does.";
                GameObject.Find("TextFace").GetComponent<Scene3TextFace>().myNumber = 1;
            } else if (enterCounter == 9){
                this.GetComponent<Text>().text = "At every layer of the Central Computer, you’ll face various challenges that you’ll need to overcome in order to reach the end of the layer, and face increasingly complex AI players. They play an ancient game using a tool from the early 21st century: dyce.";
                GameObject.Find("TextFace").GetComponent<Scene3TextFace>().myNumber = 0;
            } else if (enterCounter == 10){
                this.GetComponent<Text>().text = "Dice.";
                GameObject.Find("TextFace").GetComponent<Scene3TextFace>().myNumber = 2;
            } else if (enterCounter == 11){
                this.GetComponent<Text>().text = "Thanks. I’ve never been a good speller. Maya, you’ll need this. Let me show you how the game works.";
                GameObject.Find("TextFace").GetComponent<Scene3TextFace>().myNumber = 0;
                GameObject.Find ("MayaIDCard").SetActive(true);
                StartCoroutine (MovetoNextScene());
            } 
        }
	}

    IEnumerator MovetoNextScene(){
        yield return new WaitForSeconds (2f);
        SceneManager.LoadScene("ProofofConcept_Scene4");
    }
}

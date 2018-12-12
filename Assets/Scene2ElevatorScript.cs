using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2ElevatorScript : MonoBehaviour {

    float timer = 0;
    public Sprite[] mySprites;

    AudioSource myAudio;
    public AudioClip dingDing;

    bool haveDoneAudio = false;

	// Use this for initialization
	void Start () {
        myAudio = this.GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 10.75f) {
            SceneManager.LoadScene("ProofofConcept_Scene3");
        } else if (timer > 8.55f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[5];
            GameObject.Find("ElevatorMusic").GetComponent<AudioSource>().Stop();
            //myAudio.PlayOneShot(dingDing);
            if (!haveDoneAudio) {
                myAudio.PlayOneShot(dingDing);
                haveDoneAudio = true;
            }
        } else if (timer > 7.7f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[4];
        } else if (timer > 6.2f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[3];
        } else if (timer > 4.7f){
            this.GetComponent<SpriteRenderer>().sprite = mySprites[2];
        } else if (timer > 3.2f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[1];
        } else {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        }
    }
}

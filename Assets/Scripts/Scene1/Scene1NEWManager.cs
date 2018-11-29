using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1NEWManager : MonoBehaviour {

    public Sprite[] mySprites;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        StartCoroutine(TheBigOne());
	}


    IEnumerator TheBigOne (){
        yield return new WaitForSeconds (2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[1];
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[2];
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[3];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[4];
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[5];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[6];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[7];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[8];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[9];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[10];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[11];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[12];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[13];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[14];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[15];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[16];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[17];
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ProofofConcept_Scene2");
    }
}

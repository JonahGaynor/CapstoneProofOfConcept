using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene4CallButtonScript : MonoBehaviour {

    public GameObject[] biddingDice;
    public Sprite mayaHappy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick (){
        GameObject.Find ("Scene4Text").GetComponent<Text>().text = "Well done.";
        biddingDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach(GameObject b in biddingDice){
            Destroy(b);
        }
        StartCoroutine(ToExit());
    }

    IEnumerator ToExit(){
        GameObject.Find("Border").SetActive(false);
        //GameObject.Find("CallButton").SetActive(false);
        GameObject.Find("playerPortrait").GetComponent<SpriteRenderer>().sprite = mayaHappy;
        //GameObject.Find ("CallButtonBoarder").SetActive(false);
        this.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds (1f);
        SceneManager.LoadScene("ProofofConcept_Scene5");
    }
}

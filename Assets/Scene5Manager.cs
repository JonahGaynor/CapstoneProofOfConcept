using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene5Manager : MonoBehaviour {

    int enterCount = 0;
    public Sprite secondSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Return))
        {

            enterCount++;
        }
        if (enterCount >= 5)
        {
            SceneManager.LoadScene("ProofofConcept_Scene6");
        } else if (enterCount == 3)
        {
            GameObject.Find("AgentSprite").GetComponent<SpriteRenderer>().sprite = secondSprite;
        }
    }
}

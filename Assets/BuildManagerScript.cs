using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManagerScript : MonoBehaviour {


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("pressed n");
            StartCoroutine(GameObject.Find("endtile").GetComponent<EndTileScript>().PauseToChangeLevel());
        }
	}
}

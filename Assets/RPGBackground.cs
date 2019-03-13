using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBackground : MonoBehaviour {

    public GameObject clickEffect;
    //Vector2 mousePos;
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0.5f, GameObject.Find("Player").transform.position.y, 0), 0.1f);
        
	}

    //private void OnMouseDown()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        //clickEffect = Instantiate(mousePos,);
    //        Instantiate(clickEffect, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
    //    }
    //}
}

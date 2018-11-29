using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scene1Camera : MonoBehaviour {

    public bool active = false;
    public Camera myCam;


	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
    void Update () {
       // Debug.Log(Camera.main.fieldOfView);
        if (active && myCam.orthographicSize <= 5f){
            myCam.orthographicSize += ((6 - myCam.orthographicSize)/2) * Time.deltaTime;
            if (myCam.transform.rotation.z >= 0){
                myCam.transform.rotation = Quaternion.EulerAngles (0, 0, myCam.transform.rotation.z - (0.00000005f * Time.deltaTime));
            }
         //   myCam.orthographicSize += Time.deltaTime;
        }
   //     DORotate(Vector3.zero, 10);
	}
}

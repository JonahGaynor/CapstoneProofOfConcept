using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDiceNotInUse : MonoBehaviour {

    public bool selected = false;

    public bool isOn;

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void OnClick()
    {
        if (isOn)
        {
            if (!selected)
            {
                selected = true;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            else
            {
                selected = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}

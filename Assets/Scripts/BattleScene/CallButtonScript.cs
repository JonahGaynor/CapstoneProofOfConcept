using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallButtonScript : MonoBehaviour {

    GameObject eventSystem;

	// Use this for initialization
	void Start () {
        eventSystem = GameObject.Find("EventSystem");
	}

    public void OnClick () {
        if (GameObject.Find("BattleManager").GetComponent<BattleManager>().myTurn)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().PlayerCall();
            GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        }
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}

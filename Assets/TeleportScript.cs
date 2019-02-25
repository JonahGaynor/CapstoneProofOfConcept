using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour {

    public Transform player;

    //public int myEnemy;

    Scene thisScene;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y >= this.transform.position.y + 0.4f)
        {
            //GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().enemyHit = myEnemy;
            //GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().myPos = player.position;
            StartCoroutine(PauseToChangeLevel());
        }
    }

    IEnumerator PauseToChangeLevel()
    {
        //GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().EnterBattle();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightMeFinalSceneManager : MonoBehaviour {

    public GameObject dMan;
    bool hasStarted = false;
    bool hasStarted2 = false;
    bool hasStarted3 = false;
    bool hasStarted4 = false;
    public static bool agentsWalkedOut = false;
    public static bool pettyFinished = false;
    public static bool Agent4Finished = false;
    public Sprite[] upSprites;
    public Sprite[] rightSprites;
    public Sprite[] downSprites;
    public Sprite[] leftSprites;
    public Sprite[] wallaceShoot;
    public Sprite[] mayaShot;
    public Sprite mayaSprite;
    public Sprite mayaSprite2;
    bool up = false;
    bool right = false;
    bool down = false;
    bool left = false;
    Vector2 targetPos;
    Vector2 upPos;
    Vector2 rightPos;
    Vector2 downPos;
    Vector2 leftPos;
    int spriteCounter = 0;
    float spriteCounterTimer;
    bool active = true;
    public Dialogue afterWalkIn;
    public Dialogue pettyDialogue;

    public AudioClip bang;
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        dMan = GameObject.Find("DialogueManager");
	}
	
	// Update is called once per frame
	void Update () {
        if (dMan.GetComponent<DialogueManager>().done && !hasStarted)
        {
            hasStarted = true;
            if (this.tag == "AgentPetty")
            {
                Debug.Log("petty");
                StartCoroutine(Petty());
            }
            else if (this.tag == "AgentWallace")
            {
                Debug.Log("wallace");
                StartCoroutine(Wallace());
            }
            else if (this.tag == "AgentThree")
            {
                Debug.Log("agentthree");
                StartCoroutine(AgentThree());
            }
            else if (this.tag == "AgentFour")
            {
                Debug.Log("agentfour");
                StartCoroutine(AgentFour());
            }
        }
        else if (Agent4Finished && !hasStarted2 && dMan.GetComponent<DialogueManager>().done)
        {
            hasStarted2 = true;
            if (this.tag == "AgentPetty")
            {
                Debug.Log("petty2");
                StartCoroutine(Petty2());
            }
            //else if (this.tag == "AgentWallace")
            //{
            //    Debug.Log("wallace2");
            //    StartCoroutine(Wallace2());
            //}
            else if (this.tag == "AgentThree")
            {
                Debug.Log("agentthree2");
                StartCoroutine(AgentThree2());
            }
            else if (this.tag == "AgentFour")
            {
                Debug.Log("agentfour2");
                StartCoroutine(AgentFour2());
            }
            else if (this.tag == "Simon")
            {
                StartCoroutine(Simon());
            }
        }
        else if (agentsWalkedOut && !hasStarted3 && dMan.GetComponent<DialogueManager>().done)
        {
            hasStarted3 = true;
            if (this.tag == "AgentPetty")
            {
                StartCoroutine(Petty3());
            }
        }
        else if (pettyFinished && !hasStarted4)
        {
            hasStarted4 = true;
            if (this.tag == "AgentWallace")
            {
                StartCoroutine(Wallace2());
            }
        }
        if (hasStarted && active)
        {
            spriteCounterTimer += Time.deltaTime;
            if (spriteCounterTimer >= 0.08f)
            {
                spriteCounterTimer = 0;
                if (spriteCounter == 2)
                {
                    spriteCounter = 0;
                }
                else
                {
                    spriteCounter++;
                }
            }
            if (up)
            {
                this.GetComponent<SpriteRenderer>().sprite = upSprites[spriteCounter];
                transform.position = upPos;
                upPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
                if (transform.position.y >= targetPos.y)
                {
                    transform.position = targetPos;
                    up = false;
                    GetComponent<SpriteRenderer>().sprite = upSprites[0];
                }
            }
            else if (right)
            {
                this.GetComponent<SpriteRenderer>().sprite = rightSprites[spriteCounter];
                transform.position = rightPos;
                rightPos = new Vector2(transform.position.x + 0.1f, transform.position.y);
                if (transform.position.x >= targetPos.x)
                {
                    transform.position = targetPos;
                    right = false;
                    GetComponent<SpriteRenderer>().sprite = rightSprites[0];
                }
            }
            else if (down)
            {
                this.GetComponent<SpriteRenderer>().sprite = downSprites[spriteCounter];
                transform.position = downPos;
                downPos = new Vector2(transform.position.x, transform.position.y - 0.1f);
                if (transform.position.y <= targetPos.y)
                {
                    transform.position = targetPos;
                    down = false;
                    GetComponent<SpriteRenderer>().sprite = downSprites[0];
                }
            }
            else if (left)
            {
                this.GetComponent<SpriteRenderer>().sprite = leftSprites[spriteCounter];
                transform.position = leftPos;
                leftPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
                if (transform.position.x <= targetPos.x)
                {
                    transform.position = targetPos;
                    left = false;
                    GetComponent<SpriteRenderer>().sprite = leftSprites[0];
                }
            }
        }
    }

    IEnumerator Petty()
    {
        GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = false;
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume -= 0.02f;
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        this.GetComponent<SpriteRenderer>().sprite = rightSprites[0];
        this.GetComponent<SpriteRenderer>().sortingOrder = 78;
    }

    IEnumerator Petty2()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("Player").GetComponent<PlayerMovementScript>().MoveLeft();
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = mayaSprite;
        yield return new WaitForSeconds(2f);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        GameObject.Find("Player").GetComponent<PlayerMovementScript>().MoveRight();
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = mayaSprite2;
        this.GetComponent<SpriteRenderer>().sprite = rightSprites[0];
        FindObjectOfType<DialogueManager>().StartDialogue(pettyDialogue);
    }

    IEnumerator Petty3()
    {
        Debug.Log("in petty 3");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("let's move");
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        pettyFinished = true;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = mayaSprite;
    }

    IEnumerator Wallace()
    {
        yield return new WaitForSeconds(0.1f);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        this.GetComponent<SpriteRenderer>().sprite = leftSprites[0];
    }

    IEnumerator Wallace2()
    {
        Debug.Log("wallace2");
        yield return new WaitForSeconds(0.1f);
        myAudio = this.GetComponent<AudioSource>();
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        this.GetComponent<SpriteRenderer>().sprite = leftSprites[0];
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < wallaceShoot.Length; i++)
        {
            this.GetComponent<SpriteRenderer>().sprite = wallaceShoot[i];
            if (i == 4)
            {
                myAudio.PlayOneShot(bang);
            }
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = mayaShot[i];
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < mayaShot.Length - 6; i++)
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = mayaShot[i + 6];
        }
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = leftSprites[0];
        this.GetComponent<SpriteRenderer>().sortingOrder = 100;
        yield return new WaitForSeconds(2f);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        float a = 0.1f;
        //GameObject.Find("WhiteOut").GetComponent<SpriteRenderer>().color = new Color(100, 100, 100, 1);
        GameObject whiteOut = GameObject.FindGameObjectWithTag("WhiteOut");
        for (int i = 0; i < 10; i++)
        {
            whiteOut.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, a);
            a += 0.1f;
            yield return new WaitForSeconds(0.18f);
            Debug.Log(i);
            Debug.Log(a);
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Production_Credits");
    }

    IEnumerator Simon()
    {
        yield return new WaitForSeconds(0.4f);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
    }

    IEnumerator AgentThree()
    {
        yield return new WaitForSeconds(0.1f);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        this.GetComponent<SpriteRenderer>().sprite = rightSprites[0];
    }

    IEnumerator AgentThree2()
    {
        yield return new WaitForSeconds(0.4f);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
    }

    IEnumerator AgentFour()
    {
        Debug.Log("please?");
        yield return new WaitForSeconds(0.1f);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        rightPos = new Vector3(transform.position.x + 0.1f, transform.position.y, 0);
        right = true;
        targetPos = transform.position + Vector3.right;
        yield return new WaitWhile(() => right);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        upPos = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
        up = true;
        targetPos = transform.position + Vector3.up;
        yield return new WaitWhile(() => up);
        this.GetComponent<SpriteRenderer>().sprite = leftSprites[0];
        FindObjectOfType<DialogueManager>().StartDialogue(afterWalkIn);
        //yield return new WaitForSeconds(0.2f);
        Agent4Finished = true;
    }

    IEnumerator AgentFour2()
    {
        yield return new WaitForSeconds(0.1f);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        leftPos = new Vector3(transform.position.x - 0.1f, transform.position.y, 0);
        left = true;
        targetPos = transform.position + Vector3.left;
        yield return new WaitWhile(() => left);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        downPos = new Vector3(transform.position.x, transform.position.y - 0.1f, 0);
        down = true;
        targetPos = transform.position + Vector3.down;
        yield return new WaitWhile(() => down);
        yield return new WaitForSeconds(1f);
        agentsWalkedOut = true;
    }
}

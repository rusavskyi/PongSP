using UnityEngine;
using UnityEngine.UI;
using PongClasses;

public class Game : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public Text timeCountText;
    public int maxBallsInPlay;
    public GameObject endMenu;
    public int numberOfPlayers;
    public bool inPlay = false;

    private int ballsInPlayCounter;
    private float timeCount;
    private GameObject[] players;
    private GameObject[] ballSpawns;
    private System.Random rand;

    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        ballSpawns = GameObject.FindGameObjectsWithTag("BallSpawn");
    }

    void Start()
    {

        Time.timeScale = 1f;
        //Cursor.visible = false;
        timeCount = 3f;
        rand = new System.Random();
        CreatePlayer(1, false);
        for (int i = 1; i < numberOfPlayers; i++)
        {
            CreatePlayer(i + 1, true);
        }
        while (players.Length < numberOfPlayers)
        {
            players = null;
            players = GameObject.FindGameObjectsWithTag("Player");
        }
    }


    void Update()
    {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
            if (timeCount > 2)
                timeCountText.text = "3";
            else if (timeCount > 1)
                timeCountText.text = "2";
            else if (timeCount > 0)
                timeCountText.text = "1";
        }
        else
        {
            inPlay = true;
            if (timeCountText != null) Destroy(timeCountText);
            ballsInPlayCounter = GameObject.FindGameObjectsWithTag("Ball").Length; ;
            if (ballsInPlayCounter < maxBallsInPlay)
            {
                CreateBall();
            }
        }
        if (Input.GetButtonDown("Cancel"))
            Pause();
        if (Input.GetButtonDown("f1") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new BlackoutEffect();
        }
        if (Input.GetButtonDown("f2") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new ShieldEffect();
        }
        if (Input.GetButtonDown("f3") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new ShockEffect();
        }
        if (Input.GetButtonDown("f4") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new BoostEffect();
        }
        if (Input.GetButtonDown("f5") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new ConfusionEffect();
        }
        if (Input.GetButtonDown("f6") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new HealingEffect();
        }
        if (Input.GetButtonDown("f7") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new PaddleSqueezeEffect();
        }
        if (Input.GetButtonDown("f8") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new PaddleStretchEffect();
        }
        if (Input.GetButtonDown("f9") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new FissionEffect();
        }
        if (Input.GetButtonDown("f10") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new ReflectorEffect();
        }
        if (Input.GetButtonDown("f11") && players[0].GetComponent<PlayerController>().player._storedEffect == null)
        {
            players[0].GetComponent<PlayerController>().player._storedEffect = new SlippingEffect();
        }
    }

    private void Pause()
    {
        if (Time.timeScale == 1.0)
        {
            inPlay = false;
            Time.timeScale = 0.00001f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endMenu.SetActive(true);
        }
        else
        {
            inPlay = true;
            Time.timeScale = 1.0f;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            endMenu.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        EffectsActivation();
        EndGameCheck();
    }

    public void EffectsActivation()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Effect effect = players[i].GetComponent<PlayerController>().player._storedEffect;
            if (effect != null)
            {
                effect.Action(players[i]);
                if (!effect.IsActive())
                {
                    players[i].GetComponent<PlayerController>().player._storedEffect = null;
                }
            }
        }
    }

    public void EndGameCheck()
    {
        int count = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerController>().GetPlayer() == null)
            {
                continue;
            }
            if (players[i].GetComponent<PlayerController>().GetPlayer().GetHeath() < 1)
            {
                players[i].SetActive(false);
                GameObject.Find("Player" + (i + 1) + "SpawnPosition").GetComponentInChildren<WallController>().Enable(true);
                count++;
            }
        }
        if (count >= numberOfPlayers - 1)
        {
            ballsInPlayCounter = 0;
            Time.timeScale = 0.00001f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            endMenu.SetActive(true);
        }
    }

    void CreateBall()
    {
        Vector3 ballPos = ballSpawns[rand.Next(0, ballSpawns.Length)].transform.position;
        Instantiate(ballPrefab, ballPos, Quaternion.identity);
    }


    void CreatePlayer(int id, bool AI = false, PaddleScript.difficultyLevel diffLevel = PaddleScript.difficultyLevel.HARD)
    {
        GameObject newPlayer = playerPrefab;
        string playerString = "Player" + (id) + "SpawnPosition";
        newPlayer.transform.position = GameObject.Find(playerString).transform.position;
        newPlayer.transform.rotation = GameObject.Find(playerString).transform.rotation;
        newPlayer.GetComponent<PlayerController>().playerID = id;
        newPlayer.GetComponentInChildren<PaddleScript>().thisDifficultyLevel = diffLevel;
        GameObject.Find(playerString).GetComponentInChildren<WallController>().Enable(false);
        newPlayer.GetComponent<PlayerController>().AI = AI;
        Instantiate(newPlayer);
    }
}

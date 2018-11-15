using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BallScript : MonoBehaviour
{
    //creat ball velocity
    public float ballInitialVelocity = 0.01f;
    private float randomStartForce;
    //a rigidbody variable
    private Rigidbody rb;
    //variable for checking condition if a ball is launched
    private bool ballInPlay;
    public int MaxSpeed;
    public int MinSpeed;
    int nPlayers;
    System.Random rand;

    public int speedBonus { get; set; }
    private float boostTime = 5.0f;
    private float currentBoostTime = 0.0f;


    // Use this for initialization
    void Awake()
    {
        rand = new System.Random();
        randomStartForce = (float)rand.Next(200, 600);
        nPlayers = GameObject.Find("Arena").GetComponent<Game>().numberOfPlayers;
        //set a rigidbody component to variable rb
        rb = GetComponent<Rigidbody>();
        transform.parent = null;
        ballInPlay = true;
        rb.isKinematic = false;
        /// Choosing force to push the ball depending on it's creating position
        if ((rb.transform.position.x < 0 && rb.transform.position.z > 0) || (rb.transform.position.x > 0 && rb.transform.position.z < 0)) //If ball on left bottom corner or top right
        {
            rb.AddForce(new Vector3(ballInitialVelocity + randomStartForce, 0.0f, -ballInitialVelocity));
        }
        else
        {
            if ((nPlayers == 3) || (nPlayers == 4)) ////Fixing right top corner ball in 3p\4p mode
            {
                if (rb.transform.position.x < 0 && rb.transform.position.z < 0)
                {
                    rb.AddForce(new Vector3(ballInitialVelocity + randomStartForce, 0.0f, ballInitialVelocity));
                }
                else
                {
                    rb.AddForce(new Vector3(-ballInitialVelocity - randomStartForce, 0.0f, -ballInitialVelocity));
                }
            }
            else
            {
                rb.AddForce(new Vector3(-ballInitialVelocity - randomStartForce, 0.0f, -ballInitialVelocity));
            }
        }
        //rb.transform.LookAt(new Vector3(0, 0, 0));
        //rb.AddForceAtPosition(new Vector3(ballInitialVelocity + randomStartForce, 0.0f, ballInitialVelocity), new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y != 0.5) //Freezing y-pos (height) of ball
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }
    }

    void FixedUpdate()
    {
        /*if (ballInPlay && Math.Abs(rb.velocity.x) > 28 || Math.Abs(rb.velocity.z) > 28) //if speed > maxSpeed, drag (slow ball)
        {
            rb.drag = 1;
        }
        else rb.drag = 0;*/
        if (ballInPlay)
        {
            if (Mathf.Abs(rb.velocity.x) > (MaxSpeed + speedBonus) || Mathf.Abs(rb.velocity.z) > (MaxSpeed + speedBonus))
                rb.drag = 3;
            else if (Mathf.Abs(rb.velocity.x) < (MinSpeed - 5 + speedBonus) || Mathf.Abs(rb.velocity.z) < (MinSpeed - 5 + speedBonus))
                rb.drag = -3;
            else if (Mathf.Abs(rb.velocity.x) < (MinSpeed + speedBonus) || Mathf.Abs(rb.velocity.z) < (MinSpeed + speedBonus))
                rb.drag = -1;
            else rb.drag = 0;
        }

        if (speedBonus != 0)
        {
            if(currentBoostTime >= boostTime)
            {
                speedBonus = 0;
                currentBoostTime = 0.0f;
            }
            else
            {
                currentBoostTime += Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            PlayerController pc = other.GetComponentInParent<PlayerController>();
            pc.player.LoseHP();
            //Debug.Log("GateControler.OnTrigerEnter(); //hp lose " + pc.player.GetHeath());
            Destroy(gameObject);
        }
    }
}

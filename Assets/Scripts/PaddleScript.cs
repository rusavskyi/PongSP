using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaddleScript : MonoBehaviour
{
    public float paddleSpeed = 1.0f;
    public float paddleTouchSpeed = 10f;
    private Vector3 playerPosition;
    float leftBorder;
    float rightBorder;
    //For ai//
    private bool AI = false;
    float minDist;
    float coordBall;
    Vector3 posOfBall;
    float distanceToBall;
    public Text text;
    private float paddleSpeedAI = 30f;
    private float errorCoord;
    System.Random rand;
    private bool inPlay;
    public bool canMove = true;
    private float originalSize;
    private float originalLeftB;
    private float originalRightB;

    private Vector2 leftFingerPos = Vector2.zero;
    private Vector2 leftFingerLastPos = Vector2.zero;
    private Vector2 leftFingerMovedBy = Vector2.zero;

    public float slideMagnitudeX = 0;
    public float slideMagnitudeY = 0;

    public enum difficultyLevel { EASY, MEDIUM, HARD };
    public difficultyLevel thisDifficultyLevel;

    public bool BoostEffectEnabled { get; set; }
    public int SpeedBonus { get; set; }

    public bool reverseControl { get; set; }

    public bool FissionEffectEnabled { get; set; }
    public GameObject BallPrefab { get; set; }

    public bool SlippingEffectEnabled { get; set; }
    private float slippingTime = 0;
    private float slippingTimeDelay = 0.2f;
    public enum moveState { left, right, stay };
    public moveState moveS;

    public bool ReflectorEffectEnabled { get; set; }
    public int playerId { get; set; }
    public Transform paddleTransform { get; set; }
    private float force = 5;
    GameObject[] balls;

    void Start()
    {
        rand = new System.Random();
        switchError(); //Error for AI
        playerPosition = this.transform.localPosition;
        AI = this.GetComponentInParent<PlayerController>().AI;
        minDist = 1000;

        leftBorder = -5.85f;
        rightBorder = 5.85f;

        reverseControl = false;
        SlippingEffectEnabled = false;

        if (thisDifficultyLevel == difficultyLevel.EASY)
        {
            paddleSpeedAI = 10f;
        }
        else if (thisDifficultyLevel == difficultyLevel.MEDIUM)
        {
            paddleSpeedAI = 20f;
        }
        else if (thisDifficultyLevel == difficultyLevel.HARD)
        {
            paddleSpeedAI = 30f;
        }
    }

    void switchError()
    {
        errorCoord = rand.Next(0, 16) / 10f;
    }

    void Update()
    {
        inPlay = GameObject.Find("Arena").GetComponent<Game>().inPlay;
        if (canMove)
        {
            if (AI == false)
            {
                ///////////////
                //INPUT CONTROL
                ///////////////
                float horizontal = 0;

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
                if (reverseControl)
                {
                    horizontal = -Input.GetAxis("Horizontal");
                }
                else
                {
                    if (SlippingEffectEnabled)
                    {
                        if (moveS == moveState.stay)
                        {
                            horizontal = Input.GetAxis("Horizontal");
                            if (horizontal < 0)
                                moveS = moveState.left;
                            else if (horizontal > 0)
                                moveS = moveState.right;
                            else
                                moveS = moveState.stay;
                        }
                        else
                        {
                            slippingTime += Time.deltaTime;
                            if (moveS == moveState.left)
                            {
                                horizontal = -0.3f;
                            }
                            else
                            {
                                horizontal = 0.3f;
                            }
                            if (slippingTime >= slippingTimeDelay)
                            {
                                slippingTime = 0;
                                horizontal = Input.GetAxis("Horizontal");
                                if (horizontal < 0)
                                    moveS = moveState.left;
                                else if (horizontal > 0)
                                    moveS = moveState.right;
                                else
                                    moveS = moveState.stay;
                            }
                        }
                    }
                    else
                    {
                        horizontal = Input.GetAxis("Horizontal");
                    }
                }

                if (inPlay)
                {
                    float zPosition = transform.localPosition.z + (horizontal * paddleSpeed);
                    playerPosition = new Vector3(playerPosition.x, playerPosition.y, Mathf.Clamp(zPosition, leftBorder, rightBorder));
                    transform.localPosition = playerPosition;
                }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    leftFingerPos = Vector2.zero;
                    leftFingerLastPos = Vector2.zero;
                    leftFingerMovedBy = Vector2.zero;

                    slideMagnitudeX = 0;
                    slideMagnitudeY = 0;

                    // record start position
                    leftFingerPos = touch.position;

                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2
                                                                        // The position delta since last change.
                    leftFingerLastPos = leftFingerPos;
                    leftFingerPos = touch.position;

                    // slide horz
                    slideMagnitudeX = leftFingerMovedBy.x / Screen.width;

                    // slide vert
                    slideMagnitudeY = leftFingerMovedBy.y / Screen.height;

                }

                else if (touch.phase == TouchPhase.Stationary)
                {
                    leftFingerLastPos = leftFingerPos;
                    leftFingerPos = touch.position;

                    slideMagnitudeX = 0;
                    slideMagnitudeY = 0;
                }

                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    slideMagnitudeX = 0;
                    slideMagnitudeY = 0;
                }
            }

            float zPosition = transform.localPosition.z + (slideMagnitudeX * paddleSpeedAI);
            playerPosition = new Vector3(playerPosition.x, playerPosition.y, Mathf.Clamp(zPosition, leftBorder, rightBorder));
            transform.localPosition = playerPosition;
#endif


                //////////////////////
                ///////////////////////



            }
            else
            {
                ////// ________________AI______________ /////////////


                switch (this.GetComponentInParent<PlayerController>().playerID)
                {
                    case 1:
                        _AI_1();
                        break;
                    case 2:
                        _AI_2();
                        break;
                    case 3:
                        _AI_3();
                        break;
                    case 4:
                        _AI_4();
                        break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (ReflectorEffectEnabled)
        {
            do
            {
                balls = GameObject.FindGameObjectsWithTag("Ball");
            }
            while (balls.Length < 1);
            List<GameObject> tmp = new List<GameObject>();
            switch (playerId)
            {
                case 1:
                    {
                        for (int i = 0; i < balls.Length; i++)
                        {
                            //Debug.Log("bi: " + balls[i].transform.position.x + " p: " + paddleTransform.position.x);
                            if (balls[i].transform.position.x <= paddleTransform.position.x + 2.25 &&
                                balls[i].transform.position.x >= paddleTransform.position.x - 2.25)
                            {
                                Debug.Log("s2");
                                float distance = Vector3.Distance(balls[i].transform.position, paddleTransform.position);
                                if (distance < 5)
                                {
                                    balls[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, (distance - 5) * force), ForceMode.Impulse);
                                    Debug.Log("TADA!!!");
                                }
                                Debug.Log("s3");
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < balls.Length; i++)
                        {
                            if (balls[i].transform.position.x <= paddleTransform.position.x - 2.25 &&
                                balls[i].transform.position.x >= paddleTransform.position.x + 2.25)
                            {
                                float distance = Vector3.Distance(balls[i].transform.position, paddleTransform.position);
                                if (distance < 5f)
                                {
                                    balls[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, (5 - distance) * force), ForceMode.Impulse);
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < balls.Length; i++)
                        {
                            if (balls[i].transform.position.z <= paddleTransform.position.z - 2.25 &&
                                balls[i].transform.position.z >= paddleTransform.position.z + 2.25)
                            {
                                float distance = Vector3.Distance(balls[i].transform.position, paddleTransform.position);
                                if (distance < 5)
                                {
                                    balls[i].GetComponent<Rigidbody>().AddForce(new Vector3((distance - 5) * force, 0, 0), ForceMode.Impulse);
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        for (int i = 0; i < balls.Length; i++)
                        {
                            if (balls[i].transform.position.z <= paddleTransform.position.z - 2.25 &&
                                balls[i].transform.position.z >= paddleTransform.position.z + 2.25)
                            {
                                float distance = Vector3.Distance(balls[i].transform.position, paddleTransform.position);
                                if (distance < 5)
                                {
                                    balls[i].GetComponent<Rigidbody>().AddForce(new Vector3((5 - distance) * force, 0, 0), ForceMode.Impulse);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    #region AI methods
    void _AI_1()
    {
        GameObject[] ballsNear = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsNear.Length > 0)
        {
            minDist = 1000;
            foreach (GameObject ball in ballsNear)
            {
                posOfBall = ball.GetComponent<Rigidbody>().transform.position;
                distanceToBall = Vector3.Distance(this.transform.position, posOfBall);
                if (distanceToBall < minDist)
                {
                    minDist = distanceToBall;
                    coordBall = -posOfBall.x;
                }
            }
            if (Mathf.Abs(transform.localPosition.z) != Mathf.Abs(coordBall))
            {
                playerPosition = new Vector3(0, playerPosition.y, Mathf.Clamp(coordBall - 1.5f + errorCoord, leftBorder, rightBorder));
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerPosition, paddleSpeedAI * Time.deltaTime);
            }
        }
    }

    void _AI_2()
    {
        GameObject[] ballsNear = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsNear.Length > 0)
        {
            minDist = 1000;
            foreach (GameObject ball in ballsNear)
            {
                posOfBall = ball.GetComponent<Rigidbody>().transform.position;
                distanceToBall = Vector3.Distance(this.transform.position, posOfBall);
                if (distanceToBall < minDist)
                {
                    minDist = distanceToBall;
                    coordBall = posOfBall.x;
                }
            }
            if (Mathf.Abs(transform.localPosition.z) != Mathf.Abs(coordBall))
            {
                playerPosition = new Vector3(0, playerPosition.y, Mathf.Clamp(coordBall - 1.5f + errorCoord, leftBorder, rightBorder));
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerPosition, paddleSpeedAI * Time.deltaTime);
            }
        }
    }

    void _AI_3()
    {
        GameObject[] ballsNear = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsNear.Length > 0)
        {
            minDist = 1000;
            foreach (GameObject ball in ballsNear)
            {
                posOfBall = ball.GetComponent<Rigidbody>().transform.position;
                distanceToBall = Vector3.Distance(this.transform.position, posOfBall);
                if (distanceToBall < minDist)
                {
                    minDist = distanceToBall;
                    coordBall = -posOfBall.z;
                }
            }
            if (Mathf.Abs(transform.localPosition.z) != Mathf.Abs(coordBall))
            {
                playerPosition = new Vector3(0, playerPosition.y, Mathf.Clamp(coordBall - 1.5f + errorCoord, leftBorder, rightBorder));
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerPosition, paddleSpeedAI * Time.deltaTime);
            }
        }
    }

    void _AI_4()
    {
        GameObject[] ballsNear = GameObject.FindGameObjectsWithTag("Ball");
        if (ballsNear.Length > 0)
        {
            minDist = 1000;
            foreach (GameObject ball in ballsNear)
            {
                posOfBall = ball.GetComponent<Rigidbody>().transform.position;
                distanceToBall = Vector3.Distance(this.transform.position, posOfBall);
                if (distanceToBall < minDist)
                {
                    minDist = distanceToBall;
                    coordBall = posOfBall.z;
                }
            }
            if (Mathf.Abs(transform.localPosition.z) != Mathf.Abs(coordBall))
            {
                playerPosition = new Vector3(0, playerPosition.y, Mathf.Clamp(coordBall - 1.5f + errorCoord, leftBorder, rightBorder));
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, playerPosition, paddleSpeedAI * Time.deltaTime);
            }
        }
    }
    #endregion

    void OnCollisionEnter(Collision other)
    {
        if (BoostEffectEnabled)
        {
            if (other.gameObject.tag.Equals("Ball"))
            {
                other.gameObject.GetComponent<BallScript>().speedBonus = SpeedBonus;
            }
        }
        if (FissionEffectEnabled)
        {
            if (other.gameObject.tag.Equals("Ball"))
            {
                Vector3 tmpPosition = other.transform.position;
                Instantiate(BallPrefab, tmpPosition, new Quaternion());
            }
        }
    }

    // Делаем красиво //

    public enum typeOfResize { squeeze, stretch, original };

    public void changePuddleSize(typeOfResize type)
    {

        if (type == typeOfResize.squeeze)
        {
            originalSize = this.transform.localScale.y;
            originalLeftB = leftBorder;
            originalRightB = rightBorder;
            this.transform.localScale = new Vector3(this.transform.localScale.x, originalSize / 1.5f, this.transform.localScale.z);
            leftBorder = originalLeftB - (originalSize - this.transform.localScale.y) / 2 - 0.3f;
            rightBorder = originalRightB + (originalSize - this.transform.localScale.y) / 2 + 0.3f;
        }
        if (type == typeOfResize.stretch)
        {
            originalSize = this.transform.localScale.y;
            originalLeftB = leftBorder;
            originalRightB = rightBorder;
            this.transform.localScale = new Vector3(this.transform.localScale.x, originalSize * 1.5f, this.transform.localScale.z);
            leftBorder = originalLeftB + (this.transform.localScale.y - originalSize) / 2 + 0.3f;
            rightBorder = originalRightB - (this.transform.localScale.y - originalSize) / 2 - 0.3f;
        }
        if (type == typeOfResize.original)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, originalSize, this.transform.localScale.z);
            leftBorder = originalLeftB;
            rightBorder = originalRightB;
        }
    }
}
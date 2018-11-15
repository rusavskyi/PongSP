using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PongClasses;

public class PlayerController : MonoBehaviour {
    public Player player;
    public int playerID;
    public bool AI = false;

    void Start ()
    {
        player = new Player();
    }

    void Update () {
        switch (playerID)
        {
            case 1:
                GameObject.Find("HealthPanel").GetComponentInChildren<Text>().text = "HP: " + player.GetHeath();
                break;
            case 2:
                GameObject.Find("HealthPanelFront").GetComponentInChildren<Text>().text = "HP: " + player.GetHeath();
                break;
            case 3:
                GameObject.Find("HealthPanelRight").GetComponentInChildren<Text>().text = "HP: " + player.GetHeath();
                break;
            case 4:
                GameObject.Find("HealthPanelLeft").GetComponentInChildren<Text>().text = "HP: " + player.GetHeath();
                break;
            default:
                break;
        }
    }

    public Player GetPlayer()
    {
        return player;
    }
}

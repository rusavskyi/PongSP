using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SingleplayerButton_Click()
    {
        SceneManager.LoadScene("TestArena");
    }

    public void MultiplayerButton_Click()
    {

    }

    public void ExitButton_Click()
    {
        Application.Quit();
    }
}

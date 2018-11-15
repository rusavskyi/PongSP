using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Enable(bool state)
    {
        GetComponent<MeshRenderer>().enabled = state;
        GetComponent<Collider>().enabled = state;
    }
}

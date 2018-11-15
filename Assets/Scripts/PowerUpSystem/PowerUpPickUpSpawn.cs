using UnityEngine;
using System.Collections;

public class PowerUpPickUpSpawn: MonoBehaviour {

    public GameObject[] PickUps;
    public Transform[] SpawnPoints;
    //public GameObject[] EffectPickUpClones;
    public float PickUpSpawnTime;
    public float ScriptDelay;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnEffectPickUp", ScriptDelay, PickUpSpawnTime);
    }

	// Update is called once per frame
	void Update ()
    {
       
    }

    public void SpawnEffectPickUp()
    {
        int SpawnPointIndex = Random.Range(0, SpawnPoints.Length);
        int PickUpIndex = Random.Range(0, PickUps.Length);
        // int PickUpCloneIndex = Random.Range(0, EffectPickUpClones.Length);
        //EffectPickUpClones[PickUpCloneIndex] = 
        Instantiate(PickUps[PickUpIndex], SpawnPoints[SpawnPointIndex].position,
            SpawnPoints[SpawnPointIndex].rotation); //as GameObject;
    }

}

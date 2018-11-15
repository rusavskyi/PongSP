using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace PongClasses
{
    public class BlackoutEffect : Effect
    {
        float time, lastChange;
        GameObject directionalLight;
        GameObject centerSpotlight;
        GameObject[] lights;
        bool isDone = false;

        public BlackoutEffect() : base()
        {
            //shuting down lights
            //Debug.Log("Blackout effect is created!!");
            directionalLight = GameObject.Find("DirectionalLight");
            centerSpotlight = GameObject.Find("CenterSpotlight");
            do
            {
                lights = null;
                lights = GameObject.FindGameObjectsWithTag("Light");
                //Debug.Log("lights.Length = " + lights.Length);
            }
            while (lights.Length != 4);
            time = 0f;

            //shuting down lights

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().intensity = 0f;
            }
            directionalLight.GetComponent<Light>().intensity = 0f;
            centerSpotlight.GetComponent<Light>().intensity = 0f;
        }

        public override void Action(GameObject target)
        {

            if ((time - lastChange) >= 1.5f)
            {
                //shuting down spotlights
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].GetComponent<Light>().intensity = 0f;
                }
                Debug.Log("Blackout effect action 0!!");

                if (time >= 15f)//tuning on lights
                {
                    Debug.Log("Blackout effect action 2!!");

                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].GetComponent<Light>().intensity = 4f;
                    }
                    directionalLight.GetComponent<Light>().intensity = 0.5f;
                    centerSpotlight.GetComponent<Light>().intensity = 1f;
                    isDone = true;
                    //lastChange = time += Time.deltaTime;
                }
                else //random spotlights
                {
                    Debug.Log("Blackout effect action 1!!");
                    lights[Random.Range(0, lights.Length)].GetComponent<Light>().intensity = 4f;
                    lastChange = time += Time.deltaTime;
                }
            }
            time += Time.deltaTime;
            //Debug.Log("Blackout effect action is called!!");
        }

        public override bool IsActive()
        {
            return !isDone;
        }
    }
}
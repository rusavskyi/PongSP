using UnityEngine;
using System.Collections;
using PongClasses;

public class PowerUpPickUpBehavior : MonoBehaviour {

    public float destroyTime = 3.0f;
    //private GameObject[] players;
    // Use this for initialization
    void Start()
    {
        if (gameObject.activeSelf)
        {
            Destroy(gameObject, destroyTime);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        if (other.gameObject.name == "Puddle")
        {
            switch (gameObject.name) 
            {
                case ("BlackOutPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new BlackoutEffect();
                    }
                    break;
                case ("BoostPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new BoostEffect();
                    }
                    break;
                case ("ConfusionPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new ConfusionEffect();
                    }
                    break;
                case ("FissionPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new FissionEffect();
                    }
                    break;
                case ("HealingPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new HealingEffect();
                    }
                    break;
                case ("PaddleSqueezePickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new PaddleSqueezeEffect();
                    }
                    break;
                case ("PaddleStretchPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new PaddleStretchEffect();
                    }
                    break;
                case ("ReflectorPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new ReflectorEffect();
                    }
                    break;
                case ("ShieldPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new ShieldEffect();
                    }
                    break;
                case ("ShockPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new ShockEffect();
                    }
                    break;
                case ("SlippingPickUp(Clone)"):
                    if (other.GetComponentInParent<PlayerController>().player._storedEffect == null)
                    {
                        other.GetComponentInParent<PlayerController>().player._storedEffect = new SlippingEffect();
                    }
                    break;         
                default:
                    
                    break;
            }
            Destroy(gameObject);
        }
    }
}

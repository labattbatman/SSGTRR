using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public GameObject theMachineGun;
    public GameObject theLaser;
    public GameObject theBlackHole;

    public int gunType;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<LevelElement>().height += Mathf.PingPong(Time.timeSinceLevelLoad, 1) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            switch (gunType)
            {
                case 1:
                    hit.gameObject.GetComponent<PlayerController>().theGun = theMachineGun;
                    break;
                case 2:
                    hit.gameObject.GetComponent<PlayerController>().theGun = theLaser;
                    break;
                case 3:
                    hit.gameObject.GetComponent<PlayerController>().theGun = theBlackHole;
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }
}

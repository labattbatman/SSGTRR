using UnityEngine;
using System.Collections;

public abstract class gun : MonoBehaviour {

	public float 	fireRate;
	public Vector3 	direction;
	public float 	moveSpeed;
	
	// Use this for initialization
	void Start () {
		//GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		//foreach(GameObject player in players)
			//Physics.IgnoreCollision(collider, player.collider);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public abstract void fire(Vector3 direction, Vector3 origin);
}

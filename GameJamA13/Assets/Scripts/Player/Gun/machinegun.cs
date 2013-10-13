using UnityEngine;
using System.Collections;

public class machinegun : gun {
	
	private GameObject player;
		
	// Use this for initialization
	void Start () {
		
		fireRate = 0.1f;
		moveSpeed = 25;
		Destroy(gameObject,5);
		player = GameObject.FindGameObjectWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<LevelElement>().position += direction.x * moveSpeed * Time.deltaTime;
		GetComponent<LevelElement>().height += direction.y * moveSpeed * Time.deltaTime;
	}
	
	public override void fire(Vector3 directionFire, Vector3 origin)
	{		
		direction = directionFire.normalized;
		GameObject newBullet = (GameObject)Instantiate(this.gameObject,origin,new Quaternion(0,0,0,0));
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		
		newBullet.GetComponent<LevelElement>().position = player.GetComponent<PlayerController>().PlayerPosition;
		newBullet.GetComponent<LevelElement>().height = player.transform.position.y;
		newBullet.GetComponent<LevelElement>().UpdatePos(player.GetComponent<PlayerController>().PlayerPosition);
	}
		
	void OnTriggerEnter(Collider myCol)
	{
		if (myCol.gameObject.tag == "Enemy")
		{			
			Destroy(myCol.gameObject);
			Destroy(gameObject);	
			
			Player.score += 10;
		}
		
	}
}

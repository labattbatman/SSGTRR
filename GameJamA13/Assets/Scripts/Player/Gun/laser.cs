using UnityEngine;
using System.Collections;

public class laser : gun {
	
	private GameObject player;

	// Use this for initialization
	void Start () {
	
		fireRate = 1f;
		moveSpeed = 20;
		Destroy(gameObject,5);
		//transform.localScale = new Vector3(0.3f,0.3f,0.3f);
		//transform.Rotate(new Vector3(0,0,270));
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
		float rotation = Mathf.Atan(direction.y/direction.x);		
		rotation = rotation * 180 / Mathf.PI;
		GameObject newBullet = (GameObject)Instantiate(this.gameObject,origin,new Quaternion(0,0,0,0));
		newBullet.transform.Rotate(0,0,rotation);
		
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
			Player.score += 10;
		}
	}
}

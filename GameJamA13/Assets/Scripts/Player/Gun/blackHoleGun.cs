using UnityEngine;
using System.Collections;

public class blackHoleGun : gun {
	
	private int frameRate 	= 10;
	private Vector3 offset 	= new Vector3(0,0,4);	
	private int x_size = 5;
	private int y_size = 5;
	private GameObject player;
	
	
	// Use this for initialization
	void Start () {
	
		fireRate = 0.5f;
		moveSpeed = 5;
		
		player = GameObject.FindGameObjectWithTag("Player");
		Destroy(gameObject,3);	
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<LevelElement>().position += direction.x * moveSpeed * Time.deltaTime;
		GetComponent<LevelElement>().height += direction.y * moveSpeed * Time.deltaTime;
		transform.localScale += new Vector3(1.5f,1.5f,1.5f) * Time.deltaTime;
	
		PlayAnim(offset,frameRate);
	}
	
	public override void fire(Vector3 directionFire, Vector3 origin)
	{		
		direction = directionFire.normalized;
		GameObject newBullet = (GameObject)Instantiate(this.gameObject,origin,new Quaternion(0,0,0,0));
		
		
		player = GameObject.FindGameObjectWithTag("Player");
		newBullet.GetComponent<LevelElement>().position = player.GetComponent<PlayerController>().PlayerPosition;
		newBullet.GetComponent<LevelElement>().height = player.transform.position.y;
		newBullet.GetComponent<LevelElement>().UpdatePos(player.GetComponent<PlayerController>().PlayerPosition);
	}
		
	void OnTriggerStay(Collider myCol)
	{
		if (myCol.gameObject.tag == "Enemy" || myCol.gameObject.tag == "EnemyBullet")
		{			
			Vector3 vectorBetweenCol = transform.position - myCol.transform.position;
			myCol.GetComponent<LevelElement>().position += vectorBetweenCol.x * Time.deltaTime * 20;
			myCol.GetComponent<LevelElement>().height += vectorBetweenCol.y * Time.deltaTime * 20;
			
			if( vectorBetweenCol.magnitude < 0.5f)
			{
				Destroy(myCol.gameObject);
				
				if(myCol.gameObject.tag == "Enemy")
				Player.score += 10;
			}
			
		}
	}
	
	public void PlayAnim(Vector3 offset, int frameRate)
	{
		Vector3 newOffset = offset;
		newOffset.x += (int)(Time.timeSinceLevelLoad * frameRate) % offset.z;
		
		newOffset.x = newOffset.x / x_size + 0.001f;
		newOffset.y = newOffset.y / y_size + 0.001f;
		renderer.material.mainTextureOffset = newOffset;	
	}
}

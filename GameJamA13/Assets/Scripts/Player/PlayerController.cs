using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private enum direction{right,left}
	private direction playerDirection;
	
	//Sprite Var
	public Texture playerSprite;
	private int x_size = 10;
	private int y_size = 10;
	private int frameRate = 10;
	
	//anim : vector2(offset_x, offset_y, numframe)
	private Vector3 rightOffset = new Vector3(1,0,6);
	private Vector3 rightJumpOffset = new Vector3(0,1,7);
	private Vector3 rightImmobileOffset = new Vector3(0,0,1);
	
	// Movement Var
	public float MaxSpeed = 5;
	public int MaxJump = 8;
	public int Speed = 5;
	public float PlayerPosition = 0f;
	
	Vector3 VelocityRight = new Vector3 (1,0,0);
	Vector3 VelocityLeft = new Vector3 (-1,0,0);
	Vector3 Jump = new Vector3 (0,1,0);
	
	//Fire Var
	public GameObject theGun;
	private float fireAcc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Physics.Raycast(transform.position,-Vector3.up,0.6f) && rigidbody.velocity.y <= 0.1) 
		{
			if(Input.GetButtonDown("Jump"))
			{				
				rigidbody.AddForce(Jump * MaxJump,ForceMode.VelocityChange);
				collider.isTrigger = true;
				
			}
			
			/*if(Input.GetAxis("Vertical")<0)
			{	
				rigidbody.collider.enabled = false;
			}*/
		}
		
		if(rigidbody.velocity.y <= -0.1f)
		{
			collider.isTrigger = false;

		}
			
		if(Input.GetAxis("Horizontal")>0 && rigidbody.velocity.x < MaxSpeed)
		{
			RaycastHit hit;
			
			if(!Physics.Raycast(transform.position,new Vector3(1f,0f,0f),out hit,0.75f)) 
			{
				
				transform.localScale = new Vector3(1,1,0.1f);
				PlayerPosition += Time.deltaTime * Speed * Input.GetAxis("Horizontal");
				if(collider.isTrigger == false)
					PlayAnim(rightOffset,frameRate);
				else PlayAnim(rightJumpOffset,frameRate);
				
				playerDirection = direction.right;
			}
			else
			{
				if (hit.collider.gameObject.tag != "Wall")
				{
					transform.localScale = new Vector3(1,1,0.1f);
					PlayerPosition += Time.deltaTime * Speed * Input.GetAxis("Horizontal");
					if(collider.isTrigger == false)
						PlayAnim(rightOffset,frameRate);
					else PlayAnim(rightJumpOffset,frameRate);
					
					playerDirection = direction.right;
					
				}
						
						
			}
				
		}
		
		// When player immobile
		if(Input.GetAxis("Horizontal")==0 && playerDirection == direction.right)
			PlayAnim(rightImmobileOffset,frameRate);
		
		if(Input.GetAxis("Horizontal")==0 && playerDirection == direction.left)
		{
			transform.localScale = new Vector3(-1,1,0.1f);
			PlayAnim(rightImmobileOffset,frameRate);
		}
		
		if(Input.GetAxis("Horizontal")<0 && rigidbody.velocity.x < MaxSpeed)
		{	
			RaycastHit hit;
			
			if(!Physics.Raycast(transform.position,new Vector3(-1f,0f,0f),out hit,0.75f))
			{
				transform.localScale = new Vector3(-1,1,0.1f);
				PlayerPosition -= Time.deltaTime * Speed * -Input.GetAxis("Horizontal");
				if(collider.isTrigger == false)
					PlayAnim(rightOffset,frameRate);
				else PlayAnim(rightJumpOffset,frameRate);
				
				playerDirection = direction.left;
			}
			
			else
			{
				if (hit.collider.gameObject.tag != "Wall")
				{
					transform.localScale = new Vector3(-1,1,0.1f);
					PlayerPosition -= Time.deltaTime * Speed * -Input.GetAxis("Horizontal");
					if(collider.isTrigger == false)
						PlayAnim(rightOffset,frameRate);
					else PlayAnim(rightJumpOffset,frameRate);
					
					playerDirection = direction.left;
					
				}
						
						
			}
		}
		
		
		// Fire
		if(Input.GetButton("Fire"))
		{
			Vector3 fireDirection = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
			
			if(fireAcc > theGun.GetComponent<gun>().fireRate)
			{
				// When player immobile
				if (fireDirection.magnitude == 0 && playerDirection == direction.right)
					fireDirection = Vector3.right;
				
				if (fireDirection.magnitude == 0 && playerDirection == direction.left)
					fireDirection = Vector3.left;
				
				theGun.GetComponent<gun>().fire(fireDirection,transform.position + fireDirection.normalized);
				fireAcc = 0;
			}
			else fireAcc += Time.deltaTime;	
		}
		fireAcc += Time.deltaTime;
	
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
			


	
		


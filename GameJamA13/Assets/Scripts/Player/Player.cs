using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int life = 1;
	static public int score = 0;
	public AudioClip playerDieAudio;
	public bool isDead = true;
	private float timerDead = 0f;
	private bool isVisible;
	public GameObject theMachineGun;
	private bool step1;
	private bool step2;
	private int step3 = 3500;

    public AudioClip itemGet;
	
	// Use this for initialization
	IEnumerator Start () {
		
		life 	= 3;
		score 	= 0;
		
		yield return new WaitForSeconds(1);
		playerRespawm();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isDead)
		{
			timerDead += Time.deltaTime;
			if(timerDead > 0.15f)
			{
				if (isVisible)
				{
					this.GetComponent<MeshRenderer>().enabled = false;
					
					isVisible = false;
				}
				else if (timerDead > 0.3f)
				{
					this.GetComponent<MeshRenderer>().enabled = true;
					isVisible = true;
					timerDead = 0;
				}
			}
			
		}
		RaycastHit isLeChat;
		if(Physics.Raycast(transform.position,Vector3.left,out isLeChat,0.6f))
		{
			if(isLeChat.collider.gameObject.tag == "DeathWall")
			{
				life = 0;
				StartCoroutine(playerdie());
			}
		}
		
		if (score >= 1000 && step1 == false)
		{
			life ++;
			step1 = true;
		}
		else if ( score >= 2000 && step2 == false)
		{
			life++;
		 	step2 = true;
		}
		else if (score >= step3)
		{
			life ++;
			step3 += 3500;
		}
		
		if(life == 0)
		{
			Application.LoadLevel("GameOver");
		}
	}
	
	void playerRespawm()
	{
		isDead = false;
		this.GetComponent<MeshRenderer>().enabled = true;
	}
	
	public IEnumerator playerdie()
	{
        audio.volume = 1.0f;
		audio.PlayOneShot(playerDieAudio,1);
		if( life > 0)
		{
			isDead = true;
			life --;
			GetComponent<PlayerController>().PlayerPosition -= 5;
			this.GetComponent<PlayerController>().theGun = theMachineGun;
			yield return new WaitForSeconds(5);
			playerRespawm();
		}
		else Debug.Log("POUR TJRS"); 
	}
	
	
	void OnTriggerEnter(Collider myCol)
	{
		if(myCol.gameObject.tag == "Enemy" && !isDead)
		{
			StartCoroutine(playerdie());
		}
		
		if(myCol.gameObject.tag == "EnemyBullet" && !isDead)
		{
			if(myCol.GetComponent<EnemyBullet>().timeLife > 0.1f)
				StartCoroutine(playerdie());
		}
		
		
		if(myCol.gameObject.tag == "DeathWall")
		{
			life = 0;
			StartCoroutine(playerdie());
		}

        if (myCol.gameObject.tag == "Powerup")
        {
            audio.volume = 0.5f;
            audio.PlayOneShot(itemGet, 1);
        }
	}
	
}

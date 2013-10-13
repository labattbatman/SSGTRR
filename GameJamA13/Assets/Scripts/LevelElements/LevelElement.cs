using UnityEngine;
using System.Collections;

public class LevelElement : MonoBehaviour {
	
	public float position = 0f;
	public float height = 0f;//can be random or set before
	public float depth = 0f;
	public float generationRate = 0.6f;
	public float generationCooldown = 5f;
	
	private GameObject player;
	private GameObject levelGenerator;
	

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator");
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePos(player.GetComponent<PlayerController>().PlayerPosition);
		
		if (position < (levelGenerator.GetComponent<LevelGenerator>().wallOfDeathPos - 30f))
		{
			//die!
			Destroy(this.gameObject);
		}
	}
	
	
	//set the element position based on middle screen pos (for cam movement)
	public void UpdatePos(float pos)
	{
		this.transform.position = new Vector3(position - pos, height, depth);
	}
}

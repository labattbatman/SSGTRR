using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//todo:
//wall of death

public class LevelGenerator : MonoBehaviour {
	
	public GameObject player;
	public float playerPos = 0f; //metre la pose du script joueur dedans!
	private float playerFartestPos = 0f;
	public float wallOfDeathPos = 0f; //faut je fasse dekoi avec ca
	
	public float levelRightEdge = 10f;
	//liste d'objets possible
	public GameObject[] elementTypes;
	//cooldown timers (sort of)
	public float[] lastElementPos;// = new float[3]{10f, 10f, 10f};
	
	public GameObject[] mobTypes;
	
	//stuff for random stuff
	public int seed = 1234;
	private float totalRate = 0f;
	private float totalRateMob = 0f;
	//liste d'objets spawné dans le level
	public List<GameObject> elementList;
	
	
	//background stuff
	public GameObject starPrefab;
	public List<GameObject> starList;
	
	public GameObject deadlyThing;
	public float deadlyThingSpeed = 5.0f;
	public float deadlyThingMaxSpeed = 10f;

	
	// Use this for initialization
	void Start () {
		//a changer
		if (seed == 0f)
			seed = System.DateTime.Now.Millisecond;
		
		Random.seed = this.seed;
		
		
		elementList = new List<GameObject>();
		
		lastElementPos = new float[elementTypes.Length];
		for (int i=0; i<lastElementPos.Length; i++)
		{
			lastElementPos[i] = elementTypes[i].GetComponent<LevelElement>().generationCooldown;
			//set total random
			totalRate += elementTypes[i].GetComponent<LevelElement>().generationRate;
			
		}
		for (int i=0; i<mobTypes.Length; i++)
		{
			//set total random
			totalRateMob += mobTypes[i].GetComponent<LevelElement>().generationRate;
			
		}
		
		//background
		starList = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//update player pos
		playerPos = player.GetComponent<PlayerController>().PlayerPosition;
		
		if (playerPos > playerFartestPos)
			playerFartestPos = playerPos;
		
		
		//generate stuff in front of the player
		while (playerPos + 30f > levelRightEdge)
		{
			levelRightEdge += 1f;
			GameObject newFeature = CreateLevelFeature(levelRightEdge);
			
			if (newFeature != null)
				elementList.Add(newFeature);
			
			
			//mob
			GameObject newMob = CreateMob(levelRightEdge);
			
			if (newMob != null)
				elementList.Add(newMob);
			
			
			//stars
			float starHeight = Random.Range(-5f, 15f);
			GameObject newStar = GameObject.Instantiate(starPrefab, new Vector3(levelRightEdge, starHeight, 1f), Quaternion.identity) as GameObject;
			newStar.GetComponent<LevelElement>().position = levelRightEdge;
			newStar.GetComponent<LevelElement>().height = starHeight;
			newStar.GetComponent<LevelElement>().depth = 1f;
			elementList.Add(newStar);
		}
		
		//update current stuff position
		//UpdateLevelPosition(playerPos);
		
		wallOfDeathPos += deadlyThingSpeed * Time.deltaTime;
		deadlyThing.GetComponent<LevelElement>().position = wallOfDeathPos;
		
		WallOfDeath(wallOfDeathPos - 30f);
		
		
		
		//speed up long cat
		if ( (deadlyThingSpeed < deadlyThingMaxSpeed) && (playerFartestPos >= 10f))
			deadlyThingSpeed = deadlyThingMaxSpeed * playerFartestPos/500f + 5f;
	}
	
	void UpdateLevelPosition(float pos)
	{
		//déplace chaque objet par rapport au millieu du screen (pos)
		for (int i=0; i<elementList.Count; i++)
		{
			if (elementList[i] == null)
			{
				elementList.RemoveAt(i);
				i--;
				continue;
			}
			elementList[i].GetComponent<LevelElement>().UpdatePos(pos);
		}
	}
	
	//kill everything behind that wall
	void WallOfDeath(float pos)
	{
		for (int i=0; i<elementList.Count; i++)
		{
			if (elementList[i] == null)
			{
				elementList.RemoveAt(i);
				i--;
				continue;
			}
			if (elementList[i].GetComponent<LevelElement>().position < pos)
			{
				GameObject temp = elementList[i];
				elementList.RemoveAt(i);
				Destroy(temp);
				
				//thing got removed, so....
				i--;
			}
		}
	}
	
	
	
	//functions for my random stuff (it shines!)
	GameObject CreateLevelFeature(float pos)
	{

		if (elementTypes.Length == 0)
			return null;
		
		//random!
		float randNum = Random.Range(0f, totalRate+1f);
		
		//check which one we got (here, it shines)
		float rateAddition = 0f;
		for (int i=0; i<lastElementPos.Length; i++)
		{
			rateAddition += elementTypes[i].GetComponent<LevelElement>().generationRate;
			
			if (rateAddition > randNum)
			{
				if (lastElementPos[i] > pos - elementTypes[i].GetComponent<LevelElement>().generationCooldown)
					return null;
				
				//set cooldown (sort of)
				lastElementPos[i] = pos;
				
				//found it! return a copy
				GameObject newObject = GameObject.Instantiate(elementTypes[i], new Vector3(30f, 0f, -1f), Quaternion.identity) as GameObject;
				newObject.GetComponent<LevelElement>().position = pos;
				//newObject.GetComponent<LevelElement>().height = Random.Range(0f, 10f);
				//newObject.GetComponent<LevelElement>().depth = -1f;
				//newObject.GetComponent<LevelElement>().UpdatePos(pos);
				
				return newObject;
			}
		}
		
		return null;
		
	}
	
	GameObject CreateMob(float pos)
	{
		if (mobTypes.Length == 0)
			return null;
		
		//random!
		float randNum = Random.Range(0f, totalRateMob+1f);
		
		//check which one we got (here, it shines)
		float rateAddition = 0f;
		for (int i=0; i<mobTypes.Length; i++)
		{
			rateAddition += mobTypes[i].GetComponent<LevelElement>().generationRate;
			
			if (rateAddition > randNum)
			{				
				//found it! return a copy
				GameObject newMob = GameObject.Instantiate(mobTypes[i], new Vector3(30f, 0f, -1f), Quaternion.identity) as GameObject;
				newMob.GetComponent<LevelElement>().position = pos;
				//newObject.GetComponent<LevelElement>().height = Random.Range(0f, 10f);
				//newObject.GetComponent<LevelElement>().depth = -1f;
				//newObject.GetComponent<LevelElement>().UpdatePos(pos);
				
				return newMob;
			}
		}
		
		return null;
	}
}

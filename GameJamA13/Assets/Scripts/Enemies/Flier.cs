using UnityEngine;
using System.Collections;

public class Flier : MonoBehaviour 
{
    Vector3 originalPosition;
    public float speed = 5;
	
	private float randomWave;

	// Use this for initialization
	void Start () 
    {
        originalPosition = transform.position;
        float rand = Random.Range(0, 3) * 3 + 3.2f;
        GetComponent<LevelElement>().height = rand;
		
		randomWave = Random.Range(0f, 2f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //X movement
        GetComponent<LevelElement>().position -= speed * Time.deltaTime;

        //Y wave
        GetComponent<LevelElement>().height += Mathf.Sin((Time.timeSinceLevelLoad+randomWave) * 5f)* 5f * Time.deltaTime;
	}
}

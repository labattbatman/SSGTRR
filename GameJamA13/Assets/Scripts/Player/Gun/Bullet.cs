using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	
	public Vector2 velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<LevelElement>().position += velocity.x * Time.deltaTime;
		this.GetComponent<LevelElement>().height += velocity.y * Time.deltaTime;
	}
}

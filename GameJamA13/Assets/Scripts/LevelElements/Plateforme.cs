using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {
	
	public float platformLength = 5f;

	// Use this for initialization
	void Start () {
		//set height
		int platformHeight = Random.Range(1, 3);
		this.GetComponent<LevelElement>().height = 3 * platformHeight;
		
		this.GetComponent<LevelElement>().depth = -2f;
		
		platformLength = Random.Range(5, 12);
		this.transform.localScale = new Vector3(platformLength, 1f, 1f);
		//this.renderer.material.mainTextureScale = new Vector2(platformLength, 1f);
		this.renderer.material.mainTextureScale = new Vector2(1f, 0.5f);
		this.renderer.material.mainTextureOffset = new Vector2(0f, 0.25f);
		this.GetComponent<BoxCollider>().size = new Vector3(1f, 0.5f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

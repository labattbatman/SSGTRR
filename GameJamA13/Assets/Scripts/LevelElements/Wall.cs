using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//set height
		int wallHeight = Random.Range(0, 3);
		this.GetComponent<LevelElement>().height = 3 * wallHeight + 1.5f;
		
		this.GetComponent<LevelElement>().depth = -1f;
		
		this.transform.localScale = new Vector3(1f, 3f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class BackgroundAnim : MonoBehaviour {
	
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.renderer.material.mainTextureOffset = new Vector2(player.GetComponent<PlayerController>().PlayerPosition/20f, 1f);
	}
}

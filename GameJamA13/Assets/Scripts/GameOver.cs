using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	public GUIStyle scoreGUIStyle;

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("space"))
		{
			 Application.LoadLevel("MainLevel");
		}
	
	}
	
	void OnGUI()
	{
		//shift ver la gauche a tout les x10
		
			scoreGUIStyle = new GUIStyle(GUI.skin.label);
			scoreGUIStyle.fontSize = 100;
		
			if(Player.score == 0)
			{
				GUI.Label(new Rect((Screen.width/2-25),(Screen.height/3)*2 -50,10000,10000), Player.score.ToString(),scoreGUIStyle);
			}
			else if(Player.score >= 10 && Player.score < 100)
			{
				GUI.Label(new Rect((Screen.width/2-55),(Screen.height/3)*2 -50,10000,10000), Player.score.ToString(),scoreGUIStyle);
			}
			else if(Player.score >= 100 && Player.score < 1000)
			{
				GUI.Label(new Rect((Screen.width/2)-85,(Screen.height/3)*2 -50,10000,10000), Player.score.ToString(),scoreGUIStyle);
			}
			else
			{
				GUI.Label(new Rect((Screen.width/2)-115,(Screen.height/3)*2 -50,10000,10000), Player.score.ToString(),scoreGUIStyle);
			}
	}
}

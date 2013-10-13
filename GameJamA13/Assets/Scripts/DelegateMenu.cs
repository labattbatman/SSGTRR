using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DelegateMenu : MonoBehaviour {

	private delegate void MenuDelegate();
	private MenuDelegate menuFunction;
	
	public Texture2D nameText;
	public Texture2D playText;
	public Texture2D howToPlayText;
	public Texture2D creditText;
	public Texture2D quitText;
	public Texture2D spaceMarine;
	public Texture2D howToPlayPic;
	public Texture2D menuPic;
	
	private float spaceMarinePos;
	private int spaceMarineOptionSelected;
	
	public AudioClip changeAudio;
	public AudioClip quitAudio;
	public AudioClip creditAudio;
	public AudioClip howToPlayAudio;
	public AudioClip creditAudioBG;

	
	public Texture2D DomPic;
	public Texture2D DomName;
	public Texture2D FredPic;
	public Texture2D FredName;
	public Texture2D PhilPic;
	public Texture2D PhilName;
	public Texture2D AlexPic;
	public Texture2D AlexName;
	
	// Use this for initialization
	void Start () {
	
		menuFunction 	= mainMenu;
		spaceMarinePos 	= Screen.height * 0.35f;
		spaceMarineOptionSelected = 1;
	}
	
	
	void OnGUI()
	{
		menuFunction();	
	}
	
	void mainMenu()
	{
		navigationMainMenu();
		GUI.Label(new Rect( Screen.width * 0.35f, spaceMarinePos,150,50), spaceMarine );
		GUI.Label(new Rect( Screen.width * 0.2f, Screen.height * 0.15f,1000,1000), nameText );
		GUI.Label(new Rect( Screen.width * 0.4f, Screen.height * 0.35f,150,50), playText );
		GUI.Label(new Rect( Screen.width * 0.4f, Screen.height * 0.45f,150,50), howToPlayText );
		GUI.Label(new Rect( Screen.width * 0.4f, Screen.height * 0.55f,150,50), creditText );
		GUI.Label(new Rect( Screen.width * 0.4f, Screen.height * 0.65f,150,50), quitText );
	}
	
	void creditMenu()
	{
		
		GUI.Label(new Rect( Screen.width * 0.15f, Screen.height * 0.25f,250,250), DomPic );
		GUI.Label(new Rect( Screen.width * 0.10f, Screen.height * 0.65f,250,250), DomName );
		GUI.Label(new Rect( Screen.width * 0.3f, Screen.height * 0.25f,250,250), FredPic );
		GUI.Label(new Rect( Screen.width * 0.3f, Screen.height * 0.65f,250,250), FredName );
		GUI.Label(new Rect( Screen.width * 0.45f, Screen.height * 0.25f,250,250), PhilPic );
		GUI.Label(new Rect( Screen.width * 0.5f, Screen.height * 0.65f,250,250), PhilName );
		GUI.Label(new Rect( Screen.width * 0.58f, Screen.height * 0.18f,325,325), AlexPic );
		GUI.Label(new Rect( Screen.width * 0.7f, Screen.height * 0.65f,250,250), AlexName );
		
		GUI.Label(new Rect( Screen.width * 0.32f, Screen.height * 0.85f,150,50), spaceMarine );
		GUI.Label(new Rect( Screen.width * 0.35f, Screen.height * 0.85f,150,50), menuPic );
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Space)
			menuFunction 	= mainMenu;
	}
	
	void howToPlayMenu()
	{
		GUI.Label(new Rect( Screen.width * 0.05f, Screen.height * 0.05f,500,500), howToPlayPic );
		
		GUI.Label(new Rect( Screen.width * 0.32f, Screen.height * 0.85f,150,50), spaceMarine );
		GUI.Label(new Rect( Screen.width * 0.35f, Screen.height * 0.85f,150,50), menuPic );
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Space)
			menuFunction 	= mainMenu;
	}
	
	void changeSpaceMarinePos(int change)
	{
		spaceMarineOptionSelected += change;
		
		if(spaceMarineOptionSelected > 4)
			spaceMarineOptionSelected = 1;
		if(spaceMarineOptionSelected < 1)
			spaceMarineOptionSelected = 4;
		
		switch(spaceMarineOptionSelected)
		{
		case 1: spaceMarinePos = Screen.height * 0.35f;
				break;
		case 2: spaceMarinePos = Screen.height * 0.45f;
				break;
		case 3: spaceMarinePos = Screen.height * 0.55f;
				break;
		case 4: spaceMarinePos = Screen.height * 0.65f;
				break;
		default: spaceMarinePos = Screen.height * 0.35f;
				break;
		}
	}
	
	void navigationMainMenu()
	{
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.DownArrow)
		{
			changeSpaceMarinePos(1);
			audio.PlayOneShot(changeAudio,1);
		}
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.UpArrow)
		{
			changeSpaceMarinePos(-1);
			audio.PlayOneShot(changeAudio,1);
		}
		
		if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Space)
		{
			switch(spaceMarineOptionSelected)
			{
			case 1: 
					Application.LoadLevel("MainLevel");
					break;
			case 2: audio.PlayOneShot(howToPlayAudio,10);
					menuFunction 	= howToPlayMenu;
					break;
			case 3: audio.PlayOneShot(creditAudio,10);
					menuFunction 	= creditMenu;
					break;
			case 4: audio.PlayOneShot(quitAudio,10); 
				 	Application.Quit();
					break;

			}
			
		}
	}
	
}

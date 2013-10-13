using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public Texture lifeTexture;
	private int nbPlayer;
	
	//ToTweak
	private int playerOneLifePosInit = 120;
	private int playerTwoLifePosInit = 175;
	private int distBetweenLife = 35;
	private int uiHeight = 25;
	public GUIStyle scoreGUIStyle;
	
	
	// PlayerOne
	private GameObject playerOne;
	private float  playerOneLifePos;
	
	int  playerOneNbLife;
	int  playerOneScore;
		
	//PlayerTwo
	private GameObject playerTwo;
	private float  playerTwoLifePos;
	
	int  playerTwoNbLife;
	int  playerTwoScore;
			
	// Use this for initialization
	void Start () {
	
		nbPlayer 	= GameObject.Find("MainGame").GetComponent<MainGame>().nbOfPlayer;
		playerOne 	= GameObject.Find("Player");
		playerTwo 	= GameObject.Find("PlayerTwo");
	}
	

	void OnGUI()
		
	{	
		//PlayerOne
		
		playerOneNbLife = playerOne.GetComponent<Player>().life;
	    playerOneScore = Player.score;
		
		// Score
		GUI.Label(new Rect(0,uiHeight,150,55),playerOneScore.ToString(),scoreGUIStyle);
		
		// Life UI
		if(playerOneNbLife > 5)
			playerOneNbLife = 5;
		playerOneLifePos = playerOneLifePosInit;
		for(int x = 0; x < playerOneNbLife; x++)
		{
			GUI.Label(new Rect(playerOneLifePos,uiHeight,35,35),lifeTexture);
			playerOneLifePos += distBetweenLife;
		}

		// PlayerTwo
		if(nbPlayer >=2)
		{
			playerTwoNbLife = playerTwo.GetComponent<Player>().life;
	  		playerTwoScore = Player.score;
			
			// Score
			GUI.Label(new Rect(Screen.width-125,uiHeight,150,55),playerTwoScore.ToString(),scoreGUIStyle);
			
			// Life UI
			if(playerTwoNbLife > 5)
				playerTwoNbLife = 5;
			playerTwoLifePos = playerTwoLifePosInit;
			for(int x = 0; x < playerTwoNbLife; x++)
			{
				GUI.Label(new Rect(Screen.width-playerTwoLifePos,uiHeight,35,35),lifeTexture);
				playerTwoLifePos += distBetweenLife;
			}				
		}
	}

}

using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour 
{
	public float speed = 5;
    private string Direction = "left";
	
	//Sprite Var
	public Texture playerSprite;
	private int x_size = 10;
	private int y_size = 5;
	private int frameRate = 10;
	
	//anim : vector2(offset_x, offset_y, numframe)
	private Vector3 rightOffset = new Vector3(0,0,9);


	
	// Use this for initialization
	void Start () 
    {
        float rand = Random.Range(0, 3) * 3 + 1.2f;
        GetComponent<LevelElement>().height = rand;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //check for holes
        if (!Physics.Raycast(transform.position, new Vector3(1.0f, -1.0f, 0.0f), 1.0f) || !Physics.Raycast(transform.position, new Vector3(-1.0f, -1.0f, 0.0f), 1.0f))
        {
            DirectionChange();
        }

        //check for colliding sprites
        if (Physics.Raycast(transform.position, new Vector3(0.0f, 0.0f, 0.0f), 0.1f) || Physics.Raycast(transform.position, new Vector3(-0.5f, 0.0f, 0.0f), 0.5f))
        {
            DirectionChange();
        }

        if (Direction == "left")
        {
            transform.localScale = new Vector3(-1,1,0.1f);
			GetComponent<LevelElement>().position -= speed * Time.deltaTime;
			PlayAnim(rightOffset,frameRate);
        }
        else 
        {
            transform.localScale = new Vector3(1,1,0.1f);
			GetComponent<LevelElement>().position += speed * Time.deltaTime;
			PlayAnim(rightOffset,frameRate);
        }
		
		if (!Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), 0.55f))
			GetComponent<LevelElement>().height -= speed * Time.deltaTime;
	
	}

    void DirectionChange()
    {
        if (Direction == "left")
        {
            Direction = "right";
        }
        else
        {
            Direction = "left";
        }
    }
	
		public void PlayAnim(Vector3 offset, int frameRate)
	{
		Vector3 newOffset = offset;
		newOffset.x += (int)(Time.timeSinceLevelLoad * frameRate) % offset.z;
		
		newOffset.x = newOffset.x / x_size + 0.001f;
		newOffset.y = newOffset.y / y_size + 0.001f;
		renderer.material.mainTextureOffset = newOffset;	
	}
}

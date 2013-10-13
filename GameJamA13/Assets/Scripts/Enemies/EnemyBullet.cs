using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour 
{
    public Vector3 direction;
    public float moveSpeed;
	public float timeLife;
	
	private GameObject player;

	// Use this for initialization
	void Start () 
    {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
       timeLife += Time.deltaTime;
		GetComponent<LevelElement>().position += direction.x * moveSpeed * Time.deltaTime;
        GetComponent<LevelElement>().height += direction.y * moveSpeed * Time.deltaTime;

        if (!this.renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
	}

    public void fired(Vector3 directionFire, Vector3 origin, Vector3 position)
    {
        direction = directionFire.normalized;

        GameObject newBullet = (GameObject)Instantiate(this.gameObject, origin, Quaternion.identity);

        newBullet.GetComponent<LevelElement>().position = position.x;
        newBullet.GetComponent<LevelElement>().height = position.y;
        newBullet.GetComponent<LevelElement>().UpdatePos(position.x);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player" && player.GetComponent<Player>().isDead == false)
        {
			
			
            Destroy(this.gameObject);
			/*if(timeLife > 0.1f)
			{
				StartCoroutine(hit.GetComponent<Player>().playerdie());
			}*/
        }
    }
}

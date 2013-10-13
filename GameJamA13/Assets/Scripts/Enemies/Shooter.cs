using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{

    //public Rigidbody bulletPrefab;
    public GameObject bulletPrefab;
    public float ShootFrequency = 5.0f;
    private float shootCooldown = 0.0f;
    public bool useSpreadshot = false;

    private Vector3 enemyPosition;
    private Vector3 direction;
	
	public float speed = 5f;



    // Use this for initialization
    void Start()
    {
        float rand = Random.Range(0, 3) * 3 + 1.2f;
        GetComponent<LevelElement>().height = rand;

        //shootCooldown = ShootFrequency;
		shootCooldown = 2.9f;
		
    }



    // Update is called once per frame
    void Update()
    {
        if (shootCooldown > ShootFrequency)
        {
            shootCooldown = 0.0f;

            enemyPosition.x = GetComponent<LevelElement>().position;
            enemyPosition.y = GetComponent<LevelElement>().height;

            direction = Vector3.left;
            bulletPrefab.GetComponent<EnemyBullet>().fired(direction, transform.position + direction.normalized, enemyPosition);

            if (useSpreadshot)
            {
                direction = new Vector3(-1.0f, -1.0f, 0f);
                bulletPrefab.GetComponent<EnemyBullet>().fired(direction, transform.position + direction.normalized, enemyPosition);

                direction = new Vector3(-1.0f, 1.0f, 0f);
                bulletPrefab.GetComponent<EnemyBullet>().fired(direction, transform.position + direction.normalized, enemyPosition);
            }

        }

        if (this.renderer.isVisible)
        {
            shootCooldown += Time.deltaTime;
        }

        if (!Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), 0.55f))
            GetComponent<LevelElement>().height -= speed * Time.deltaTime;
    }
}


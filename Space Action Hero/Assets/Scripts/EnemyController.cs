using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;

    public Vector2 startDirection;

    public bool shouldChangeDirection;
    public float changeDirectionXPoint;
    public Vector2 changeDirection;

    public GameObject shotToFire;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    public bool canShoot;
    private bool allowShoot;

    public int currentHealth;
    public GameObject deathEffect, deathEffect1;

    public int scoreValue = 100;

    public GameObject[] powerUps;
    public int dropSuccessRate;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    void Update()
    {
        //transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        if (!shouldChangeDirection)
        {

            transform.position += new Vector3(startDirection.x * moveSpeed * Time.deltaTime, startDirection.y * moveSpeed * Time.deltaTime, 0f);
        }

        else
        {
            if (transform.position.x > changeDirectionXPoint)
            {
                transform.position += new Vector3(startDirection.x * moveSpeed * Time.deltaTime, startDirection.y * moveSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position += new Vector3(changeDirection.x * moveSpeed * Time.deltaTime, changeDirection.y * moveSpeed * Time.deltaTime, 0f);
            }
        }

        if (allowShoot)
        {



            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {

                shotCounter = timeBetweenShots;
                Instantiate(shotToFire, firePoint.position, firePoint.rotation);
            }
        }

    }

    public void HurtEnemy()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {

            GameManager.instance.AddScore(scoreValue);

            int randomChance = Random.Range(0, 100);
            if(randomChance < dropSuccessRate)
            {
                int randomPick = Random.Range(0, powerUps.Length);
                Instantiate(powerUps[randomPick], transform.position, transform.rotation);
            }

            

            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(deathEffect1, transform.position, transform.rotation);
        }


    }





    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
        if (canShoot)
        {
            allowShoot = true;
        }
    }
}

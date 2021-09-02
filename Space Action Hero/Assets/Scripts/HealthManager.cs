using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;


    public int currentHealth;
    public int maxHealth;


    public GameObject deathEffect;

    public float invincibleLenght = 2f;
    private float invincCounter;
    public SpriteRenderer theSR;

    public int shieldPwr;
    public int shieldMaxPwr = 2;
    public GameObject theShield;



    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.healtBar.maxValue = maxHealth;
        UIManager.instance.healtBar.value = currentHealth;
        UIManager.instance.shieldBar.maxValue = shieldMaxPwr;
        UIManager.instance.shieldBar.value = shieldPwr;

    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter >= 0)
        {
            invincCounter -= Time.deltaTime;

            if (invincCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }

    }

    public void HurtPlayer()
    {
        if (invincCounter <= 0)
        {

            if (theShield.activeInHierarchy)
            {
                shieldPwr--;

                if (shieldPwr <= 0)
                {
                    theShield.SetActive(false);
                }
                UIManager.instance.shieldBar.value = shieldPwr;

            }
            else
            {
                currentHealth--;

                UIManager.instance.healtBar.value = currentHealth;

                if (currentHealth <= 0)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                    gameObject.SetActive(false);

                    GameManager.instance.KillPlayer();

                    WaveManager.instance.canSpawnWaves = false;
                }
                PlayerController.instance.doubleShotActive = false;
            }

        }
    }
    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        UIManager.instance.healtBar.value = currentHealth;

        invincCounter = invincibleLenght;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
    }


    public void ActivateShield()
    {
        theShield.SetActive(true);
        shieldPwr = shieldMaxPwr;


        UIManager.instance.shieldBar.value = shieldPwr;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;
    public bool isBoost;
    public bool isDoubleShot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            if (isShield)
            {
                HealthManager.instance.ActivateShield();
            }

            if (isBoost)
            {
                PlayerController.instance.ActivateSpeedBoost();
            }
            if (isDoubleShot)
            {
                PlayerController.instance.doubleShotActive = true;
            }
        }
    }

}

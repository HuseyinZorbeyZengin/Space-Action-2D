using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRB;

    public Transform bottomLeftLimit, topRightLimit;

    public Transform shotPoint;
    public GameObject shot;



    public float timeBetweenShots = .1f;
    private float shotCounter;

    private float normalSpeed;
    public float boostSpeed;
    public float boostLenght;
    private float boostCounter;

    public bool doubleShotActive;
    public float doubleShotOffset;

    public bool StopMovement;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        normalSpeed = moveSpeed;
    }


    void Update()
    {
        if (!StopMovement)
        {



            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.position.x, topRightLimit.position.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.position.y, topRightLimit.position.y), transform.position.z);

            //Copy of shots
            if (Input.GetButtonDown("Fire1"))
            {
                if (!doubleShotActive)
                {
                    Instantiate(shot, shotPoint.position, shotPoint.rotation);
                }
                else
                {
                    Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                    Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                }

                shotCounter = timeBetweenShots;
            }

            if (Input.GetButton("Fire1"))
            {
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    if (!doubleShotActive)
                    {
                        Instantiate(shot, shotPoint.position, shotPoint.rotation);
                    }
                    else
                    {
                        Instantiate(shot, shotPoint.position + new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                        Instantiate(shot, shotPoint.position - new Vector3(0f, doubleShotOffset, 0f), shotPoint.rotation);
                    }
                    shotCounter = timeBetweenShots;
                }
            }
            if (boostCounter > 0)
            {
                boostCounter -= Time.deltaTime;
                if (boostCounter <= 0)
                {
                    moveSpeed = normalSpeed;
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    public void ActivateSpeedBoost()
    {
        boostCounter = boostLenght;
        moveSpeed = boostSpeed;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Object Variables")]
    public Rigidbody2D theRB;
    public Animator anim;

    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpForce;
    private float movX;
    public bool canDoubleJump;
    public float dashTime, dashSpeed, timeBetweenDashes;
    private float dashCounter, dashWaitCounter;
    [HideInInspector] public bool canMove;

    [Header("GroundCheck Variables")]
    public LayerMask whatIsGround;
    public bool isGrounded, isBelowGround;
    public Transform groundCheckPoint, belowGroundPoint;

    [Header("Ball Mode Variables")]
    public float waitToSwitch;
    private float switchCounter;
    public GameObject standingObject, ballObject;
    public Animator ballAnim;

    [Header("Ball Mode Variables")]
    public GameObject theBomb;
    public Transform bombPoint;
    public float timeBetweenBombs;
    private float bombCounter;

    [Header("Shooting Variables")]
    public BulletController theBullet;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotCounter;
    public int currentAmmo = 15, currentBombAmmo = 10;

    [Header("After Image Effect Variables")]
    public Color afterImageColor;
    public float afterImageLifeTime, timeBetweenImages;
    private float afterImageCounter;
    public SpriteRenderer theSR, afterImage;

    void Awake()
    {
        //Set the instance equal to this gameobject
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //Get Components
        theRB = GetComponent<Rigidbody2D>();

        //Make the Player Able to Move at Start
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            WorldChecks();

            Inputs();

            Countdowns();
        }

        Animations();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (dashCounter <= 0)
            {
                //Move the rigidbody on the x-axis depending on the players input
                theRB.velocity = new Vector2(movX, theRB.velocity.y);

                //Call flip function to flip the players local scale depending on the rigidbodys velocity on the x-axis
                Flip();
            }
            else if (dashCounter > 0)
            {
                //Move the rigidbody on the x-axis depending on the dashSpeed * the local scale of the player on the x-axis
                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, 0f);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    void WorldChecks()
    {
        //Detecting if the player is grounded using Unity's built in physics and layer masks
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        isBelowGround = Physics2D.OverlapCircle(belowGroundPoint.position, .2f, whatIsGround);
    }

    void Inputs()
    {
        //Get inputs for movement
        movX = (Input.GetAxisRaw("Horizontal") * moveSpeed);

        //Get inputs for jump and double jup and perform it
        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            if (isGrounded)
                canDoubleJump = true;
            else
                canDoubleJump = false;

            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            AudioManager.instance.PlaySFXAdjusted(1);

        }

        //Get inputs for dashing and perform it
        if (Input.GetButtonDown("Dash") && dashWaitCounter <= 0 && standingObject.activeSelf)
        {
            dashCounter = dashTime;
            AudioManager.instance.PlaySFXAdjusted(0);
            ShowAfterImage();
            dashWaitCounter = timeBetweenDashes;
        }

        //Get inputs for shooting and perform it
        if ((Input.GetButtonDown("Shoot") || Input.GetAxis("ShootingTrigger") > 0.5f))
        {
            if (standingObject.activeSelf && shotCounter <= 0 && currentAmmo > 0)
            {
                //Create a bullet and base its move direction of the players local scale
                Instantiate(theBullet, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                anim.SetTrigger("shotFired");
                shotCounter = timeBetweenShots;

                currentAmmo--;
                UIController.instance.UpdateAmmoUI();
            }
            else if (ballObject.activeSelf && bombCounter <= 0 && currentBombAmmo > 0)
            {
                Instantiate(theBomb, bombPoint.position, bombPoint.rotation);
                bombCounter = timeBetweenBombs;

                currentBombAmmo--;
                UIController.instance.UpdateBombUI();
            }
        }

        if (!ballObject.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                switchCounter -= Time.deltaTime;

                if (switchCounter <= 0)
                {
                    ballObject.SetActive(true);
                    standingObject.SetActive(false);
                }
            }
            else
                switchCounter = waitToSwitch;
        }
        else if (!standingObject.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") > .9f && !isBelowGround)
            {
                switchCounter -= Time.deltaTime;

                if (switchCounter <= 0)
                {
                    ballObject.SetActive(false);
                    standingObject.SetActive(true);
                }
            }
            else
                switchCounter = waitToSwitch;
        }
    }

    void Flip()
    {
        //If player is moving left than flip the players local scale to -1
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //If player is moving right than flip the players local scale to 1
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    void Countdowns()
    {
        //Make the shot counter decrement
        if (shotCounter > 0)
            shotCounter -= Time.deltaTime;

        if (bombCounter > 0)
            bombCounter -= Time.deltaTime;

        if (dashCounter > 0)
        {
            //Make the dash counter decrement
            dashCounter -= Time.deltaTime;

            //Make after image counter decrement
            afterImageCounter -= Time.deltaTime;

            //When the after image counter hits - then call the show after images function
            if (afterImageCounter <= 0)
                ShowAfterImage();
        }

        //Make time between dashes decrement
        if (dashWaitCounter > 0)
            dashWaitCounter -= Time.deltaTime;
    }

    void ShowAfterImage()
    {
        //Create a sprite renderer variable named image of the after image
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);

        //Switch the players sprite to the image, the local scale to the player and the color of image to the after image color
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        //Destroy the after image based on the image lifetime
        Destroy(image.gameObject, afterImageLifeTime);

        //Set the after image counter to time between images to create multiple images
        afterImageCounter = timeBetweenImages;
    }

    void Animations()
    {
        //Use parameters to set up animations for the player depending on mode (standing or ball)
        if (standingObject.activeSelf)
        {
            anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
            anim.SetBool("isGrounded", isGrounded);
        }
        else if (ballObject.activeSelf)
        {
            ballAnim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        }
    }

    void OnDrawGizmosSelected()
    {
        //Draw Gizmos for the ground point
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, .2f);
    }
}

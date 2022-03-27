using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Movement Variables")]
    public float moveSpeed;
    public float jumpForce;
    private float movX;
    public bool canDoubleJump;
    public float dashTime, dashSpeed, timeBetweenDashes;
    private float dashCounter, dashWaitCounter;

    [Header("GroundCheck Variables")]
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Shooting Variables")]
    public BulletController theBullet;
    public Transform shotPoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("After Image Effect Variables")]
    public Color afterImageColor;
    public float afterImageLifeTime, timeBetweenImages;
    private float afterImageCounter;
    public SpriteRenderer theSR, afterImage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //Get Components
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!PauseMenu.instance.isPaused && !UIController.instance.isDead && !GameManager.instance.isExiting)
        {
            WorldChecks();

            Inputs();

            Countdowns();
        }
    }

    void FixedUpdate()
    {
        if (!PauseMenu.instance.isPaused && !UIController.instance.isDead && !GameManager.instance.isExiting)
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
    }

    void WorldChecks()
    {
        //Detecting if the player is grounded using Unity's built in physics and layer masks
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
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
        if (Input.GetButtonDown("Dash") && dashWaitCounter <= 0)
        {
            dashCounter = dashTime;
            AudioManager.instance.PlaySFXAdjusted(0);
            ShowAfterImage();
            dashWaitCounter = timeBetweenDashes;
        }

        //Get inputs for shooting and perform it
        if ((Input.GetButtonDown("Shoot") || Input.GetAxis("ShootingTrigger") > 0.5f) && shotCounter <= 0)
        {
            //Create a bullet and base its move direction of the players local scale
            Instantiate(theBullet, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
            shotCounter = timeBetweenShots;
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

    void OnDrawGizmosSelected()
    {
        //Draw Gizmos for the ground point
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, .2f);
    }
}

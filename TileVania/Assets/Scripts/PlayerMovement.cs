using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //important header to use package

public class PlayerMovement : MonoBehaviour
{
    Vector2 directionInput;
    Rigidbody2D myRB2D;
    Animator myAnimator;

    float gravityScaleAtStart;
    bool isAlive = true;

    [SerializeField] float runSpeed = 5f; //to show on inspector
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    // Start is called before the first frame update
    [SerializeField] Vector2 enemyTouch = new Vector2(2f, 5f);

    [SerializeField] float delayTime = .25f;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform playerGun;
    CapsuleCollider2D myBodyCollider; // grabs player 2d collider
    BoxCollider2D myFeetCollider;

    
    void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRB2D.gravityScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!isAlive) {return;}
        playerRun();
        playerDirection();
        playerClimb();
        playerDeath();
    }
    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        directionInput = value.Get<Vector2>();
        //Debug.Log(directionInput);
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}
        //IsTouchingLayer a bool whether this collider is touching any collider on the specified layermask or not
        //LayerMask.GetMask returns a layer mask from the created layername
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if(value.isPressed) { //does not need to be update since it's a button
                myRB2D.velocity += new Vector2(0f, jumpSpeed);
                }
        }
        
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
        myAnimator.SetTrigger("Shooting");
        //you can use the Invoke function to call a method after a specified time delay. 
        //Invoke is a built-in function of the MonoBehaviour class, so it is available in any script that inherits from MonoBehaviour.
        Invoke("arrowShoot", delayTime);
    }

    void arrowShoot()
    {
        Instantiate(bullet, playerGun.position, transform.rotation);
    }

    void playerRun()
    {
        Vector2 playerRunVelocity = new Vector2(directionInput.x * runSpeed, myRB2D.velocity.y); //grabs user y gravity velocity
        myRB2D.velocity = playerRunVelocity;
        //Debug.Log(myRB2D.velocity.x);
        bool runningAnimationCheck = Mathf.Abs(myRB2D.velocity.x) > Mathf.Epsilon; // if velocity is detected = true
        myAnimator.SetBool("isRunning", runningAnimationCheck);
    }

    void playerDirection()
    {
        // built in function
        // eplison is used instead of 0 to detect the smallest case e.g. .0000000000x
        // abs = absolute value
        bool playerHasHorizontalSpeed = Mathf.Abs(myRB2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) { // change local scale flips character in 2D
            transform.localScale = new Vector2(Mathf.Sign(myRB2D.velocity.x), 1f);  //x value grabs current object velocity
        }
        
    }

    void playerClimb()
    {
        bool climbingAnimationCheck = Mathf.Abs(myRB2D.velocity.y) > Mathf.Epsilon;
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myRB2D.gravityScale = 0f;
            Vector2 playerClimbVelocity = new Vector2(myRB2D.velocity.x, directionInput.y * climbSpeed);
            myRB2D.velocity = playerClimbVelocity;
            myAnimator.SetBool("isClimbing", climbingAnimationCheck);
        }
        else {
            myRB2D.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
        }
        
    }
    
    void playerDeath()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRB2D.velocity = enemyTouch;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    
}

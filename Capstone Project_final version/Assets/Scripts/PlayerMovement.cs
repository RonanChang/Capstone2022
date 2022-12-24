using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D player;
    private BoxCollider2D coll;


    private bool isRunningLeft;
    private bool isRunningRight;
    private bool isStill;
    private bool isJumping = false;
    private bool isFalling;
    private bool doubleJump;
    [SerializeField]private bool flipMovement = false;
   
    
    private float originalGravity;
    private float dirX = 0f;
    private float dirY = 0f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 18f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float floatingSpeed = 2f;
    [SerializeField] private TrailRenderer trail;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        originalGravity = player.gravityScale;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing) {
            return;
        }
        //move horizontally
        if (!flipMovement)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);
        }
        else
        {
            dirX = Input.GetAxisRaw("Horizontal");
            player.velocity = new Vector2(-dirX * moveSpeed, player.velocity.y);
        }
        
       
            
        
        


        if (isGrounded() && !Input.GetButton("Jump")) {
            doubleJump = false;
        }


        //floating
        if (Input.GetKey("k")) {

            //Debug.Log("we are floating now");
            Floating();
        }

        if (Input.GetKeyUp("k"))
        {
            //Debug.Log("no longer floating");
            player.gravityScale = originalGravity;

        }

        //dashing
        if (Input.GetKeyDown("l") && canDash)
        {
            //Debug.Log("we are dashing");
            StartCoroutine(Dash());
        }


        //jump - press "space" and double jump
        if (Input.GetButtonDown("Jump")) {

            if (isGrounded() || doubleJump) {
                Debug.Log("space pressed");
                player.velocity = new Vector2(player.velocity.x, jumpForce);

                doubleJump = !doubleJump;
            }
        }

        //press longer to jump higher
        if (Input.GetButtonUp("Jump") && isJumping) {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
        }

        //check if running right or left or not running
        if (dirX > 0f)
        {
            isRunningRight = true;
        }
        else if (dirX < 0f)
        {
            isRunningLeft = true;
        }
        else {
            isStill = true;
        }

        if (player.velocity.y > .1f)
        {
            isJumping = true;
        }
        else if (player.velocity.y < -.1f) {
            isFalling = true;
        }



     
    }

   

    private bool isGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Floating() {
        //set the gravity to megative 
        player.gravityScale = -3f;
        //Debug.Log("the gravity is" + player.gravityScale);


        //move horizontally
        //dirX = Input.GetAxis("Horizontal");
        //player.velocity = new Vector2(dirX * floatingSpeed, player.velocity.y);

        if (!flipMovement)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);
        }
        else
        {
            dirX = Input.GetAxisRaw("Horizontal");
            player.velocity = new Vector2(-dirX * moveSpeed, player.velocity.y);
        }

        //move vertically
        dirY = Input.GetAxis("Vertical");
        player.velocity = new Vector2(player.velocity.x, dirY * floatingSpeed);

    }


    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        player.gravityScale = 0f;

        //change the dashing according to the direction the player is facing
        Debug.Log("dashing" + transform.localScale.x * dashingPower);
        player.velocity = new Vector2(dirX * dashingPower, 0f);

        trail.emitting= true;
        yield return new WaitForSeconds(dashingTime);
        trail.emitting = false;
        player.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowField"))
        {
            Debug.Log("I'm slow now");
            moveSpeed = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowField"))
        {
            Debug.Log("I'm slow now");
            moveSpeed = 3f;
        }
    }
}

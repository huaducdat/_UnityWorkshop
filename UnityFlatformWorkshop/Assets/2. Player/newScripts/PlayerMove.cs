using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]   
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rigi2d;
    private Vector3 refVelocity = Vector3.zero;
    private float MovementSmoothing = .05f;
    private bool FacingRight = true;
    public Transform collisionHit;
    public float collisionHitRadius = 0.5f;
    public Collider2D collision2DHit;
    public AnimationClip Hitmotion;
    private float timeOff = 0;
    //private float timeEnble = 0;
    public float jumpForce = 350f;

    public Transform groundCheck;
    private float radiusCheck = 0.2f;
    public LayerMask whatIsGround;


    bool checkPlay;
    bool isGround;

   

    /// <summary>
    /// ///////////
    /// </summary>
    private void Hit()
    {
        
        
        if (Input.GetButtonDown("Fire1") && isGround)
        {
            animator.SetTrigger("IsHit");
            //
            //collision2DHit.enabled = true;
            checkPlay = true;          
        }

        if (checkPlay)
        {
            timeOff += Time.deltaTime;
        }


        if (timeOff >= 0.5f)
        {
            collision2DHit.enabled = true;
            Collider2D collider2D = Physics2D.OverlapCircle(collisionHit.position, collisionHitRadius);

        }

        if (timeOff >= 1f)
        {
            collision2DHit.enabled = false;
            timeOff = 0;
            checkPlay = false;
        }
        Debug.LogError(timeOff);
    }



    // Start is called before the first frame update
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        float move = horizontal * Time.deltaTime;
       

        Vector3 targetVelocity = new Vector2(move * 10f, rigi2d.velocity.y);
        rigi2d.velocity = Vector3.SmoothDamp(rigi2d.velocity, targetVelocity, ref refVelocity, MovementSmoothing);


        // If the input is moving the player right and the player is facing left...
        if (move> 0 && !FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move< 0 && FacingRight)
        {
            // ... flip the player.
            Flip();
        }

    }

    private void Jump()
    {
        


        if(Input.GetButtonDown("Jump") && isGround)
        {
            
            rigi2d.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("IsJump", true);


        }
    }

   
    private void CheckGround()
    {
        bool wasGround = isGround;
        isGround = false;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheck.position, radiusCheck, whatIsGround);
        for(int i = 0; i < collider2Ds.Length; i++)
        {
            if(collider2Ds[i].gameObject != gameObject)
            {
                isGround = true;  
                if(!wasGround)
                {
                    animator.SetBool("IsJump", false);
                }
            }
           
        }

        Debug.LogError(isGround);

    }






    private void Flip()
    {


        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


    }


    void Start()
    {
        animator = GetComponent<Animator>();
        rigi2d = GetComponent<Rigidbody2D>();
        collision2DHit.enabled = false;
        isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hit();
        CheckGround();
        Jump();
    }
}

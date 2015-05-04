using UnityEngine;
using System.Collections;

public class GiantMovementController : MonoBehaviour {
    
     public bool movingRight, movingLeft, jumping = false;
    public float force;
    Rigidbody2D myRigidbody;
    public float jumpForce;
    public bool grounded;
    void Start()
    {
        grounded = false;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StoppedMovingLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StoppedMovingRight();
        }

        this.transform.up = this.transform.position;

        if (movingLeft == true)
        {

            //Left Movement Function
             Move(-force);
            
        }
        else if (movingRight == true)
        {

            //Right Movement Function
             Move(force);
        }
    }

    public void MoveRight()
    {
        if (movingLeft == true)
        {
             Jump();
            jumping = true;
        }
        else
        {
            movingRight = true;
            Player.player.animator.SetBool("isRunning", true);
        }
    }
    
    public void MoveLeft()
    {

        if (movingRight == true)
        {
             Jump();
            jumping = true;
        }
        else
        {
            movingLeft = true;
            Player.player.animator.SetBool("isRunning", true);
        }
    }
    public void StoppedMovingRight()
    {
        movingRight = false;
        if (!jumping)
        {
            myRigidbody.velocity = Vector2.zero;
        }
        Player.player.animator.SetBool("isRunning", false);
    }
    public void StoppedMovingLeft()
    {
        movingLeft = false;
        if (!jumping)
        {
            myRigidbody.velocity = Vector2.zero;
        }
        Player.player.animator.SetBool("isRunning", false);

    }

    public void Move(float force)
    {
        //this.myRigidBody.AddForce(this.transform.right*force,ForceMode2D.Impulse);
        if (grounded)
        {
            //to make the object face the direction it's moving to
            if (force < 0)
            {
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                this.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //moveForce = force;
            myRigidbody.velocity = this.transform.right * force;
            //myRigidBody.velocity = truncate(myRigidBody.velocity); 
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            myRigidbody.velocity += (Vector2)(this.transform.up * jumpForce);
            grounded = false;
            jumping = true;
            Player.player.animator.SetTrigger("Jumped"); 
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planet")
        {
            if (!grounded)
            {
                jumping = false;
                grounded = true;


                Player.player.animator.SetTrigger("Landed");
                if (movingLeft == true || movingRight == true)
                {
                    Player.player.animator.SetBool("isRunning", true);
                }
                
            }

        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Planet")
        {
            grounded = false;
        }
    }
    
}

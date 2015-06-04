using UnityEngine;
using System.Collections;

public class GiantMovementController : MonoBehaviour {
    
     public bool movingRight, movingLeft, jumping = false;
    public float force;
    Rigidbody2D myRigidbody;
    public float jumpForce;
    public bool grounded;
    public int counter;
    void Start()
    {
        counter = 0;
        grounded = false;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Your code is bad and you should feel bad
        //very fucking bad
        this.transform.up = this.transform.position.normalized;

        if ((int)(this.transform.rotation.eulerAngles.z + 0.5f) <= 180 && (this.transform.rotation.eulerAngles.z + 0.5f) >= 178f)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 180.5f);
        }


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
       
        if (movingLeft == true && grounded)
        {
             Jump();
            jumping = true;
        }
        else if (movingLeft)
        {
            //this is so the giant does not jump when a false jump occurs as well as not switching direction when it doesnt
        }
        else
        {
            movingRight = true;
            Player.player.animator.SetBool("isRunning", true);
        }
    }
    
    public void MoveLeft()
    {

        if (movingRight == true && grounded)
        {
            Jump();
            jumping = true;
        }
        else if(movingRight)
        {
            //this is so the giant does not jump when a false jump occurs as well as not switching direction when it doesnt
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
        if (grounded && !GiantDeath.instance.isDead)
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
        if (grounded && !GiantDeath.instance.isDead)
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
                if (jumping)
                {
                    Player.player.animator.SetTrigger("Landed");
                }
                jumping = false;
                grounded = true;

                
                if (movingLeft == true || movingRight == true)
                {
                    Player.player.animator.SetBool("isRunning", true);
                }
                
            }
            //counter++;
            //if (counter > 1 || counter < 0)
            //{
            //    StartCoroutine("DisableEnableCollider");
            //    counter = 0;
            //}
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planet")
        {
            
            grounded = false;
            StartCoroutine("DisableEnableCollider");
            //counter--;
            //if (counter > 1 || counter < 0)
            //{
            //    StartCoroutine("DisableEnableCollider");
            //    counter = 0;
            //}
        }
    }

    public IEnumerator DisableEnableCollider()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForEndOfFrame();
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
}

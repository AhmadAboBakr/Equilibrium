using UnityEngine;
using System.Collections;

public class SurfaceMovingObject : MonoBehaviour
{
    //your Script is bad and you should feel bad
    public bool grounded = true;
    public float jumpForce = 10;
    public float maxSpeed = 3;
    public float moveForce = 0;
    private Rigidbody2D myRigidBody;
    public bool stopped;
    public Animator myanim;
    
     void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myanim = gameObject.GetComponent<Animator>();
        //StartCoroutine("ShouldAddForce");
    }

    protected void Update()
    {
        this.transform.up = this.transform.position;

        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.right,.5f);
        if (right.collider != null)
        {


            this.myRigidBody.AddForce(this.transform.up * 2,ForceMode2D.Impulse);
            RaycastHit2D down = Physics2D.Raycast(transform.position, transform.up, .5f, 5);

        }
        else
        {


            //RaycastHit2D down = Physics2D.Raycast(transform.position, -transform.up, 1.5f, 5);
            //if (down.collider != null)
            //{
            //    this.transform.up = down.normal;
            //}
            //else
            //{
            //    this.transform.up = this.transform.position;
            //}
            
        }
        
    }
    /*
     * this functions takes the force that should be used to move the object and set it to moveForce variable
     *  ----------------------------------------------------------------------
     * parms: 
     *  *  float force : the desired force to be applied
     *  ----------------------------------------------------------------------
     * returns : nothing
     * -----------------------------------------------------------------------
     * History:
     * 
     */
    public void Move(float force)
    {
        //this.myRigidBody.AddForce(this.transform.right*force,ForceMode2D.Impulse);
        //this.transform.up = this.transform.position;
       if (grounded )
        {
            stopped = false;
           //to make the object face the direction it's moving to
            if (force<0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else 
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            //moveForce = force;
            this.myRigidBody.AddForce(this.transform.right*force,ForceMode2D.Impulse);
            //myRigidBody.velocity = truncate(myRigidBody.velocity);
        }
       this.GetComponent<Animator>().SetBool("Running", true);
        
    }
    public void StopMoving()
    {

       
        stopped = true;
        this.GetComponent<Animator>().SetBool("Running", false);
    }

    
    Vector2 truncate(Vector2 vec2)
    {
        if (vec2.magnitude > maxSpeed)
        {
            return vec2.normalized * maxSpeed;
        }
        else
            return vec2;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planet")
        {

            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Planet")
        {
            if (!this.CompareTag("Player"))
            {
                grounded = false;
            }
        }
    }

   
}

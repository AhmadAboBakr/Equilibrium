using UnityEngine;
using System.Collections;

public class MoveItMoveIt : MonoBehaviour
{
    //your Script is bad and you should feel bad
    public bool grounded = true;
    public float jumpForce = 10;
    public float maxSpeed = 3;
    public float moveForce = 0;
    private Rigidbody2D myRigidBody;
    
     void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();

        //StartCoroutine("ShouldAddForce");
    }

    protected void Update()
    {
        this.transform.up = this.transform.position;

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
       if (grounded )
        {
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
        
    }
    public void StopMoving()
    {
        myRigidBody.velocity = Vector2.zero;
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
    void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Planet"))
        {
            Vector2 avarage = Vector2.zero;
            foreach (var point in other.contacts)
            {
                avarage += point.normal;
                //myRigidBody.AddForce(point.normal*20,ForceMode2D.Impulse);
            }
            this.transform.up = avarage;
            this.myRigidBody.AddForce(this.transform.right*moveForce,ForceMode2D.Impulse);
        }
    }
}

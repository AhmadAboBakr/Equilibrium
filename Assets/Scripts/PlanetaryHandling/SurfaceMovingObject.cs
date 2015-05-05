using UnityEngine;
using System.Collections;

public class SurfaceMovingObject : MonoBehaviour
{
    //your Script is bad and you should feel bad
    public bool grounded = true;
    public int layer;
    public float jumpForce = 10;
    public float maxSpeed = 5;
    public float moveForce = 0;
    public bool movingRight, MovingLeft;
    public bool needsToGoUp = false;
    public Transform rayCaster;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private string[] collisionLayer;
    
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        StartCoroutine("UpdateCouroutine");
        movingRight = MovingLeft = false;
        myAnimator= this.GetComponent<Animator>();
        grounded = false;
        collisionLayer = new string[1];
        collisionLayer[0] = "planetSegments";

    }

    void Update()
    {
        this.transform.up = this.transform.position;
        if (needsToGoUp)
        {
            this.myRigidBody.AddForce(this.transform.up * 2, ForceMode2D.Impulse);
            Move(-this.transform.localScale.x);
        }

    }
    IEnumerator UpdateCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            RaycastHit2D right = Physics2D.Raycast(transform.position, rayCaster.localPosition, 1f,LayerMask.GetMask(collisionLayer));

            if (right.collider != null)
            {

                needsToGoUp = true;

            }
            else
            {
                
                needsToGoUp = false;
            }

        }
    }
    //protected void Update()
    //{
    //    //else
    //    //{

    //    //    //RaycastHit2D down = Physics2D.Raycast(transform.position, -transform.up, 1.5f, 5);
    //    //    //if (down.collider != null)
    //    //    //{
    //    //    //    this.transform.up = down.normal;
    //    //    //}
    //    //    //else
    //    //    //{
    //    //    //    this.transform.up = this.transform.position;
    //    //    //}

    //    //}

    //}
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
        if (grounded)
        {
            //to make the object face the direction it's moving to
            if (force < 0)
            {
                if (MovingLeft)
                {
                    this.myRigidBody.velocity = Vector2.zero;
                }
                movingRight = true;
                MovingLeft = false;
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (movingRight)
                {
                    this.myRigidBody.velocity = Vector2.zero;

                }
                movingRight = false;
                MovingLeft = true;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            this.myRigidBody.velocity = Vector3.zero;
            this.myRigidBody.AddForce(this.transform.right * force*10, ForceMode2D.Impulse);
            this.myRigidBody.velocity = truncate(this.myRigidBody.velocity);

            //This is needed to stop the Object Momentum
            //var angle = Vector3.Angle(this.transform.right);
            
        }
        myAnimator.SetBool("Running", true);

    }
    public void StopMoving()
    {
        myAnimator.SetBool("Running", false);
        if (grounded)
        {
            this.myRigidBody.velocity = Vector2.zero ;
        }
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
            grounded = false;
        }
    }
}

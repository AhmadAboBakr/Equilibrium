using UnityEngine;
using System.Collections;

public class SurfaceMovingObject : MonoBehaviour
{
    //your Script is bad and you should feel bad
    public bool grounded = true;
    public float jumpForce = 10;
    public float maxSpeed = 5;
    public float moveForce = 0;
    private Rigidbody2D myRigidBody;
    public bool stopped;
    public bool needsToGoUp=false;
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        StartCoroutine("UpdateCouroutine");
    }

    void Update()
    {
        this.transform.up = this.transform.position;
        if (needsToGoUp)
        {
            this.myRigidBody.AddForce(this.transform.up * 2, ForceMode2D.Impulse);
        }

    }
    IEnumerator UpdateCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);

            RaycastHit2D right = Physics2D.Raycast(transform.position, transform.right, .5f);
            if (right.collider != null)
            {
                needsToGoUp = true;
                this.GetComponent<SpriteRenderer>().color = Color.black;

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
        //this.myRigidBody.AddForce(this.transform.right*force,ForceMode2D.Impulse);
        //this.transform.up = this.transform.position;
        if (grounded)
        {
            stopped = false;
            //to make the object face the direction it's moving to
            if (force < 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            var tangentForce=Vector2.Dot(this.transform.right, this.myRigidBody.velocity);
            if (tangentForce > 0)
            {
                //this.myRigidBody.velocity /= 10;
            }
            this.myRigidBody.AddForce(this.transform.right * force, ForceMode2D.Impulse);
            this.myRigidBody.velocity = truncate(this.myRigidBody.velocity);
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
            grounded = false;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurfaceMovingObject : MonoBehaviour
{
    //caching
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CircleCollider2D myCircleCollider;
    ArtifitialGravity myArtifitialGravity;
    SeekingClass mySeekingAgent;
    //your Script is bad and you should feel bad
    public bool grounded = true;
    public int layer;
    public float jumpForce = 10;
    public float maxSpeed = 5;
    public float moveForce = 0;
    public bool movingRight, MovingLeft;
    public bool needsToGoUp = false;
    public float upForce;
    private string[] collisionLayer;
    public LayerMask planet;
    bool idle = false;
    List<GameObject> children;
    void Awake()
    {
        myAnimator = this.GetComponent<Animator>();
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myArtifitialGravity = this.GetComponent<ArtifitialGravity>();
        myCircleCollider = this.GetComponent<CircleCollider2D>();
        mySeekingAgent = this.GetComponent<SeekingClass>();

    }
    void Start()
    {
        children = new List<GameObject>();
        //StartCoroutine("UpdateCouroutine");
        movingRight = MovingLeft = false;
        grounded = false;
        collisionLayer = new string[1];
        collisionLayer[0] = "planetSegments";
        
        //******************** remove
        //for (int i = 0; i < this.gameObject.transform.childCount; i++)
        //    {
        //        children.Add(this.gameObject.transform.GetChild(i).gameObject);
        //    }

        //for (int i = 0; i < children.Count; i++)
        //{
        //    for (int j = 0; j < children[i].gameObject.transform.childCount; j++)
        //    {
        //        children.Add(children[i].gameObject.transform.GetChild(j).gameObject);
        //    }
        //}

        //for (int i = this.gameObject.transform.childCount; i < children.Count; i++)
        //{
        //    for(int j = 0; j < children[i].gameObject.transform.childCount; j++)
        //    {
        //        if (children[i].gameObject.transform.GetChild(j).gameObject != null)
        //            children.Add(children[i].gameObject.transform.GetChild(j).gameObject);
        //    }
        //}

        //for(int i = 0; i < children.Count; i++)
        //{
        //   BoxCollider2D coll = children[i].GetComponent<BoxCollider2D>();

        //    if(coll != null)
        //    {
        //        DestroyImmediate(coll);
        //    }
        //}
	
        
        //************************

    }
    void OnEnable()
    {
        StartCoroutine("CheckInGiantRange");
        grounded = false;
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    void Update()
    {

        this.transform.up = this.transform.position;
        if (this.transform.rotation.eulerAngles.z == 180)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 180.01f);
        }
        if (needsToGoUp)
        {
            this.myRigidBody.AddForce(this.transform.up * upForce, ForceMode2D.Impulse);

        }
        if(this.myRigidBody.velocity.magnitude >= 120)
        {

            this.GetComponent<DestructibleEnemy>().DisableAfterTime();
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

        if (grounded &&!idle)
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
            this.myRigidBody.AddForce(this.transform.right * force * moveForce, ForceMode2D.Impulse);
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
            this.myRigidBody.velocity = Vector2.zero;
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
            needsToGoUp = false;
            //if (needsToGoUp)
            //{
            //    needsToGoUp = false;
            //}
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {

            needsToGoUp = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Planet") && needsToGoUp)
        {
            myRigidBody.AddForce(this.transform.right *- this.transform.localScale.x * 3, ForceMode2D.Impulse);
            needsToGoUp = false;
        }
    }
    public void DisableCollider()
    {
        //grounded = false;
        this.myCircleCollider.enabled = false;
    }
    public void EnableCollider()
    {
        this.myCircleCollider.enabled = true;
    }
    IEnumerator CheckInGiantRange()
    {
        for (int i = 0; i < 20; i++)
        {
            Move(Random.Range(-8, 8)); 
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (Vector2.Distance(this.transform.position, Player.player.transform.position) < 170)
            {
                idle = false;
                myArtifitialGravity.enabled = true;
                myRigidBody.isKinematic = false;
                mySeekingAgent.enabled = true;
                myAnimator.enabled = true;
            }
            else
            {
                idle = true;
                myArtifitialGravity.enabled = false;
                myRigidBody.isKinematic = true;
                mySeekingAgent.enabled = false;
                myAnimator.enabled = false;
                myRigidBody.Sleep();

            }
        }
    }
    //public IEnumerator DisableEnableCollider()
    //{
    //    //grounded = false;
    //    //this.GetComponent<CircleCollider2D>().enabled = false;
    //    //yield return null;
    //    //this.GetComponent<CircleCollider2D>().enabled = true;
    //}
}

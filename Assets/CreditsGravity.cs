using UnityEngine;
using System.Collections;

public class CreditsGravity : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    public GameObject centerOfGravity;
    public Vector2 myPosition;
    public Quaternion myRotation;
    bool clicked;
    // Use this for initialization
    void Awake()
    {
        myRotation = this.transform.rotation;
        myPosition = this.transform.position;
    }
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        //myRigidBody.AddForce(new Vector2(70, 70));
        


    }
    void OnEnable()
    {
        this.transform.position = myPosition;
        this.transform.rotation = myRotation;
        clicked = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clicked = true;
        }
        if (clicked)
        {
            Vector2 gravityVec = this.centerOfGravity.GetComponent<Rigidbody2D>().position - myRigidBody.position;
            myRigidBody.velocity += gravityVec.normalized * 40 * Time.deltaTime;
        }
        
    }

    
}

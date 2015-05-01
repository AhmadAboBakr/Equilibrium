using UnityEngine;
using System.Collections;

public class FlyingObjectArchived : MonoBehaviour
{
    public float minFlapsPerSeconds;
    public float maxFlapsPerSeconds;
    public float flapForce;
    public float maxHeight;
    public float minHeight;
     
    Rigidbody2D myRigidBody;
    IEnumerator Flap()
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.position.normalized,20);

            if (hit.collider != null)
            {
                myRigidBody.AddForce(this.transform.position.normalized * flapForce, ForceMode2D.Impulse);
               float distance =  Vector2.Distance(hit.point, this.transform.position);
              
               if (distance > maxHeight) 
                    yield return new WaitForSeconds(1/minFlapsPerSeconds);
                else
                {
                    yield return new WaitForSeconds(1/maxFlapsPerSeconds);
                }
            }
            else
            {
                yield return new WaitForSeconds(1 / maxFlapsPerSeconds);
            }
        }
    }

    protected void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        StartCoroutine("Flap");
    }

    protected void Update()
    {
    }
}

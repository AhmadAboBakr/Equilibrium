using UnityEngine;
using System.Collections;

public class DestructableComponent : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    BoxCollider2D collider;
    ArtifitialGravity aGravity;
    public bool isGiant;
    PolygonCollider2D giantCollider;
    float timeToStartFade = 4;
    float fadeRate = 0.1f;
    SpriteRenderer item;
    void Awake()
    {
        aGravity = this.GetComponent<ArtifitialGravity>();
        if (isGiant)
        {
            giantCollider = this.GetComponent<PolygonCollider2D>();
            giantCollider.enabled = false;
        }
        else
        {
            collider = this.GetComponent<BoxCollider2D>();
            collider.enabled = false;
        }
        myRigidBody = this.GetComponent<Rigidbody2D>();


        myRigidBody.isKinematic = true;
        aGravity.enabled = false;

    }
    void Start()
    {
        item = this.GetComponent<SpriteRenderer>();
    }
    public void SelfDestruct(Transform parent)
    {
        aGravity.enabled = true;
        if (isGiant)
            giantCollider.enabled = true;
        else
            collider.enabled = true;
        myRigidBody.isKinematic = false;
        this.myRigidBody.AddForce((parent.position - this.transform.position).normalized * -20, ForceMode2D.Impulse);
        this.myRigidBody.angularVelocity = 100*(Random.Range(0,2)*2)-1;
        this.transform.parent = null;
        //StartCoroutine("DestroyAfterTime");
    }


    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToStartFade);
        while (true)
        {



            Color c = item.color;

            c.a /= 1.05f;
            item.color = c;
            if (c.a < 0.04)
            {
                Destroy(this.gameObject);
            }

            //for (int i = 0; i < transform.childCount; i++)
            //{

            //    Color c = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            //    c.a /= 1.05f;
            //    transform.GetChild(i).GetComponent<SpriteRenderer>().color = c;
            //    if (c.a < 0.04)
            //    {
            //        Destroy(this.gameObject);
            //    }
            //}
            yield return new WaitForSeconds(fadeRate);
        }
    }
}

using UnityEngine;
using System.Collections;

public class DestructableComponent : MonoBehaviour {
    Rigidbody2D myRigidBody;
    BoxCollider2D collider;
    ArtifitialGravity aGravity;
    float timeToStartFade = 4;
    float fadeRate = 0.1f;
    void Awake()
    {
        aGravity = this.GetComponent<ArtifitialGravity>();
        collider = this.GetComponent<BoxCollider2D>();
        myRigidBody = this.GetComponent<Rigidbody2D>();
        collider.enabled = false;
        myRigidBody.isKinematic = true;
        aGravity.enabled = false;

    }
	void Start () {
	}
    public void SelfDestruct(Transform parent)
    {
        aGravity.enabled = true;
        collider.enabled = true;
        myRigidBody.isKinematic = false;
        this.myRigidBody.AddForce((parent.position - this.transform.position).normalized*-20,ForceMode2D.Impulse);
        this.transform.parent = null;
        StartCoroutine("DestroyAfterTime");
    }
    public IEnumerator DestroyAfterTime()
    {

        yield return new WaitForSeconds(timeToStartFade);
        while (true)
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                Color c = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                c.a /= 1.05f;
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = c;
                if (c.a < 0.04)
                {
                    Destroy(this.gameObject);
                }
            }
            yield return new WaitForSeconds(fadeRate);
        }
    }
}

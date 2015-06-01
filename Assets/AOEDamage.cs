using UnityEngine;
using System.Collections;

public class AOEDamage : MonoBehaviour {
    public float counter;
    public float damage;
    
	// Use this for initialization
	void Start () {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        //If the object in the trigger is a static object such as a tree or a building, then it only decreases its health
        if (other.CompareTag("Building"))
        {
            other.GetComponent<DestructableObject>().Health -= damage;

            counter++;

        }
        //If the object is an enemy or rubble then it is added to the list ThrowableObjectsInTrigger:gameobject
        if (other.CompareTag("Enemy"))
        {

            counter++;
            other.GetComponent<Rigidbody2D>().AddForce((other.transform.position - this.transform.position).normalized * damage, ForceMode2D.Impulse);
            other.GetComponent<DestructibleEnemy>().Health--;

        }
        if (other.CompareTag("Plane"))
        {
            other.GetComponent<PlaneScript>().Health -= damage;
        }
        if(other.CompareTag("Tree"))
        {
            other.GetComponent<DestructableObject>().Health -= damage;
        }


        StartCoroutine("DestroyAfterFinishing");
        //Destroy(gameObject);
    }
    public IEnumerator DestroyAfterFinishing()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.transform.parent.gameObject);
    }
}

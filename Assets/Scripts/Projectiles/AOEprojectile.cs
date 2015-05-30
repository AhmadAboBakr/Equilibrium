using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AOEprojectile : MonoBehaviour {
    bool collided;
    public float spellPower;
    private List<GameObject> ThrowableObjectsInTrigger;
    public float damage;
    public float counter;

    void Start()
    {
        counter = 0;
        ThrowableObjectsInTrigger = new List<GameObject>();
    }
	
   
   
    void OnTriggerEnter2D(Collider2D other)
    {
        
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
                other.GetComponent<Rigidbody2D>().AddForce((other.transform.position - this.transform.position).normalized * spellPower, ForceMode2D.Impulse);
                other.GetComponent<DestructibleEnemy>().Health--;
            }

            Debug.Log(other.gameObject.name);
            StartCoroutine("DestroyAfterFinishing");
            //Destroy(gameObject);
    }


    //void OnTriggerExit2d(Collider2D other)
    //{
    //    //If an object leaves the trigger before the coroutine is called then it is removed from the list
    //    if (collided)
    //    {
    //        ThrowableObjectsInTrigger.Remove(other.gameObject);
    //    }
    //}
    //IEnumerator DestroySelf()
    //{
    //    //Waits two frames to get all object in the trigger
    //    yield return new WaitForEndOfFrame();
    //    yield return new WaitForEndOfFrame();

    //    foreach (var throwable in ThrowableObjectsInTrigger)
    //    {
    //        //Add force to object in the list
    //        throwable.GetComponent<Rigidbody2D>().AddForce((throwable.transform.position - this.transform.position)*spellPower , ForceMode2D.Impulse);

    //    }
    //    //destroy projectile
    //    Destroy(this.gameObject);
    //}

    void OnCollisionEnter2D(Collision2D other)
    {
        collided = true;
        this.GetComponent<CircleCollider2D>().enabled = true;

        if (other.gameObject.tag == "Planet")
        {
            
            PlanetClass planet = other.gameObject.GetComponent<PlanetClass>();
            planet.Dig(GetComponent<PolygonCollider2D>(), transform.position+Vector3.down);
        }


        

        
    }

    public IEnumerator DestroyAfterFinishing()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}

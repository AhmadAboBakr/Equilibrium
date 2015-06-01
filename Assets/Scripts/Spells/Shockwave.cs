using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour
{
    public float force;
    public float damage;
    public float rateOfGrowth;
    public Vector3 final;
    public GameObject particle;
    void Update()
    {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.transform.localScale * 4, Time.deltaTime * 3);
        if (this.transform.localScale.x>64) Destroy(this.gameObject); ;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody2D>().velocity = (other.transform.position - this.transform.position).normalized * force;
        }
        if (other.CompareTag("Tree") || other.CompareTag("Building"))
        {
            other.GetComponent<DestructableObject>().Health -= damage;
        }
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<DestructibleEnemy>().Health -= damage;
            other.GetComponent<Rigidbody2D>().AddForce((other.transform.position - this.transform.position).normalized * damage, ForceMode2D.Impulse);
        }
        Instantiate(particle, this.transform.position, Quaternion.identity);
        StartCoroutine("DestroyAfterFinishing");
    }

    public IEnumerator DestroyAfterFinishing()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.transform.gameObject);
    }
}

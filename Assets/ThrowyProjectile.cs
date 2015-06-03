using UnityEngine;
using System.Collections;

public class ThrowyProjectile : MonoBehaviour {
    public Rigidbody2D myRigidbody;
    public float damage = 0.1f;
    public GeneralPooling pooler;
    public GameObject explosion;
	void Start () 
    {
        //myRigidbody = this.GetComponent<Rigidbody2D>();
        //myRigidbody.AddForce(new Vector2(-1, 1) * 20, ForceMode2D.Impulse);
        pooler = this.transform.parent.GetComponent<GeneralPooling>();
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Player.player.HealthPoints -= damage;
        }
        pooler.ReturnObjectToPool(this.gameObject);
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

}

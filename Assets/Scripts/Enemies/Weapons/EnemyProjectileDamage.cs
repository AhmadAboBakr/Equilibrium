using UnityEngine;
using System.Collections;

public class EnemyProjectileDamage : MonoBehaviour {
    public float damage;
    public GameObject explosion;
    /*
     * Hit is for making sure the enemy projectile doesn't damage the player twice. 
     * al mo2men la yoldagh men go7r maretein
     */
    public bool hit = false;

	void Update () 
    {
        	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.CompareTag("Player"))
        {
            Player.player.HealthPoints -= damage;
        }
        if(explosion)
            Instantiate(explosion, this.transform.position, Quaternion.identity);
        hit = true;
    }
    void OnDisable(){
        hit=false;
    }
}

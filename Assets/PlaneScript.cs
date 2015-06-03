using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour {
    public float health;
    public bool dead;
    public bool isInGiantMeleeList = false;
    public GameObject smokeTrail;
    public GameObject explosion;
    public bool landed;


    void Start()
    {
        landed = false;
    }
    //void Update()
    //{
    //    //To refactor in the future so it doesnt keep updating constantly
    //    //This was to replace line 44 to line 48 or the section where the object is removed from the list in <destructibleObject.cs>
    //    if (dead && GiantMeleeAttack.player.attackables.Contains(this.gameObject))
    //    {
    //        GiantMeleeAttack.player.attackables.Remove(this.gameObject);
    //    }
    //}
    void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<ArtifitialGravity>().enabled = true;
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5, ForceMode2D.Impulse);

        }
    }


    public float Health
    {
        get { return health; }
        set
        {
            health = value;

           
            if (health <= 0 && !dead)
            {
                dead = true;
                this.GetComponent<Animator>().enabled = false;
                this.GetComponent<ArtifitialGravity>().enabled = true;
                this.GetComponent<Rigidbody2D>().isKinematic = false;
                this.GetComponent<Rigidbody2D>().AddForce(-Vector2.right*5);
                smokeTrail.SetActive(true);
                
                
               
            }

        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Planet") && !landed)
        {
            landed = true;
            explosion.SetActive(true);
            this.gameObject.GetComponent<EnemySpawn>().enabled = false;
            StartCoroutine("DieAfterTime");
            
        }
    }
    public IEnumerator DieAfterTime()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.transform.parent.gameObject);
    }
}

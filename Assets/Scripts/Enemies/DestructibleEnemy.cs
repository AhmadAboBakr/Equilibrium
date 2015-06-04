using UnityEngine;
using System.Collections;

public class DestructibleEnemy : MonoBehaviour
{
    public float health = 1;
    public bool dead = false;
    public float timeToDestroy = 2f;
    public Animator myAnim;
    public Rigidbody2D myRigidbody;
    public ArtifitialGravity myArtGrav;
    public GeneralPooling pooler;
    public bool animationCalled;
    public float deathTimer;
    public float startHealth;
    
    // Use this for initialization
    void OnEnable()
    {
        dead = false;
        health = startHealth;
    }
    
    void OnDisable()
    {
        dead = false;
        health = startHealth;
        deathTimer = 0;
    }
    void Start()
    {
        StartCoroutine("checkPosition");
        deathTimer = 0;
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myArtGrav = GetComponent<ArtifitialGravity>();
        pooler = this.transform.parent.GetComponent<GeneralPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && GiantMeleeAttack.player.attackables.Contains(this.gameObject))
        {
            GiantMeleeAttack.player.attackables.Remove(this.gameObject);
            myAnim.SetTrigger("die");

        }
        if(health <= 0 && animationCalled == false)
        {
            dead = true;
        }
        if(dead)
        {
            deathTimer += Time.deltaTime;
        }
        if(deathTimer > 0.8f)
        {
            DisableAfterTime();

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
                GameState.CurrentNumberOfEnemies--;
                myAnim.SetTrigger("die");

                //Disable Box Collider of parent Object
                //this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                //myRigidbody.isKinematic = true;
                //myAnim.enabled = false;
                //myArtGrav.enabled = false;
                //for (int i = 0; i < transform.childCount; i++)
                //{
                //    while (this.transform.GetChild(i).childCount > 0)
                //    {
                //        this.transform.GetChild(i).GetChild(0).parent = this.transform;

                //    }
                //}
                //for (int i = 0; i < transform.childCount; i++)
                //{
                //    //Add Rigid body to all rubble objects and enable their disabled box colliders and add Gravity so they fall
                //    transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                //    transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;
                //    transform.GetChild(i).gameObject.AddComponent<ArtifitialGravity>();
                //    //Add force to rubble objects 
                //    transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().AddForce((transform.GetChild(i).position - Player.player.transform.position).normalized * Random.Range(10, 20), ForceMode2D.Impulse);
                //}
                //Remove the gameobject from the attackables list they enter ontriggerenter with the player
                //(GameObject.FindGameObjectWithTag("Player") as GameObject).GetComponent<GiantMeleeAttack>().RemoveSelfFromAttackableList(this.gameObject);
                //GiantMeleeAttack.player.RemoveSelfFromAttackableList(this.gameObject);
                //GiantMeleeAttack.player.attackables.Remove(this.gameObject);
                //Destroy rubble after time
                //StartCoroutine("DisableAfterTime");
            }

        }
    }
    public void DisableAfterTime()
    {
        health = startHealth;
        dead = false;
        pooler.ReturnObjectToPool(this.gameObject);
        GameManager.instance.enemyKillCount++;
    }
    public IEnumerator checkPosition()
    {
        while(true)
        {
            if(this.transform.position.magnitude < 50)
            {
                //pooler.ReturnObjectToPool(this.gameObject);
               
            }
            yield return new WaitForSeconds(1);
        }
    }
}

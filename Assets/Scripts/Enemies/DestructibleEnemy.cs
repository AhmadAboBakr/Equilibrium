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
        if(deathTimer > 1f)
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
            }

        }
    }
    public void DisableAfterTime()
    {
        this.myRigidbody.velocity = Vector2.zero;
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

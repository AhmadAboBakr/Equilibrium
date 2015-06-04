using UnityEngine;
using System.Collections;

public class DestructableObject : MonoBehaviour
{
    public float health = 1;
    public bool dead = false;
    public bool isInGiantMeleeList = false;
    public float timeToStartFade = 4;
    public float fadeRate = 0.1f;
    public GameObject debris;
    public AudioSource myAudioSource;

    void Start()
    {
        //foreach (var component in this.transform.GetComponentsInChildren<DestructableComponent>())
        //{
        //    component.SelfDestruct(this.transform);
        //}
        myAudioSource = this.GetComponent<AudioSource>();

    }
    void Update()
    {
        //To refactor in the future so it doesnt keep updating constantly
        //This was to replace line 44 to line 48 or the section where the object is removed from the list 
        if (dead && GiantMeleeAttack.player.attackables.Contains(this.gameObject))
        {
            GiantMeleeAttack.player.attackables.Remove(this.gameObject);
        }
    }
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (this.gameObject.CompareTag("Building"))
            {
                Instantiate(debris, this.transform.position, Quaternion.Euler(-this.transform.position));
                myAudioSource.Play();
            }
            //GetComponent<Animator>().SetTrigger("Hit");
            if (health <= 0 && !dead)
            {
                dead = true;
                //Disable Box Collider of parent Object
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //only trees have animators so must disable their animator for the trees to fall 
                if (this.gameObject.tag == "Tree")
                {
                    this.transform.GetComponentInChildren<Animator>().enabled = false;
                }
                foreach (var component in this.transform.GetComponentsInChildren<DestructableComponent>())
                {
                    component.SelfDestruct(this.transform);
                }
                if (GetComponent<EnemySpawn>())
                {

                    this.GetComponent<EnemySpawn>().enabled = false;
                }

                
                
            }

        }
    }
    void LateUpdate()
    {
        if (health <= 0)
        {
            GiantMeleeAttack.player.RemoveSelfFromAttackableList(this.gameObject);
            StartCoroutine("Delay");
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);

    }
    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToStartFade);
        while (true)
        {
            

            foreach (var item in GetComponentsInChildren<SpriteRenderer>())
            {
                Color c = item.color;
                Debug.Log(c);
                c.a /= 1.05f;
                item.color = c;
                if (c.a < 0.04)
                {
                    Destroy(this.gameObject);
                }
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
        //Destroy(this.gameObject);

    }
    void OnDestroy()
    {

    }
}

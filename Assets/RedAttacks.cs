using UnityEngine;
using System.Collections;

public class RedAttacks : MonoBehaviour {
    public GameObject launchingDevice;
    public float launchForce;
    public GameObject projectilePrefab;
    public bool isInRange;
    public float checkRangeEvery;
    public float range;
    public float enemyAccuracy;
    public float timeToReachTarget;
    public GeneralPooling pooler;
    public Vector2 velocity;
    public Animator myAnimator;
	// Use this for initialization
	void Start () 
    {
        myAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void ThrowyAttack()
    {
       GameObject prefab = Instantiate(projectilePrefab, launchingDevice.transform.position, Quaternion.identity) as GameObject;
    }

    IEnumerator CheckForGaint()
    {

        while (true)
        {
            if (Vector2.Distance(Player.player.transform.position, this.transform.position) < range)
            {
                isInRange = true;

            }
            else
            {
                isInRange = false;
            }
            yield return new WaitForSeconds(checkRangeEvery);
        }
    }

    public void shootGiant()
    {

        float x = (Player.player.transform.position.x - this.transform.position.x) + Random.Range(-enemyAccuracy, enemyAccuracy);
        float y = (Player.player.transform.position.y - this.transform.position.y);
        //float x = (Player.player.gameObject.GetComponent<CircleCollider2D>().radius*0.5f - this.transform.position.x) + Random.Range(-enemyAccuracy, enemyAccuracy);
        //float y = (Player.player.gameObject.GetComponent<CircleCollider2D>().radius * 0.5f - this.transform.position.y);

        Vector2 gravity = -((Vector2)this.transform.position).normalized * GameState.gravity;
        float vx = x / timeToReachTarget - .5f * gravity.x * timeToReachTarget;
        float vy = y / timeToReachTarget - .5f * gravity.y * timeToReachTarget;
        var instance = pooler.CreateObject(launchingDevice.transform.position, Quaternion.LookRotation(new Vector2(vx, vy))) as GameObject;
        velocity.x = vx;
        velocity.y = vy;
        myAnimator.SetTrigger("Attack");
        instance.GetComponent<Rigidbody2D>().velocity = (new Vector2(vx, vy));
    }

}

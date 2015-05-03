using UnityEngine;
using System.Collections;

public class RangedGroundEnemy : MonoBehaviour
{
    public Vector2 ArrotVelocity;
    public float attacksEvery = 2f;
    public float range = 4f;
    //Coroutine checks if player is in range every number of seconds so that check doesnt happen in update.
    public float checkRangeEvery = .5f;
    public bool isInRange = false;
    //The ammount of time for the projectile to reach the player from the enemy.
    public float timeToReachTarget=2;
    public GameObject projectile;
    //Position of the object that will instantiate the projectile i.e. Bow, Hand, Cannon, etc...
    public Transform launchingDevice;
    //Random range around the player that the enemy will shoot towards 0 is very accurate the higher the number the less accurate the enemy.
    public float enemyAccuracy;

    public SurfaceMovingObject mySurfaceMovingObject;
    Animator myanimator;
    void Start(){
        mySurfaceMovingObject = this.GetComponent<SurfaceMovingObject>();
        myanimator = GetComponent<Animator>();
        StartCoroutine("CheckForGaint");
        StartCoroutine("attack");
        StartCoroutine("Seek");
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
        var instance = Instantiate(projectile, launchingDevice.transform.position, Quaternion.LookRotation(new Vector2(vx,vy))) as GameObject;
        ArrotVelocity.x = vx;
        ArrotVelocity.y = vy;
        myanimator.SetTrigger("Attack");
        instance.GetComponent<Rigidbody2D>().velocity = (new Vector2(vx, vy));
    }
  
    /*
     * This function checks if the player is in range every checksRangeEvery:float seconds
     */
    IEnumerator CheckForGaint()
    {

        while (true)
        {
            if (Vector2.Distance(Player.player.transform.position, this.transform.position) < range) {
                isInRange = true; ;
            }
            else
            {
                isInRange = false;
            }
            yield return new WaitForSeconds(checkRangeEvery);
        }
    }
    // This function attacks the player every attackevery:float seconds.
    IEnumerator attack()
    {
        while (true)
        {
            if (isInRange)
            {
                shootGiant();
            }
            yield return new WaitForSeconds(attacksEvery);
        }
    }
    IEnumerator Seek()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isInRange)
            {
                this.transform.localScale = new Vector3(-PlanetMath.ShortestDirection(this.transform.position, Player.player.transform.position), 1, 1);
                mySurfaceMovingObject.StopMoving();
            }
            else
            {
                mySurfaceMovingObject.Move(PlanetMath.ShortestDirection(this.transform.position, Player.player.transform.position));
            }
        }
    }
}

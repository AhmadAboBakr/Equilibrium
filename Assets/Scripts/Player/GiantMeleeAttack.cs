using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GiantMeleeAttack : MonoBehaviour {
    public static GiantMeleeAttack player;
    public List<GameObject> attackables;
    public float damagePerHit=1;
    public float attackForce;
    public float staminaPerHit;
    void Awake()
    {
        if (!player) player = this;
    }
	
    void Start () {
        
        attackables = new List<GameObject>();
	}
	
	void Update () 
    {

	}

    public void Attack()
    {
        //if (Player.player.stamina > staminaPerHit)
        //{
        //    //Decrease stamina based on staminaPerHit
        //    Player.player.Stamina -= staminaPerHit;
        //    foreach (var attackable in attackables)
        //    {

        //        //if Attacked objects are rubble or enemies and contain rigidbody components then add force on them
        //        if (attackable.gameObject.tag == ("Rubble"))
        //        {
        //            attackable.GetComponent<Rigidbody2D>().AddForce((attackable.transform.position - this.transform.position * 0.95f).normalized * attackForce);
        //        }
        //        //Decrease the health of the destructible Object
        //        if (attackable.gameObject.tag == ("Building") || attackable.gameObject.tag == ("Tree"))
        //        {
        //            attackable.GetComponent<DestructableObject>().Health -= damagePerHit;
        //        }
        //        if (attackable.gameObject.tag == ("Enemy"))
        //        {
        //            attackable.GetComponent<DestructibleEnemy>().Health--;
        //        }
        //    }
        if (Player.player.stamina > 0.45f) {
            Player.player.animator.SetTrigger("attacked");
        }
    }

    public void doAttack()
    {

        if (Player.player.stamina > staminaPerHit)
        {
            Debug.Log(Player.player.stamina + " :   " + staminaPerHit);
            //Decrease stamina based on staminaPerHit
            Player.player.Stamina -= staminaPerHit;
            foreach (var attackable in attackables)
            {

                //if Attacked objects are rubble or enemies and contain rigidbody components then add force on them
                if (attackable.gameObject.tag == ("Rubble"))
                {
                    attackable.GetComponent<Rigidbody2D>().AddForce((attackable.transform.position - this.transform.position * 0.95f).normalized * attackForce);
                }
                //Decrease the health of the destructible Object
                if (attackable.gameObject.tag == ("Building") || attackable.gameObject.tag == ("Tree"))
                {
                    attackable.GetComponent<DestructableObject>().Health -= damagePerHit;
                }
                if (attackable.gameObject.tag == ("Enemy"))
                {
                    attackable.GetComponent<DestructibleEnemy>().Health--;
                }
            }
        }
        else
        {

        }
    }
    
    //Add object into list of attackable objects if they are Trees Enemies Buildings or Rubble
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree") || other.CompareTag("Enemy") || other.CompareTag("Building") || other.CompareTag("Rubble"))
        {
            //Add it into the list only one time
            if (!attackables.Contains(other.gameObject))
            {
                attackables.Add(other.gameObject);
                //Boolean is used in DestructibleObject script so that if it dies, it is removed from the list

                
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //If something leaves the trigger then it is removed from the list
        if (attackables.Contains(other.gameObject))
            attackables.Remove(other.gameObject);
    }

    public void RemoveSelfFromAttackableList(GameObject other)
    {
        //function used to remove objects from the list if it contains them
        if (attackables.Contains(other))
        {
            attackables.Remove(other);
        }
    }
}

/*
 * InvalidOperationException: Collection was modified; enumeration operation may not execute.
System.Collections.Generic.List`1+Enumerator[UnityEngine.GameObject].VerifyState () (at /Users/builduser/buildslave/mono-runtime-and-classlibs/build/mcs/class/corlib/System.Collections.Generic/List.cs:778)
System.Collections.Generic.List`1+Enumerator[UnityEngine.GameObject].MoveNext () (at /Users/builduser/buildslave/mono-runtime-and-classlibs/build/mcs/class/corlib/System.Collections.Generic/List.cs:784)
GiantMeleeAttack.doAttack () (at Assets/Scripts/Player/GiantMeleeAttack.cs:60)

 */
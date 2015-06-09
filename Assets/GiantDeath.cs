using UnityEngine;
using System.Collections;

public class GiantDeath : MonoBehaviour {
    public static GiantDeath instance;
    Animator myAnimator;
    public bool isDead;
    GiantMovementController myMovement;
	// Use this for initialization
	void Start () 
    {
        isDead = false;
        if(!instance)
        {
            instance = this;
        }
        myAnimator = this.GetComponentInChildren<Animator>();
        myMovement = this.GetComponentInChildren<GiantMovementController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Die()
    {
        myAnimator.enabled = false;
        myMovement.enabled = false;
        foreach (var item in GetComponentsInChildren<DestructableComponent>())
        {
            item.SelfDestruct(this.transform);
            item.GetComponent<Rigidbody2D>().AddForce((item.transform.position - this.transform.position).normalized * 13, ForceMode2D.Impulse);
            isDead = true;
        }
    }
}

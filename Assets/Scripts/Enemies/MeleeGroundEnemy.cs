﻿using UnityEngine;
using System.Collections;

public class MeleeGroundEnemy : MonoBehaviour
{
    public float attacksEvery = 1.5f;
    public float damage;
    public bool GiantInAttackArea = false;
    Animator myAnimator;
    SurfaceMovingObject mySurfaceMovingObject;
    void Start()
    {
        mySurfaceMovingObject=this.GetComponent<SurfaceMovingObject>();
        myAnimator = this.GetComponent<Animator>();
        StartCoroutine("Seek");
    }
    
    IEnumerator attack()
    {
        while (true)
        {
            myAnimator.SetTrigger("Attack");
            Player.player.HealthPoints -= damage;
            yield return new WaitForSeconds(attacksEvery);
        }
    }
    void getPicked(float duration)
    {

    }
    void takeDamage(float damage)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("attack");
            GiantInAttackArea=true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("attack");
            GiantInAttackArea = false;
        }
    }

    IEnumerator Seek()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (GiantInAttackArea)
            {
                mySurfaceMovingObject.StopMoving();
            }
            else
            {
                mySurfaceMovingObject.Move(PlanetMath.ShortestDirection(this.transform.position, Player.player.transform.position));
            }
        }
    }

}
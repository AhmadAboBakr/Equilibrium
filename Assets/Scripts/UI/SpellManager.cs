using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum spell
{
    telekinesis,
    shockWave,
    fireBall,
    melee
}
public class SpellManager : MonoBehaviour {
    public static SpellManager manager;
    spell currentSpell;
    public List<Spell> allSpells;
    GameObject currentlyEnabledSpell;
    public GameObject giantHead;
    public Image[] spellIcons;
	// Use this for initialization
    void Awake()
    {
        if (!manager)
        {
            manager = this;
        }
    }
	void Start () {
        
        currentSpell = spell.telekinesis;
        
	}
	
	void Update () 
    {
        for (int i = 0; i < allSpells.Count; i++)
        {
            if(allSpells[i].currentCooldown > 0)
            {
                allSpells[i].currentCooldown -= Time.deltaTime;
            }
            if(allSpells[i].currentCooldown < 0 )
            {
                allSpells[i].currentCooldown = 0;
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            currentSpell = spell.melee;
            startCasting();
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentSpell = spell.telekinesis;
        }
        if (Input.GetKey(KeyCode.W))
        {
            currentSpell = spell.shockWave;
            startCasting();
        }


	}
    public void startCasting()
    {
        Vector3 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,10);
        Vector3 playerPos = Player.player.transform.position;

        switch (currentSpell)
        {
            case spell.telekinesis:
                if (Player.player.Mana >= allSpells[0].manaCost)
                {
                    currentlyEnabledSpell = GameObject.Instantiate(allSpells[0].spellPrefab, mousePosition, Quaternion.identity) as GameObject;
                }
                break;
            case spell.shockWave:
                if (allSpells[1].currentCooldown==0 && Player.player.Mana>=allSpells[1].manaCost)
                {
                    currentlyEnabledSpell = GameObject.Instantiate(allSpells[1].spellPrefab, playerPos, Quaternion.identity) as GameObject;
                    
                    allSpells[1].currentCooldown = allSpells[1].totalCooldown;
                    Player.player.Mana -= allSpells[1].manaCost;
                }
                break;
            case spell.fireBall:
                if (allSpells[2].currentCooldown == 0 && Player.player.Mana >= allSpells[2].manaCost)
                {
                    allSpells[2].currentCooldown = allSpells[2].totalCooldown;
                    currentlyEnabledSpell = GameObject.Instantiate(allSpells[2].spellPrefab, giantHead.transform.position, Quaternion.identity) as GameObject;
                    currentlyEnabledSpell.GetComponent<Rigidbody2D>().velocity = Player.player.transform.parent.GetComponent<Rigidbody2D>().velocity;

                    currentlyEnabledSpell.GetComponent<Rigidbody2D>().AddForce((mousePosition - playerPos).normalized*40 ,ForceMode2D.Impulse);
                    Player.player.Mana -= allSpells[2].manaCost;
                }
                break;
            case spell.melee:
                GiantMeleeAttack.player.Attack();
                break;
            default:
                break;
        }
    }
    public void stopCasting()
    {
        switch (currentSpell)
        {
            case spell.telekinesis:
                GameObject.Destroy(currentlyEnabledSpell);
                break;
            default:
                break;
        }

    }
    public void SetCurrentSpell(int index)
    {
        
            currentSpell = (spell)index;
            foreach (var item in spellIcons)
            {
                var color = item.color;
                color = Color.grey;
                item.color = color;
            }
            spellIcons[index].color = Color.white;
          
    }
    IEnumerator DestroySpell()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject.Destroy(currentlyEnabledSpell);
    }

}

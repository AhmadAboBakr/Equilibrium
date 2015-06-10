using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //Static class to access player properties at anytime 
    public static Player player;
    private float healthPoints;
    public float maxHealthPoints = 10;
    public float mana;
    public float maxMana = 10;
    public float stamina;
    public float maxStamina = 10;

    public float healthRegen;
    public float manaRegen;
    public float staminaRegen;

    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;

    public Animator animator;
    public bool attacked;
    public Text manaText, healthText, staminaText;
    public float timer;

    void Awake()
    {
        if(!player)
        {
            player = this;
        }
        healthPoints = maxHealthPoints;
        mana = maxMana;
        stamina = maxStamina;
 
    }
    public void Start()
    {
        
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Slider>();
        staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Slider>();
        //animator = transform.GetChild(0).GetComponent<Animator>();
        healthText =  GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        manaText =    GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        staminaText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }
   
    

    public float HealthPoints
    {
        get
        {
            return healthPoints;
        }
        set
        {
            if(value<healthPoints)
            {
                attacked = true;
            }
            healthPoints = value;
            if(healthPoints <=0)
            {
                GiantDeath.instance.Die();
            }
            healthBar.value = healthPoints / maxHealthPoints;

        }
    }
    public float Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
            manaBar.value = mana / maxMana;
        }
    }
    public float Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
            staminaBar.value = stamina / maxStamina;
        }
    }




    
    public void Update()
    {
        if(attacked)
        {
            timer += Time.deltaTime;
            if(timer > 3)
            {
                attacked = false;
                timer = 0;
            }
        }
        if(healthPoints < maxHealthPoints && !attacked)
            HealthPoints += healthRegen * Time.deltaTime;
        if(mana < maxMana)
            Mana += manaRegen * Time.deltaTime;
        if(stamina < maxStamina)
            Stamina += staminaRegen * Time.deltaTime;
    }


}

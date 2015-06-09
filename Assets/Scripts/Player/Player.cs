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
    public Text healthText, staminaText, manaText;
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
        animator = GetComponent<Animator>();
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        manaText = GameObject.FindGameObjectWithTag("ManaText").GetComponent<Text>();
        staminaText = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<Text>();
    }
   
    

    public float HealthPoints
    {
        get
        {
            return healthPoints;
        }
        set
        {
            healthPoints = value;
            if(healthPoints <=0)
            {
                GiantDeath.instance.Die();
            }
            healthBar.value = healthPoints / maxHealthPoints;
            healthText.text = (int)(value / maxHealthPoints*100) + " %";
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
            manaText.text = (int)(value / maxHealthPoints * 100) + " %";

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
            staminaText.text = (int)(value / maxStamina * 100) + " %";

        }
    }




    
    public void Update()
    {
        if(healthPoints < maxHealthPoints)
            HealthPoints += healthRegen * Time.deltaTime;
        if(mana < maxMana)
            Mana += manaRegen * Time.deltaTime;
        if(stamina < maxStamina)
            Stamina += staminaRegen * Time.deltaTime;
    }


}

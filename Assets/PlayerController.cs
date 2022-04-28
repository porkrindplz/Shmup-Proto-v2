using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public int startingHealth;
    [SerializeField]
    public int health;
    [SerializeField]
    public int points;
    [SerializeField]
    public int streak;
    [SerializeField]
    public float timer;

    [SerializeField]
    public float winstreak;

    [SerializeField]
    public float speed;    

    private bool isPlaying;
    private bool invincible;
    private float startingInvincibilityTime = 2;
    private float takeDamageInvincibilityTime = 1;

    [SerializeField]
    private GameObject gunMid;
    [SerializeField]
    private GameObject bulletMid;
    private bool gunSidesActive;
    [SerializeField]
    private GameObject gunR;
    [SerializeField]
    private GameObject gunL;
    [SerializeField]
    private GameObject bulletSides;

    private int damage;

    private WeaponAction weaponAction;
    private CharacterMovement characterMovement;
    private DamageController damageController;



    private void Awake()
    {

 
    }

    void Start()
    {
        invincible = true;
        Debug.Log("invincible - true? " + invincible);
        ResetHealth();
        isPlaying = true;
        gunSidesActive = true;
        weaponAction = GetComponent<WeaponAction>();
        characterMovement = GetComponent<CharacterMovement>();
        damageController = GetComponent<DamageController>();
        StartCoroutine("InvincibilityTime", startingInvincibilityTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            //Game Timer
            timer += Time.deltaTime; 

            //Movement Controls
            if (Input.GetKey(KeyCode.W))
            {
                characterMovement.MoveForward(speed);
            } else
            if (Input.GetKey(KeyCode.S))
            {
                characterMovement.MoveBack(speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                characterMovement.MoveLeft(speed);
            } else if (Input.GetKey(KeyCode.D))
            {
                characterMovement.MoveRight(speed);
            }

            //Attack Controls
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
            {
                weaponAction.Shoot(bulletMid, gunMid);
                if (gunSidesActive)
                {
                    weaponAction.Shoot(bulletSides, gunL);                    
                    weaponAction.Shoot(bulletSides, gunR);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.transform.parent != null)
            {
                damage = other.gameObject.transform.parent.GetComponent<DamageController>().damage;
            }else
            {
                damage = other.gameObject.transform.GetComponent<DamageController>().damage;
            }
            TakeDamage(damage);
        }
        else
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0 && !invincible)
        {
            health -= damage;
            ResetStreak();
            damageController.FlashRed(takeDamageInvincibilityTime);
            invincible = true;
            StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
        }

        if (health <= 0)
        {
            HasDied();
        }
    }

    public void HasDied()
    {
        if (gameObject.CompareTag("Player"))
        {

            ResetStreak();
            //explosion animation
            //stop camera movement
            //pause game
            //death screen
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Enemy"))
        {
            //explostion animation
            Destroy(gameObject);
        }
    }

    public void ResetHealth()
    {
        health = startingHealth;    
    }
    public void ResetStreak()
    {
        streak = 0;
    }
    public void ResetTimer()
    {
        timer = 0;
    }

    IEnumerator InvincibilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}

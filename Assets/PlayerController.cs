using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int restartCount;
    [SerializeField]
    public int startingHealth;
    [SerializeField]
    public int health;
    [SerializeField]
    public static int points;
    [SerializeField]
    private int initPoints = 0;
    [SerializeField]
    public int streak;
    public static int topStreak;
    [SerializeField]
    public float timer;
    [SerializeField]
    public float winstreak;
    [SerializeField]
    public float speed;   
    [SerializeField]
    public bool isPlaying;

    public float startStreak = 10;
    public float secondStreak = 20;
    public float incrementalStreak;


    private bool invincible;
    private float startingInvincibilityTime = 2;
    private float takeDamageInvincibilityTime = 1;

    [SerializeField]
    private GameObject gunMid;
    [SerializeField]
    private GameObject bulletMid;
    public static bool gunSidesActive;
    [SerializeField]
    private GameObject gunR;
    [SerializeField]
    private GameObject gunL;
    [SerializeField]
    private GameObject bulletSides;
    [SerializeField]
    private float bulletSpeed;

    private int damage;

    private WeaponAction weaponAction;
    private CharacterMovement characterMovement;
    private DamageController damageController;

    [SerializeField]
    private GameObject weaponUpgrade;
    [SerializeField]
    private GameObject shieldUpgrade;
    [SerializeField]
    public bool shieldActive;
    private ParticleSystem shieldEffect;

    private bool upgradeSpawned;
    private bool upgrade2Spawned;
    public bool autoFire;

    private void Awake()
    {

 
    }

    void Start()
    {
        points = initPoints;
        invincible = true;
        ResetHealth();
        isPlaying = true;
        weaponAction = GetComponent<WeaponAction>();
        characterMovement = GetComponent<CharacterMovement>();
        damageController = GetComponent<DamageController>();
        shieldEffect = GetComponent<ParticleSystem>();
        StartCoroutine("InvincibilityTime", startingInvincibilityTime);
        ResetStreakGoal();
        shieldEffect.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (streak > topStreak)
            {
                topStreak = streak;
            }
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space) && autoFire)
            {
                weaponAction.Shoot(bulletMid, gunMid, bulletSpeed);
                if (gunSidesActive)
                {
                    weaponAction.Shoot(bulletSides, gunL, bulletSpeed);                    
                    weaponAction.Shoot(bulletSides, gunR, bulletSpeed);
                }
            }
            //ITEM UPGRADE
            if(!gunSidesActive && !shieldActive && streak >=startStreak && !upgradeSpawned)
            {
                Instantiate(weaponUpgrade, new Vector3(-5, transform.position.y, transform.position.z + 20), transform.rotation);
                Instantiate(shieldUpgrade, new Vector3(5, transform.position.y, transform.position.z + 20), transform.rotation);
                upgradeSpawned = true;
            } else if(!gunSidesActive && !shieldActive && streak >=incrementalStreak && upgradeSpawned && !upgrade2Spawned){
                Instantiate(weaponUpgrade, new Vector3(-5, transform.position.y, transform.position.z + 20), transform.rotation);
                Instantiate(shieldUpgrade, new Vector3(5, transform.position.y, transform.position.z + 20), transform.rotation);
                upgrade2Spawned = true;
                IncreaseStreakGoal();

            }
            else if (!gunSidesActive && shieldActive && streak >=incrementalStreak && upgradeSpawned && !upgrade2Spawned)
            {
                Instantiate(weaponUpgrade, new Vector3(0, transform.position.y, transform.position.z + 20), transform.rotation);
                upgrade2Spawned = true;
                IncreaseStreakGoal();
            }
            else if (gunSidesActive && !shieldActive && streak >=incrementalStreak && upgradeSpawned && !upgrade2Spawned)
            {
                Instantiate(shieldUpgrade, new Vector3(0, transform.position.y, transform.position.z + 20), transform.rotation);
                upgrade2Spawned = true;
                IncreaseStreakGoal();
            }

            if (shieldActive)
            {
                Debug.Log("SHIELD");
                if (shieldEffect.isStopped)
                {
                    shieldEffect.Play();
                }
            }
            if (health <= 0)
            {
                HasDied();
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerHit(other);
    }

    public void TriggerHit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.transform.parent != null)
            {
                damage = other.gameObject.transform.parent.GetComponent<DamageController>().damage;
            }
            else
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
            if (shieldActive)
            {
                //deactivate shield
                shieldActive = false;
                if (shieldEffect.isPlaying)
                {
                    shieldEffect.Stop();
                }
                upgrade2Spawned = false;

            }
            else
            {
                health -= damage;
                ResetStreak();
                damageController.FlashRed(takeDamageInvincibilityTime);
                invincible = true;
                StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
            }
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
            pauseMenu.isGameOver = true;
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
        ResetStreakGoal();
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

    public void ActivateGunSides()
    {
        gunSidesActive = true;
    }

    public void IncreaseStreakGoal()
    {
        incrementalStreak += secondStreak;
    }
    public void ResetStreakGoal()
    {
        incrementalStreak = secondStreak;
    }
    public static void PointsRising(int pointsTotal, int increments)
    {


    }

    public IEnumerator PointUp(int pointsTotal, int increments)
    {
        int pointCount = Mathf.RoundToInt(pointsTotal / increments);
        for (int i = pointCount; i > 0; i--)
        {
            points += increments;
            yield return new WaitForSeconds(.01f);
        }

    }
}

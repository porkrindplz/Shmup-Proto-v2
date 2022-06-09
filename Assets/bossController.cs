using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float rotateSpeed = 1;
    [SerializeField]
    private int health;
    [SerializeField]
    private int lowHealth;
    [SerializeField]
    public int pointValue;

    private bool dead;
    private bool isPlaying;
    public bool enemyActive; //active on screen

    [SerializeField]
    private bool _isEndBoss;
    [SerializeField]
    private GameObject gunMid;
    [SerializeField]
    private GameObject bulletMid;
    [SerializeField]
    public bool midGunActive;
    [SerializeField]
    private float midBulletSpeed;
    [SerializeField]
    private float midCoolDownTime;
    private float midNextShotTime;
    [SerializeField]
    private GameObject gunL;
    [SerializeField]
    private GameObject bulletL;
    [SerializeField]
    private bool lGunActive;
    [SerializeField]
    private float lBulletSpeed;
    [SerializeField]
    private float lCoolDownTime;
    private float lNextShotTime;

    [SerializeField]
    private GameObject gunR;
    [SerializeField]
    private GameObject bulletR;
    [SerializeField]
    private bool rGunActive;
    [SerializeField]
    private float rBulletSpeed;
    [SerializeField]
    private float rCoolDownTime;
    private float rNextShotTime;
    public GameObject bodyToExplode;
    public GameObject[] objectToDestroy;

    private WeaponAction weaponAction;
    private RotateTowardsPlayer targetPlayer;
    private PlayerController playerController;
    private CharacterMovement characterMovement;
    private DamageController damageController;



    private bool invincible;
    private float takeDamageInvincibilityTime = 0.1f;
    private int damage;
    public bool isMoving = false;

    public ParticleSystem deathParticles;

    private Animator anim;
    private int attackAnim = 0;
    private int attackRnd;
    private float timeLeft = 0;

    private MovementPath movePath;

    private Renderer[] rend;
    private int numOfChildren;
    private GameObject children;

    public AudioSource _audio;
    public AudioClip hitSound, deathSound;
    

    // Start is called before the first frame update
    void Start()
    {
        lowHealth = Mathf.RoundToInt(health *.25f);
        isPlaying = true;
        weaponAction = GetComponent<WeaponAction>();
        characterMovement = GetComponent<CharacterMovement>();
        damageController = GetComponent<DamageController>();
        targetPlayer = GetComponentInChildren<RotateTowardsPlayer>();
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        movePath = FindObjectOfType<MovementPath>();
        _audio = GetComponent<AudioSource>();
        numOfChildren = transform.childCount;


    }

    void Update()
    {
        forwardSpeed = -movePath.speed;
        if (isPlaying & enemyActive)
        {
            //movePath.isMoving = false;
            characterMovement.MoveForward(forwardSpeed);
            if (gameObject.name.Contains("Tank"))
            {
                targetPlayer.TargetPlayer(rotateSpeed);
            }


            if (midNextShotTime <= Time.time && gunMid != null && bulletMid != null && midGunActive == true)
            {
                weaponAction.Shoot(bulletMid, gunMid, midBulletSpeed);
                midNextShotTime = Time.time + midCoolDownTime;
            }
            if (lNextShotTime <= Time.time && gunL != null && bulletL != null && lGunActive == true)
            {
                weaponAction.Shoot(bulletL, gunL, lBulletSpeed);
                lNextShotTime = Time.time + lCoolDownTime;
            }
            if (rNextShotTime <= Time.time && gunR != null && bulletR != null && rGunActive == true)
            {
                weaponAction.Shoot(bulletR, gunR, rBulletSpeed);
                rNextShotTime = Time.time + rCoolDownTime;
            }



            if (timeLeft <= 0 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Default"))
            {
                Random.seed = System.DateTime.Now.Millisecond;
                attackRnd = Random.Range(1, 5);
                attackAnim = attackRnd;
                anim.SetInteger("Attack", attackAnim);
            }

            if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                timeLeft = 2;
                anim.SetInteger("Attack", 0);

            }

            timeLeft -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.transform.parent != null)
            {
                damage = other.gameObject.transform.parent.GetComponent<DamageController>().damage;
            }
            else
            {
                damage = other.gameObject.transform.GetComponent<DamageController>().damage;
            }
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0 && !invincible)
        {
            damageController.FlashRed(takeDamageInvincibilityTime);
            _audio.PlayOneShot(hitSound);
            invincible = true;
            StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
            health -= damage;
        }

        if (health<=lowHealth)
        {
            for (int i = 0; i < numOfChildren; i++)
            {
                children = transform.GetChild(i).gameObject;
                if(children.GetComponent<Renderer>() != null)
                {
                    children.GetComponent<Renderer>().material.SetFloat("ColorPulse", 8);
                }
            }

            anim.SetBool("LowHealth", true);

        }

        if (health <= 0 && !dead)
        {
            _audio.PlayOneShot(deathSound);
            playerController.streak++;
            PlayerController.points+=pointValue;
            HasDied();
        }
    }

    public void HasDied()
    {
        if (gameObject.CompareTag("Player"))
        {
            dead = true;
            //explosion animation
            //stop camera movement
            //pause game
            //death screen
            Destroy(gameObject,0.5f);
        }
        if (gameObject.CompareTag("Enemy"))
        {

            if (_isEndBoss)
            {
                StartCoroutine(playerController.PointUp(5000, 10));
                Instantiate(deathParticles, bodyToExplode.transform.position, Quaternion.identity);
                for (int i = 0; i < objectToDestroy.Length; i++)
                {
                    Destroy(objectToDestroy[i]);
                }
                StartCoroutine("WaitTime", 3);
            }

        }
    }

    IEnumerator InvincibilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerWins();
        Destroy(gameObject, .1f);

    }

    void PlayerWins()
    {
        pauseMenu._levelComplete = true;
    }
}

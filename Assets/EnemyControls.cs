using UnityEngine;
using System.Collections;

public class EnemyControls : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotateSpeed = 1;

    [SerializeField]
    private int health;

    private bool dead;

    private bool isPlaying;

    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject bullet;

    public bool enemyActive; //active on screen

    private WeaponAction weaponAction;

    private RotateTowardsPlayer targetPlayer;
    private PlayerController playerController;
    private CharacterMovement characterMovement;
    private DamageController damageController;

    [SerializeField]
    private float coolDownTime;
    private float nextShotTime;

    private bool invincible;
    private float takeDamageInvincibilityTime = 0.1f;
    private int damage;


    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        isPlaying = true;
        weaponAction = GetComponent<WeaponAction>();
        characterMovement = GetComponent<CharacterMovement>();
        damageController = GetComponent<DamageController>();
        targetPlayer = GetComponentInChildren<RotateTowardsPlayer>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (isPlaying & enemyActive)
        {
            if (gameObject.name.Contains("Tank"))
            {
                characterMovement.MoveForward(speed);
                targetPlayer.TargetPlayer(rotateSpeed);
            }else

            if(gameObject.name.Contains("Flyer"))
            {
                Debug.Log("Forward!");
                characterMovement.sMovement(speed);
            }

            if (nextShotTime<= Time.time && gun != null && bullet !=null)
            {
                weaponAction.Shoot(bullet, gun);
                nextShotTime = Time.time + coolDownTime;
            }

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
            invincible = true;
            StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
            health -= damage;
        }

        if (health <= 0 && !dead)
        {
            playerController.streak++;
            playerController.points++;
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
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Enemy"))
        {
            //explostion animation
            Destroy(gameObject);
        }
    }

    IEnumerator InvincibilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

}

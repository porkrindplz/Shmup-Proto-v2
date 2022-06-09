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
    [SerializeField]
    public int pointValue;

    public Vector3 startPos;

    private bool dead;
    private bool isPlaying;

    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    public bool gunActive = true;
    [SerializeField]
    private float bulletSpeed;

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
    public bool isMoving = false;

    public ParticleSystem deathParticles;

    private AudioSource _audio;
    public AudioClip hitSound, deathSound;


    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        weaponAction = GetComponent<WeaponAction>();
        characterMovement = GetComponent<CharacterMovement>();
        damageController = GetComponent<DamageController>();
        targetPlayer = GetComponentInChildren<RotateTowardsPlayer>();
        playerController = FindObjectOfType<PlayerController>();
        _audio = GetComponent<AudioSource>();
        startPos = transform.position;
    }

    void Update()
    {
        if (isPlaying & enemyActive)
        {
            if (gameObject.name.Contains("Tank"))
            {
                characterMovement.MoveForward(speed);
                if(targetPlayer !=null)
                targetPlayer.TargetPlayer(rotateSpeed);
            }

            if(gameObject.name.Contains("S-Flyer"))
            {
                characterMovement.sMovement(speed);
            }
            if (gameObject.name.Contains("Sin"))
            {
                characterMovement.SinMovement(speed, startPos);
            }
           if (gameObject.name.Contains("Missile") && isMoving)
            {
                GetComponentInChildren<CharacterMovement>().LocalMoveForward(speed);
            }

            /*if(gameObject.name.Contains("Missile") && gunActive && !hasFired)
            {
                Debug.Log("fire!");
                weaponAction.LaunchChild(bullet, gun, bulletSpeed);
                hasFired = true;
                gunActive = false;
            } */
            if (nextShotTime<= Time.time && gun != null && bullet !=null && gunActive == true)
            {
                weaponAction.Shoot(bullet, gun, bulletSpeed);
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
            _audio.clip = hitSound;
            _audio.Play();
            invincible = true;
            StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
            health -= damage;
        }

        if (health <= 0 && !dead)
        {

            HasDied();
        }
    }

    public void HasDied()
    {
        if (gameObject.CompareTag("Player"))
        {
            dead = true;
            _audio.PlayOneShot(deathSound);
            //explosion animation
            //stop camera movement
            //pause game
            //death screen
            Destroy(gameObject,.05f);
        }
        if (gameObject.CompareTag("Enemy"))
        {
            playerController.streak++;
            PlayerController.points += pointValue;
            //enemyActive = false;
            //explostion animation
            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            if (childs <= 0)
            {
                _audio.PlayOneShot(deathSound);
                Destroy(gameObject, 1.0f);
            }
        }
    }

    IEnumerator InvincibilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

}

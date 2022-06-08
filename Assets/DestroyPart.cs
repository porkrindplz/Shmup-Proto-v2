using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPart : MonoBehaviour
{
    public GameObject[] objectToDestroy;
    public int health;
    private int damage;
    private bool invincible;
    private float takeDamageInvincibilityTime = 0.1f;
    private bool dead;
    private PlayerController playerController;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            invincible = true;
            StartCoroutine("InvincibilityTime", takeDamageInvincibilityTime);
            health -= damage;
        }

        if (health <= 0 && !dead)
        {
            boss.GetComponent<bossController>()._audio.PlayOneShot(boss.GetComponent<bossController>().deathSound);
            HasDied();
        }
    }

    public void HasDied()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(playerController.PointUp(1000, 50));
            Instantiate(boss.GetComponent<bossController>().deathParticles, transform.position, Quaternion.identity);
            for (int i = 0; i < objectToDestroy.Length; i++)
            {
                Destroy(objectToDestroy[i]);
            }
            Destroy(gameObject, .1f);
        }
    }
    IEnumerator InvincibilityTime(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }
}

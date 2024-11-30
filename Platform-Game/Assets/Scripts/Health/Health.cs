using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

   [Header("Health")]
   [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return; 
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            //Igrac je povredjen
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            //Igrac je mrtav
            if(!dead)
            {
                anim.SetTrigger("die");


                ////Igrac/Player
                //if(GetComponent<PlayerMovement>() != null)
                //    GetComponent<PlayerMovement>().enabled = false;

                ////Enemy
                //if(GetComponentInParent<EnemyPatrol>() != null)
                //     GetComponentInParent<EnemyPatrol>().enabled = false;

                //if(GetComponent<MeleeEnemy>() != null)
                //    GetComponent<MeleeEnemy>().enabled = false;

                foreach(Behaviour component in components)
                    component.enabled = false;
                

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9,true);
        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

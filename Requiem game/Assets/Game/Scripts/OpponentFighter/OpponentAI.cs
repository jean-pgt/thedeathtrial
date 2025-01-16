using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpponentAI : MonoBehaviour 
{
    [Header("Opponent Movement")]
    public float movementSpeed = 2f;
    public float rotationSpeed = 10f;
    public CharacterController characterController;
    public Animator animator;

    [Header("Opponent Fight")]
    public float attackCooldown = 0.5f;
    public int attackDamages = 5;
    public string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    public float dodgeDistance = 2f;
    public int attackCount = 0;
    public int randomNumber;
    public float attackRadius = 2f;
    public FightingController[] fightingController;
    public Transform[] players;
    public bool isTakingDamage;
    private float lastAttackTime;

    [Header("Effects and Sound")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;

    public AudioClip[] hitSounds;

    [Header("Health")]
    public int maxHealth = 100;
    public int currenthealth;
    public HealthBar healthBar;


    void Start()
    {
  
        transform.position = new Vector3(-6, -1, -6);
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    void Awake()
    {
        currenthealth = maxHealth;
        healthBar.GiveFullHealth(currenthealth);

        createrandomnumber();
    }

    void Update()
    {
       
        /*if(attackCount == randomNumber)
        {
            attackCount = 0;
            createRandomNumber();
        }*/

        for (int i = 0; i < fightingController.Length; i++)
        {
            if (players[i].gameObject.activeSelf && Vector3.Distance(transform.position, players[i].position) <= attackRadius)
            {
                animator.SetBool("Walking", false);

                if (Time.time - lastAttackTime > attackCooldown)
                {
                    int randomAttackIndex = Random.Range(0, attackAnimations.Length);

                    if (!isTakingDamage)
                    {
                        PerformAttack(randomAttackIndex);
                    }

                    // Play hit/damage animation on the player
                    fightingController[i].StartCoroutine(fightingController[i].PlayHitDamageAnimation(attackDamages));

                }
            }
            else
            {

                if (players[i].gameObject.activeSelf)
                {
                    Vector3 direction = (players[i].transform.position - transform.position).normalized;
                    Debug.Log("Direction to player: " + direction);
                    characterController.Move(direction * movementSpeed * Time.deltaTime);
                    Quaternion targetrotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotationSpeed * Time.deltaTime);
                    animator.SetBool("Walking", true);
                    
                }
            }
        }
    }
    void PerformAttack(int attackIndex)
    {
    
            animator.Play(attackAnimations[attackIndex]);

            int damage = attackDamages;
            Debug.Log("Performed attack " + (attackIndex + 1) + " dealing " + damage + "damage");

            lastAttackTime = Time.time;

      
    }


    void PerformDodgeFront()
    {
        
        
            animator.Play("DodgeFrontAnimation");

            Vector3 dodgeDirection = -transform.forward * dodgeDistance;
                                                          
            characterController.SimpleMove(dodgeDirection);
       
    }

    void createrandomnumber()
    {
        randomNumber = Random.Range(1, 5);
    }

    public IEnumerator PlayHitDamageAnimation(int takeDamage)
    {
        yield return new WaitForSeconds(0.5f);

        //play random hit sound
        if (hitSounds != null && hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            AudioSource.PlayClipAtPoint(hitSounds[randomIndex], transform.position);
        }

        //decvrease the heaalth
        currenthealth -= takeDamage;
        healthBar.SetHealth(currenthealth);


        if (currenthealth <= 0)
        {
            healthBar.SetHealth(0);

            Die();
        }

        animator.Play("HitDamageAnimation");
    }

    void Die()
    {
        Debug.Log("Opponent died.");
    }


    public void Attack1Effect()
    {
        attack1Effect.Play();
    }
    public void Attack2Effect()
    {
        attack2Effect.Play();
    }
    public void Attack3Effect()
    {
        attack3Effect.Play();
    }
    public void Attack4Effect()
    {
        attack4Effect.Play();
    }
}

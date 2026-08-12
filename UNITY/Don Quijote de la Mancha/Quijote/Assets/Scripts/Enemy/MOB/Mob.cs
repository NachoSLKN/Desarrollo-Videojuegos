using System.Collections;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed;
    public float range;
    public CharacterController Controller;
    public Transform player;
    private int health;
    private Animator anim;
    private bool isDead = false;

    private Color[] originalColors;


    public float detectionRange = 10f;   // rango donde te detecta
    public float followRange = 15f;      // rango donde deja de seguirte


    public float attackRange = 1.2f;
    public float attackCooldown = 1f;
    private bool canAttack = true;

    private bool isStunned = false; // no puede moverse mientras recibe daño
    public float hitStunTime = 0.4f; // tiempo de la animación de golpe



    private bool playerDetected = false;



    void Start()
    {
        anim = GetComponent<Animator>();
        health = 100;
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        originalColors = new Color[rends.Length];

        for (int i = 0; i < rends.Length; i++)
        {
            originalColors[i] = rends[i].material.color;
        }

        SetRagdollState(false);   // Desactiva ragdoll al inicio

    }

    void Update()
    {
        if (isDead) return;
        if (isStunned)
        {
            anim.SetFloat("Speed", 0f);
            return; // no moverse ni atacar mientras recibe daño
        }

        float distance = Vector3.Distance(transform.position, player.position);

        // --- DETECCIÓN ---
        if (distance <= detectionRange)
            playerDetected = true;

        // Si ya te detectó pero te fuiste demasiado lejos -> dejar de seguir
        if (playerDetected && distance > followRange)
        {
            playerDetected = false;
            anim.SetFloat("Speed", 0f);
            return;
        }

        // Si todavía no te detecta, nos quedamos quietos
        if (!playerDetected)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        // --- ATAQUE ---
        if (distance <= attackRange)
        {
            attack();
            anim.SetFloat("Speed", 0f);
            return;
        }

        // --- PERSECUCIÓN ---
        chase();
        updateAnimation();
    }




    void attack()
    {
        if (!canAttack || isStunned) return;
        anim.SetTrigger("Attack");
        player.GetComponent<Fighter>().TakeDamage(10); // inflige 10 de daño al jugador
        Debug.Log("Enemy attacked the player!");
        StartCoroutine(AttackDelay());

    }


    


    private IEnumerator AttackDelay()
    {

        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }

    public void getHit(int damage)
    {
        if (isDead) return;

        health -= damage;

        anim.SetTrigger("Hit");
        StartCoroutine(HitStun()); // bloqueamos movimiento/ataque temporalmente

        if (health <= 0)
            Die();
    }


    IEnumerator HitStun()
    {
        isStunned = true;
        yield return new WaitForSeconds(hitStunTime);
        isStunned = false;
    }


    void chase()
    {
        transform.LookAt(player.position);
        Controller.SimpleMove(transform.forward * speed);
    }

    void updateAnimation()
    {
        float velocity = Controller.velocity.magnitude;

        if (velocity > 0.2f)
        {
            anim.SetFloat("Speed", 1f); // correr
        }
        else if (velocity < 0.05f)
        {
            anim.SetFloat("Speed", 0f); // idle
        }
    }

    void OnMouseOver()
    {
        if (isDead) return;

        player.GetComponent<Fighter>().opponent = gameObject;

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.material.color = Color.red;
    }

    void OnMouseExit()
    {
        if (isDead) return;

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.material.color = Color.white;
    }




    public void SetRagdollState(bool active)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (var rb in bodies)
            rb.isKinematic = !active;

        foreach (var col in colliders)
            if (col.gameObject != this.gameObject)   // deja el collider principal si es necesario
                col.enabled = active;

        anim.enabled = !active;
        Controller.enabled = !active;
    }

    void Die()
    {
        isDead = true;
        speed = 0; // para que deje de moverse
        Controller.enabled = false;
        anim.enabled = false;
        SetRagdollState(true);
        Destroy(gameObject, 20f);
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.color = originalColors[i];
        }

    }


}

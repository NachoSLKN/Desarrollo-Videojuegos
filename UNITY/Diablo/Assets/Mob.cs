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


    void Start()
    {
        anim = GetComponent<Animator>();
        health = 100;
        SetRagdollState(false);   // Desactiva ragdoll al inicio

    }

    void Update()
    {

        if (isDead) return;   //  No moverse si está muerto
        chase();
        updateAnimation();
        Debug.Log(health);
    }

    public void getHit(int damage)
    {
        health -= damage;
        Debug.Log(health);

        if (health <= 0)
            Die();
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
        player.GetComponent<Fighter>().opponent = gameObject;
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
        speed = 0; // para que deje de moverse
        SetRagdollState(true);
        Destroy(gameObject, 20f);

    }


}

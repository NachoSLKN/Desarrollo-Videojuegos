using UnityEngine;

public class Fighter : MonoBehaviour
{
    public GameObject opponent;
    private Animator anim;

    public int damage = 30;
    private bool attacking = false;

    public float attackDelay = 1f; // tiempo que tarda el golpe en aplicarse

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!attacking && opponent != null)
            {
                attacking = true;
                anim.SetTrigger("Attack");
                StartCoroutine(DelayedDamage());
            }
        }
    }

    private System.Collections.IEnumerator DelayedDamage()
    {
        // Esperamos el tiempo del delay temporal
        yield return new WaitForSeconds(attackDelay);

        // Aplicamos daño si todavía hay un enemigo válido
        if (opponent != null)
        {
            opponent.GetComponent<Mob>().getHit(damage);
            Debug.Log("Daño aplicado tras delay");
        }

        // El ataque termina y se puede volver a atacar
        attacking = false;
    }
}

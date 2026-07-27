using UnityEngine;

public class Fighter : MonoBehaviour
{
    public GameObject opponent;
    private Animator anim;

    public int damage = 30;
    private bool attacking = false;
    public bool IsAttacking
    {
        get { return attacking; }
    }

    public float attackDelay = 1f; // tiempo que tarda el golpe en aplicarse
    public float range;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
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


    bool inRange()
    {
        if (opponent == null)
            return false;

        return Vector3.Distance(opponent.transform.position, transform.position) <= range;
    }


    bool isFacingOpponent()
    {
        if (opponent == null)
            return false;

        Vector3 dirToOpponent = (opponent.transform.position - transform.position).normalized;

        // Ángulo entre jugador y enemigo
        float angle = Vector3.Angle(transform.forward, dirToOpponent);

        // Solo atacar si el enemigo está dentro de un ángulo de 60 grados
        return angle < 60f;
    }



    public void TakeDamage(int damage)
    {
        // Aquí iría la lógica para reducir la salud del jugador
        Debug.Log("Jugador recibió " + damage + " de daño.");
    }
}

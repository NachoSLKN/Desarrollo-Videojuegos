using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    private Health health;
    private Animator animationController;
    [SerializeField] private GameObject bossSprite;
        [SerializeField] private int attackDamage = 5;

    private Vector3 originalPosition;

    private void Awake()
    {
        health = GetComponent<Health>();
        animationController = bossSprite.GetComponent<Animator>();
    }

    private void Start()
    {
        originalPosition = bossSprite.transform.position;
    }

   private void OnEnable()
   {
    BossEvents.OnBossHit += HandleBossHit;
    TurnEvents.OnBossTurnStart += Attack;
   }

   private void OnDisable()
   {
    BossEvents.OnBossHit -= HandleBossHit;
        TurnEvents.OnBossTurnStart -= Attack;

   }

private void Attack()
{
    StartCoroutine(BossAttackAnimation());
}

    private void HandleBossHit(CardData cardData)
    {
        health.TakeDamage(cardData.attackPower);
        if(!health.IsAlive())
        {
            //Die
            Die();
        }


    }


    private void Die()
     {

        animationController.Play("Die");
        BossEvents.BossDeath();


     }

     private IEnumerator BossAttackAnimation()
    {
        Vector3 targetPosition = originalPosition + new Vector3(-4f, 0,0);

        float duration = .5f;
        float timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            bossSprite.transform.position = Vector3.Lerp(originalPosition, targetPosition, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        animationController.Play("Attack");
        PlayerEvents.PlayerHit(attackDamage);


        yield return new WaitForSeconds(.5f);

        timeElapsed = 0f;
        while(timeElapsed < duration)
        {
            bossSprite.transform.position = Vector3.Lerp(targetPosition, originalPosition, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

}

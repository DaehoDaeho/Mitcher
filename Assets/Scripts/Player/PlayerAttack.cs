using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;  // 공격의 중심 위치.

    [SerializeField]
    private float attackRange = 0.5f;   // 공격 범위.

    [SerializeField]
    private int attackDamage = 10;

    [SerializeField]
    private LayerMask enemyLayerMask;   // 공격 대상이 될 오브젝트의 레이어를 지정.

    [SerializeField]
    private float attackCooldown = 0.5f;    // 공격 쿨타임.

    [SerializeField]
    private Animator animator;

    private float timeSinceLastAttack;  // 공격 쿨타임을 체크할 타이머 변수.

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.J) == true)
        {
            if(timeSinceLastAttack >= attackCooldown)
            {
                Attack();
                timeSinceLastAttack = 0.0f;
            }
        }
    }

    void Attack()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);

        if(hitEnemies != null)
        {
            for(int i=0; i<hitEnemies.Length; ++i)
            {
                EnemyHealth enemyHealth = hitEnemies[i].GetComponent<EnemyHealth>();
                if(enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

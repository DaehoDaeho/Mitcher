using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private float detectionRange = 5.0f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer sr;

    private void Awake()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        // 태그 정보를 이용해서 플레이어 오브젝트를 찾아온다.
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if(go != null)
        {
            target = go.transform;
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }

        Vector2 enemyPosition = rb.position;
        Vector2 targetPosition = target.position;

        Vector2 toTarget = targetPosition - enemyPosition;  // 대상의 방향 계산.
        float distanceToTarget = toTarget.magnitude;    // 대상과의 거리 계산.

        // 거리가 감지 가능 범위보다 크면 아무 처리도 하지 않는다.
        if(distanceToTarget > detectionRange)
        {
            if(animator != null)
            {
                animator.SetBool("Move", false);
            }

            return;
        }

        Vector2 moveDirection = toTarget.normalized;
        Vector2 moveAmount = moveDirection * moveSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = enemyPosition + moveAmount;
        rb.MovePosition(newPosition);

        if (animator != null)
        {
            animator.SetBool("Move", true);
        }

        UpdateDirection(moveDirection.x);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void UpdateDirection(float dirX)
    {
        if (sr == null)
        {
            return;
        }

        if (dirX < 0.0f)
        {
            sr.flipX = false;
        }
        else if (dirX > 0.0f)
        {
            sr.flipX = true;
        }
    }
}

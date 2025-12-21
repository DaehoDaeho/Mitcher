using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector2 movementInput = Vector2.zero;   // 플레이어가 방향 키를 입력했을 때 그 입력 값을 받아 계산한 방향 벡터를 저장.

    private void Awake()
    {
        if(playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }

        if(playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(horizontalInput, verticalInput);
        if (movementInput.sqrMagnitude > 1.0f)
        {
            movementInput = movementInput.normalized;   // 벡터의 정규화. 벡터의 크기를 1로 만든다. 방향 정보로 사용하기 위해서.
        }

        UpdateAnimation();
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        if(playerRigidbody != null)
        {
            Vector2 currentPosition = playerRigidbody.position;
            Vector2 moveAmount = movementInput * moveSpeed * Time.fixedDeltaTime;
            Vector2 newPosition = currentPosition + moveAmount;

            playerRigidbody.MovePosition(newPosition);
        }
    }

    void UpdateAnimation()
    {
        if(playerAnimator == null)
        {
            return;
        }

        float speedValue = movementInput.sqrMagnitude;

        playerAnimator.SetFloat("Speed", speedValue);
    }

    void UpdateDirection()
    {
        if(spriteRenderer == null)
        {
            return;
        }

        if(movementInput.x < 0.0f)
        {
            spriteRenderer.flipX = false;
        }
        else if(movementInput.x > 0.0f)
        {
            spriteRenderer.flipX = true;
        }
    }
}

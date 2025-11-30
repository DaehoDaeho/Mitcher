using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance = 1.0f;

    [SerializeField]
    private LayerMask interactionLayerMask;

    private Vector2 lastMoveDirection = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        UpdateLastMoveDirection();
        CheckInteractable();
        HandleInteractionInput();
    }

    void CheckInteractable()
    {
        Vector2 origin = transform.position;
        Vector2 direction = lastMoveDirection;
        float distance = interactionDistance;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, interactionLayerMask);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.ShowTooltip();
            }
        }
    }

    void TryInteract()
    {
        Vector2 origin = transform.position;
        Vector2 direction = lastMoveDirection;
        float distance = interactionDistance;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, interactionLayerMask);

        Debug.DrawRay(origin, direction * distance, Color.yellow, 0.5f);

        if(hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void HandleInteractionInput()
    {
        if(Input.GetKeyDown(KeyCode.F) == true)
        {
            TryInteract();
        }
    }

    void UpdateLastMoveDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        if(inputVector.sqrMagnitude > 0.0f)
        {
            lastMoveDirection = inputVector.normalized;
        }
    }
}

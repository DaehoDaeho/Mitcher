using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;

    public virtual void Interact(Transform owner)
    {
        Debug.Log("Interactable.Interact()가 호출되었습니다.");
    }

    public virtual void ShowTooltip()
    {

    }
}

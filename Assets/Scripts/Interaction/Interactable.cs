using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interactable.Interact()가 호출되었습니다.");
    }

    public virtual void ShowTooltip()
    {

    }
}

using System.Collections;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField]
    private SpriteRenderer chestSpriteRenderer;

    [SerializeField]
    private Color closedColor = Color.white;

    [SerializeField]
    private Color openedColor = Color.yellow;

    [SerializeField]
    private GameObject toolTip;

    private bool isOpened = false;

    private bool isShowedTooltip = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateVisual();

        if(toolTip != null)
        {
            toolTip.SetActive(false);
        }
    }

    void UpdateVisual()
    {
        if(chestSpriteRenderer == null)
        {
            return;
        }

        if(isOpened == true)
        {
            chestSpriteRenderer.color = openedColor;
        }
        else
        {
            chestSpriteRenderer.color = closedColor;
        }
    }

    public override void Interact()
    {
        isOpened = !isOpened;

        UpdateVisual();
    }

    public override void ShowTooltip()
    {
        if(isShowedTooltip == true)
        {
            return;
        }

        isShowedTooltip = true;

        if(toolTip != null)
        {
            toolTip.SetActive(true);
            StartCoroutine(HideTooltip());
        }
    }

    IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(2.0f);

        toolTip.SetActive(false);
    }
}

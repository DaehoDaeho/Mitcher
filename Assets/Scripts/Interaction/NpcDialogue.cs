using UnityEngine;

/// <summary>
/// NPC의 대화정보를 가지고 있고,
/// 플레이어가 상호작용(F키)을 했을 때 DialogueUI에게
/// 대화를 시작하라고 요청
/// </summary>
public class NpcDialogue : Interactable
{
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private DialogueUI dialogueUI;

    /// <summary>
    /// 플레이어가 npc에게 다가와서 상호작용을 요청했을 때
    /// 대사창 UI를 열고, npc의 이름과 대사 데이터를 전달한다
    /// </summary>
    public override void Interact(Transform owner)
    {
        if(dialogueUI == null)
        {
            return;
        }

        if(dialogueUI.IsOpen() == true)
        {
            return;
        }

        if(dialogueLines == null || dialogueLines.Length == 0)
        {
            return;
        }

        UpdateDirection(owner);
        dialogueUI.StartDialogue(npcName, dialogueLines);
    }

    void UpdateDirection(Transform owner)
    {
        if(spriteRenderer == null)
        {
            return;
        }

        if(owner.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if(owner.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }
}

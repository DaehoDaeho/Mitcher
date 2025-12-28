using UnityEngine;

/// <summary>
/// 슬라임 퀘스트를 주는 NPC를 위한 대화 시스템.
/// 
/// </summary>
public class QuestNpcDialogue : Interactable
{
    [SerializeField] private string npcName;    // 이름.

    [SerializeField] private string[] beforeQuestLines; // 퀘스트 시작 전 대사.
    [SerializeField] private string[] inProgressLines;  // 퀘스트 진행 중 대사.
    [SerializeField] private string[] completedLines;   // 퀘스트 완료 후 대사.

    [SerializeField] private DialogueUI dialogueUI; // 대사출력 UI.

    [SerializeField] private QuestManager questManager;

    public override void Interact(Transform owner)
    {
        if(dialogueUI == null)
        {
            return;
        }

        if(questManager == null)
        {
            return;
        }

        // 대사출력 UI가 이미 열려 있으면 아무것도 하지 않는다.
        if(dialogueUI.IsOpen() == true)
        {
            return;
        }

        // 현재 슬라임 퇴치 퀘스트의 상태를 문자열 형태로 가져온다.
        SlimeQuestState questState = questManager.GetSlimeQuestState();
        string[] linesToUse = null;

        switch(questState)
        {
            case SlimeQuestState.NotStarted:
                {
                    linesToUse = beforeQuestLines;  // 시작 전 대사 세팅.
                    questManager.StartSlimeQuest(); // 퀘스트 시작을 알림.
                }
                break;

            case SlimeQuestState.InProgress:
                {
                    linesToUse = inProgressLines;   // 진행 중 대사 세팅.
                }
                break;

            case SlimeQuestState.Completed:
                {
                    linesToUse = completedLines;    // 완료 대사 세팅.
                }
                break;
        }

        if(linesToUse == null || linesToUse.Length == 0)
        {
            return;
        }

        // 대사 출력 함수 호출.
        dialogueUI.StartDialogue(npcName, linesToUse);
    }
}

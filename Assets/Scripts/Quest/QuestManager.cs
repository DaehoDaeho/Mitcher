using TMPro;
using UnityEngine;

/// <summary>
/// 슬라임 퀘스트 상태를 나타내는 열거형.
/// NotStarted : 아직 퀘스트를 받지 않은 상태.
/// InProgress : 퀘스트를 받은 뒤 진행 중인 상태.
/// Completed : 목표를 달성해 퀘스트를 완료한 상태.
/// </summary>
public enum SlimeQuestState
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2
}

/// <summary>
/// 게임 전체의 퀘스트 진행 상황을 관리하는 클래스.
/// 퀘스트 상태(시작 전, 진행 중, 완료)를 저장하고,
/// 슬라임을 몇 마리 처치했는지 기록하고,
/// 퀘스트 진행 상황을 화면에 텍스트로 보여준다.
/// </summary>
public class QuestManager : MonoBehaviour
{
    [SerializeField] private int requiredSlimeKillCount = 3;    // 퀘스트 완료에 필요한 슬라임 퇴치 수.
    [SerializeField] private int currentSlimeKillCount = 0; // 현재까지 퇴치한 슬라임 수.

    [SerializeField] private TMP_Text questText;    // 퀘스트 진행상황을 표시할 텍스트.

    private SlimeQuestState slimeQuestState = SlimeQuestState.NotStarted;   // 현재 퀘스트 진행 상태.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeQuestState = SlimeQuestState.NotStarted;
        currentSlimeKillCount = 0;
        UpdateQuestUI();
    }

    /// <summary>
    /// 퀘스트 UI 텍스트를 현재 상태에 맞게 갱신.
    /// </summary>
    void UpdateQuestUI()
    {
        if(questText == null)
        {
            return;
        }

        if(slimeQuestState == SlimeQuestState.NotStarted)
        {
            questText.text = "Quest: (없음)";
        }
        else if(slimeQuestState == SlimeQuestState.InProgress)  // 진행 중일 경우.
        {
            questText.text = "Quest: Slime " + currentSlimeKillCount + "/" + requiredSlimeKillCount;
        }
        else
        {
            questText.text = "Quest: Slime " + currentSlimeKillCount + "/" + requiredSlimeKillCount + " (완료!)";
        }
    }

    /// <summary>
    /// 현재 슬라임 퀘스트가 완료되었는지 여부를 반환.
    /// </summary>
    /// <returns></returns>
    public bool IsSlimeQuestCompleted()
    {
        if(currentSlimeKillCount == requiredSlimeKillCount)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 현재 슬라임 퀘스트 상태를 외부에 알려준다.
    /// </summary>
    /// <returns></returns>
    public SlimeQuestState GetSlimeQuestState()
    {
        return slimeQuestState;
    }

    /// <summary>
    /// 슬라임을 죽였을 때 호출되는 통지 함수.
    /// 슬라임 퀘스트가 진행 중일 때만 현재 퇴치 수를 1 증가시킨다.
    /// </summary>
    public void ReportSlimeKilled()
    {
        // 현재 퀘스트 상태가 진행 중이 아니면 아무것도 하지 않는다.
        if(slimeQuestState != SlimeQuestState.InProgress)
        {
            return;
        }

        ++currentSlimeKillCount;    // 퇴치 수를 하나 증가시킨다.

        // 현재 퇴치한 수가 목표 퇴치 수를 초과했으면 현재 퇴치 수를 목표 퇴치 수로 고정.
        if(currentSlimeKillCount > requiredSlimeKillCount)
        {
            currentSlimeKillCount = requiredSlimeKillCount;
        }

        // 현재 퇴치 수가 목표 퇴치 수와 같으면 퀘스트 상태를 완료로 전환.
        if(currentSlimeKillCount == requiredSlimeKillCount)
        {
            slimeQuestState = SlimeQuestState.Completed;
        }

        // UI 갱신.
        UpdateQuestUI();
    }

    /// <summary>
    /// 슬라임 퀘스트를 시작할 때 호출.
    /// </summary>
    public void StartSlimeQuest()
    {
        // 현재 퀘스트 상태가 시작 전 상태가 아니면 아무것도 하지 않는다.
        if(slimeQuestState != SlimeQuestState.NotStarted)
        {
            return;
        }

        slimeQuestState = SlimeQuestState.InProgress;
        currentSlimeKillCount = 0;
        UpdateQuestUI();
    }
}

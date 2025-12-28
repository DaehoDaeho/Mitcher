using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 화면에 대화창을 띄워주는 역할을 하는 클래스
/// 대화창 패널 켜기/끄기
/// 이름과 대사 텍스트 바꾸기
/// '다음' 버튼을 눌렀을 때 다음 줄로 넘어가게 만든다
/// </summary>
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;   // 대화창 전체 패널 : 켜고 끄는 대상
    [SerializeField] private TMP_Text nameText; // 이름을 보여주는 텍스트
    [SerializeField] private TMP_Text bodyText; // 실제 대사 내용을 보여주는 텍스트
    [SerializeField] private Button nextButton; // '다음' 버튼

    private string[] currentLines = null;   // 현재 대화에서 사용할 대사 줄들을 저장하는 배열
    private int currentLineIndex = 0;   // 현재 몇번째 줄을 보여주고 있는지 나타내는 인덱스

    private bool isOpen = false;    // 현재 대화창이 열려 있는지 여부를 저장

    private void Awake()
    {
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if(nextButton != null)
        {
            nextButton.onClick.AddListener(OnClickNext);
        }
    }

    /// <summary>
    /// 새로운 대화를 시작할 때 호출
    /// 대화를 하는 사람의 이름과 대사 줄 배열을 받아서 내부에 저장하고, 대화창을 열고 첫번째 줄을 보여준다
    /// </summary>
    /// <param name="speakerName">대화를 하는 사람의 이름</param>
    /// <param name="lines">대사 줄들이 들어있는 문자열 배열</param>
    public void StartDialogue(string speakerName, string[] lines)
    {
        if(dialoguePanel == null || nameText == null || bodyText == null)
        {
            return;
        }

        if(lines == null || lines.Length == 0)
        {
            return;
        }

        currentLines = lines;
        currentLineIndex = 0;

        nameText.text = speakerName;
        bodyText.text = currentLines[currentLineIndex];

        dialoguePanel.SetActive(true);
        isOpen = true;
    }

    /// <summary>
    /// '다음' 버튼을 클릭했을 때 호출
    /// </summary>
    void OnClickNext()
    {
        if(isOpen == false)
        {
            return;
        }

        if(currentLines == null)
        {
            return;
        }

        ++currentLineIndex; // 다음 줄로 넘어가기 위해 인덱스를 증가

        if(currentLineIndex < currentLines.Length)
        {
            bodyText.text = currentLines[currentLineIndex];
        }
        else
        {
            CloseDialogue();
        }
    }

    /// <summary>
    /// 현재 대화창이 열려 있는지 여부를 반환
    /// </summary>
    public bool IsOpen()
    {
        return isOpen;
    }

    /// <summary>
    /// 대화창을 닫고, 상태를 초기화한다
    /// </summary>
    public void CloseDialogue()
    {
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        isOpen = false;
        currentLines = null;
        currentLineIndex = 0;
    }
}

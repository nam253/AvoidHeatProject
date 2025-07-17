using UnityEngine;
using UnityEngine.UI;

public class OXQuizManager : MonoBehaviour
{
    [Header("UI References")]
    public Text questionText;
    public Button oButton;
    public Button xButton;
    public Text feedbackText;

    [Header("Quiz Data")]
    public string[] questions;    // 예: { "태양은 별이다.", "바다는 민물이다." }
    public bool[] answers;        // 질문별 정답: true=O, false=X
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private int currentIndex = 0;

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        oButton.onClick.AddListener(() => OnAnswerSelected(true));
        xButton.onClick.AddListener(() => OnAnswerSelected(false));
        feedbackText.text = "";
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentIndex < questions.Length)
        {
            questionText.text = questions[currentIndex];
            feedbackText.text = "";
        }
        else
        {
            questionText.text = "퀴즈 끝! 수고하셨습니다.";
            oButton.gameObject.SetActive(false);
            xButton.gameObject.SetActive(false);
        }
    }

    void OnAnswerSelected(bool isO)
    {
        bool correct = answers[currentIndex];
        if (isO == correct)
        {
            GetComponent<AudioSource>().PlayOneShot(correctSound);
            feedbackText.text = "정답!";
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(wrongSound);
            feedbackText.text = "오답!";
        }

        currentIndex++;
        Invoke(nameof(ShowQuestion), 1f); // 1초 후 다음 질문 표시
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject popupPanel;
    public TMP_Text questionText;
    public Button option1Button;
    public Button option2Button;
    public TMP_Text option1Text;
    public TMP_Text option2Text;
    public TMP_Text moneyText;
    public TMP_Text longTermText;
    public GameObject feedbackPanel;
    public TMP_Text feedbackText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Initialize panels
        popupPanel.SetActive(false);
        feedbackPanel.SetActive(false);
    }

    // Renamed from ShowDecision to match your original
    public void ShowPopup(DecisionData decision)
    {
        popupPanel.SetActive(true);
        questionText.text = decision.question;
        option1Text.text = decision.option1Text;
        option2Text.text = decision.option2Text;

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => {
            GameManager.Instance.ApplyDecision(true, decision);
            ClosePopup(); // hide popup
            ShowFeedback(decision.option1Feedback);
            ResumePlayer(); // resume movement
        });

        option2Button.onClick.AddListener(() => {
            GameManager.Instance.ApplyDecision(false, decision);
            ClosePopup(); // hide popup
            ShowFeedback(decision.option2Feedback);
            ResumePlayer(); // resume movement
        });
    }

    public void UpdateMoneyDisplay(int money, int longTermValue)
    {
        moneyText.text = $"Cash: ${money}";
        longTermText.text = $"Investments: ${longTermValue}";
    }

    public void ShowFeedback(string message, bool isEndGame = false)
    {
        feedbackText.text = message;
        feedbackPanel.SetActive(true);
        if (!isEndGame) Invoke(nameof(HideFeedback), 3f);
    }


    private void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    private void ResumePlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var mover = player.GetComponent<LaneMover>();
            if (mover != null) mover.isPaused = false;
        }
    }

    private void HideFeedback() => feedbackPanel.SetActive(false);
}



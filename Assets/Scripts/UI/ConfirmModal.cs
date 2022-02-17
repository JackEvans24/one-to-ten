using TMPro;
using UnityEngine;

public class ConfirmModal : GameStep
{
    [Header("UI References")]
    [SerializeField] private TMP_Text modalText;
    [SerializeField] private TMP_Text answerText;

    public override void UpdateGameValues(CurrentTurnData turn)
    {
        this.modalText.text = TextProvider.GetModalText(turn);
    }

    public void UpdateAnswerText(string answer)
    {
        this.answerText.text = answer;
    }
}

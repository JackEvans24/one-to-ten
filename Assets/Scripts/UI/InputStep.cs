using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputStep : GameStep
{
    [Header("Text references")]
    [SerializeField] protected TMP_Text promptText;
    [SerializeField] protected TMP_InputField playerInput;
    [SerializeField] protected Button doneButton;

    public string PlayerInput { get => playerInput.text; }

    public override void UpdateGameValues(CurrentTurnData turn)
    {
        this.promptText.text = TextProvider.GetPromptText(turn);
        this.playerInput.text = string.Empty;
    }

    public void UpdateButtonOnTextChange()
    {
        doneButton.interactable = PlayerInput.Length > 0;
    }
}

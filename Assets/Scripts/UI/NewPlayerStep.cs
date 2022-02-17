using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerStep : GameStep
{
    [Header("UI References")]
    [SerializeField] private TMP_Text playerLabel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Image colourImage;
    [SerializeField] private Button nextButton;

    [Header("Settings")]
    [SerializeField] private GameSettings settings;

    public override void UpdateGameValues(CurrentTurnData turn)
    {
        base.UpdateGameValues(turn);

        this.playerLabel.text = "Player " + (GameManager.GetPlayerCount() + 1);
        this.nameInput.text = string.Empty;
        this.colourImage.color = this.settings.GetNextColour();
        this.SetNextButtonEnabled();
    }

    public void ChangeImageColour()
    {
        this.colourImage.color = this.settings.GetNextColour();
    }

    public void SetNextButtonEnabled() => this.nextButton.interactable = this.nameInput.text.Length > 0;

    public Player GetPlayer() => new Player(this.nameInput.text, this.colourImage.color);
}

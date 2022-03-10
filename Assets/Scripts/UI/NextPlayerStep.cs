using System.Collections;
using TMPro;
using UnityEngine;

public class NextPlayerStep : GameStep
{
    [Header("Text references")]
    [SerializeField] protected TMP_Text passToPlayerText;
    [SerializeField] protected CanvasGroup addPlayerButtonCanvas;
    [SerializeField] protected CanvasGroup nextButtonCanvas;
    [SerializeField] protected TMP_Text cycleButtonText;

    [HideInInspector] public bool AllowNewPlayers = true;
    private bool showingNewPlayerCanvas = true;

    public override void UpdateGameValues(CurrentTurnData turn)
    {
        if (this.AllowNewPlayers)
        {
            this.passToPlayerText.text = "Pass the device to the next player";
            this.cycleButtonText.text = "Back to " + TextProvider.GetPlayerNameLabel(GameManager.PeekNextPlayer());
        }
        else
        {
            this.passToPlayerText.text = "Pass the device to " + TextProvider.GetPlayerNameLabel(GameManager.PeekNextPlayer());
        }
    }

    public void NoNewPlayers()
    {
        this.AllowNewPlayers = false;
    }

    public override IEnumerator Hide()
    {
        yield return base.Hide();

        if (!this.AllowNewPlayers && this.showingNewPlayerCanvas)
            this.SwitchCanvases();
    }

    private void SwitchCanvases()
    {
        this.addPlayerButtonCanvas.alpha = 0;
        this.addPlayerButtonCanvas.interactable = false;
        this.addPlayerButtonCanvas.blocksRaycasts = false;

        this.nextButtonCanvas.interactable = true;
        this.nextButtonCanvas.blocksRaycasts = true;
        this.nextButtonCanvas.alpha = 1;

        this.showingNewPlayerCanvas = false;
    }
}

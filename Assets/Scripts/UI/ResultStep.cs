using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultStep : GameStep
{
    [Header("Text references")]
    [SerializeField] protected TMP_Text promptText;
    [SerializeField] protected TMP_Text playerNameText;
    [SerializeField] protected TMP_Text actualValueText;
    [SerializeField] protected TMP_Text guessedValueText;

    [Header("Other references")]
    [SerializeField] protected Button nextButton;
    [SerializeField] protected ParticleSystem congratsParticles;

    [Header("Reveal timings")]
    [SerializeField] protected float revealOffset = 0.6f;
    [SerializeField] protected float revealDuration = 0.4f;
    [SerializeField] protected Ease revealEasing = Ease.InSine;

    private bool isCorrectAnswer;

    public override void UpdateGameValues(CurrentTurnData turn)
    {
        this.promptText.text = TextProvider.GetResultText(turn);

        this.playerNameText.text = TextProvider.GetPlayerNameLabel(turn.CurrentPlayer) + ":";
        this.actualValueText.text = TextProvider.GetSliderValueText(turn.ActualValue);
        this.guessedValueText.text = TextProvider.GetSliderValueText(turn.GuessedValue.Value);

        isCorrectAnswer = turn.GuessedValue.Value == turn.ActualValue;
    }

    public override IEnumerator Show()
    {
        nextButton.interactable = false;
        
        yield return base.Show();

        yield return new WaitForSeconds(this.revealOffset);

        yield return this.guessedValueText.transform
            .DOScale(1f, this.revealDuration)
            .SetEase(this.revealEasing)
            .WaitForCompletion();

        yield return new WaitForSeconds(this.revealOffset);

        yield return this.actualValueText.transform
            .DOScale(1f, this.revealDuration)
            .SetEase(this.revealEasing)
            .WaitForCompletion();
        
        if (isCorrectAnswer)
            congratsParticles.Play();

        nextButton.interactable = true;
    }

    public override IEnumerator Hide()
    {
        yield return base.Hide();

        this.actualValueText.transform.localScale = Vector3.zero;
        this.guessedValueText.transform.localScale = Vector3.zero;
    }
}

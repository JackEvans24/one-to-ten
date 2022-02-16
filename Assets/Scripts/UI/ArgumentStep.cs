using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArgumentStep : GameStep
{
    [Header("Text references")]
    [SerializeField] protected TMP_Text promptText;
    [SerializeField] private Slider valueSlider;
    [SerializeField] private TMP_Text valueText;

    public int GuessedValue { get => Mathf.RoundToInt(this.valueSlider.value); }

    public void UpdateGameValues(CurrentTurnData turn)
    {
        this.promptText.text = TextProvider.GetQuestionText(turn);

        this.valueSlider.value = 0;
        this.UpdateSliderTextValue();
    }

    public void UpdateSliderTextValue()
    {
        var value = this.valueSlider.value;
        this.valueText.text = TextProvider.GetSliderValueText(value);
    }
}

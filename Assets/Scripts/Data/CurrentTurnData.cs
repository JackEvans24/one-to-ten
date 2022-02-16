using UnityEngine;

public class CurrentTurnData
{
    public string CurrentScale { get; private set; }
    public int ActualValue { get; private set; }
    public string PlayerInput { get; set; }
    public int? GuessedValue { get; set; }

    public void Reset(string scale)
    {
        this.CurrentScale = scale;
        this.ActualValue = Random.Range(0, 11);
        this.PlayerInput = string.Empty;
        this.GuessedValue = null;
    }
}

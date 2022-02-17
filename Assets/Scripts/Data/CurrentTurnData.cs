using UnityEngine;

public class CurrentTurnData
{
    public GameState CurrentState { get; set; }
    public Player CurrentPlayer { get; private set; }
    public string CurrentScale { get; private set; }
    public int ActualValue { get; private set; }
    public string PlayerInput { get; set; }
    public int? GuessedValue { get; set; }

    public CurrentTurnData()
    {
        this.CurrentState = GameState.StartOfTurn;
    }

    public void Reset(Player currentPlayer, string scale)
    {
        this.CurrentPlayer = currentPlayer;
        this.CurrentScale = scale;
        this.ActualValue = Random.Range(0, 11);
        this.PlayerInput = string.Empty;
        this.GuessedValue = null;
    }

    public void UpdateState(bool allowNewPlayers)
    {
        if (this.CurrentState == GameState.Results)
            this.CurrentState = GameState.StartOfTurn;
        else if (this.CurrentState == GameState.StartOfTurn && !allowNewPlayers)
            this.CurrentState = GameState.PlayerInput;
        else
            this.CurrentState = (GameState)((int)this.CurrentState + 1);
    }
}

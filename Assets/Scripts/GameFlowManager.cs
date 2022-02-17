using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private NextPlayerStep nextPlayerCanvas;
    [SerializeField] private NewPlayerStep newPlayerCanvas;
    [SerializeField] private InputStep inputCanvas;
    [SerializeField] private GameStep audienceCanvas;
    [SerializeField] private ArgumentStep argumentCanvas;
    [SerializeField] private ResultStep resultCanvas;
    [SerializeField] private ConfirmModal confirmModal;
    [SerializeField] private GameStep pauseModal;

    private Queue<string> scaleQueue = new Queue<string>();
    private CurrentTurnData currentTurn = new CurrentTurnData();

    protected void Awake()
    {
        LogManager.Log("Game flow manager Awake()");

        var scales = ScaleService.Instance.AllScales;

        foreach (var scale in scales)
            this.scaleQueue.Enqueue(scale);

        GameManager.ResetPlayers();
    }

    protected void Start()
    {
        LogManager.Log("Game flow manager Start()");

        this.NextGameStep();
    }

    public void NextGameStep()
    {
        LogManager.Log("Next game step started: " + this.currentTurn.CurrentState);

        switch (this.currentTurn.CurrentState)
        {
            case GameState.StartOfTurn:
                this.StartOfNewTurn();
                break;

            case GameState.NewPlayer:
                this.AddNewPlayer();
                break;

            case GameState.PlayerInput:
                this.ShowAudienceCanvas();
                break;

            case GameState.PassToAudience:
                this.ShowArgumentCanvas();
                break;

            case GameState.Argument:
                this.ShowResultCanvas();
                break;

            case GameState.Results:
                this.ShowNextPlayerCanvas();
                break;

            default:
                throw new NotImplementedException();
        }

        this.currentTurn.UpdateState(this.nextPlayerCanvas.AllowNewPlayers);
    }

    public void Pause()
    {
        this.confirmModal.UpdateAnswerText(string.Empty);
        StartCoroutine(this.pauseModal.Show());
    }

    public void ClosePauseModal()
    {
        StartCoroutine(this.pauseModal.Hide());
    }

    public void QuitGame()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    public void ShowModal()
    {
        string answer = string.Empty;
        if (this.currentTurn.CurrentState == GameState.PlayerInput)
            answer = TextProvider.WrapWithColour(this.inputCanvas.PlayerInput);
        else if (this.currentTurn.CurrentState == GameState.Argument)
            answer = TextProvider.GetSliderValueText(this.argumentCanvas.GuessedValue);

        this.confirmModal.UpdateAnswerText(answer);

        this.confirmModal.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.confirmModal.Show());
    }

    public void CloseModal()
    {
        StartCoroutine(this.confirmModal.Hide());
    }

    protected void ShowNextPlayerCanvas()
    {
        this.nextPlayerCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.nextPlayerCanvas));
    }

    protected void StartOfNewTurn()
    {
        if (this.nextPlayerCanvas.AllowNewPlayers)
            this.ShowNewPlayerCanvas();
        else
        {
            this.NewTurn();
            this.ShowInputCanvas();
        }
    }

    protected void ShowNewPlayerCanvas()
    {
        this.newPlayerCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.newPlayerCanvas));
    }

    protected void AddNewPlayer()
    {
        var newPlayer = this.newPlayerCanvas.GetPlayer();
        GameManager.AddPlayer(newPlayer);

        this.NewTurn(newPlayer);
        this.ShowInputCanvas();
    }

    protected void ShowInputCanvas()
    {
        this.inputCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.inputCanvas));
    }

    protected void ShowAudienceCanvas()
    {
        StartCoroutine(this.ShowGameStep(this.audienceCanvas));
    }

    protected void ShowArgumentCanvas()
    {
        this.currentTurn.PlayerInput = this.inputCanvas.PlayerInput;

        this.argumentCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.argumentCanvas));
    }

    protected void ShowResultCanvas()
    {
        this.currentTurn.GuessedValue = this.argumentCanvas.GuessedValue;

        this.resultCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.resultCanvas));
    }

    protected void NewTurn(Player nextPlayer = null)
    {
        if (nextPlayer == null)
            nextPlayer = GameManager.GetNextPlayer();
        var scale = this.GetNextScale();

        this.currentTurn.Reset(nextPlayer, scale);
    }

    protected string GetNextScale()
    {
        var nextScale = this.scaleQueue.Dequeue();
        this.scaleQueue.Enqueue(nextScale);

        return nextScale;
    }

    protected IEnumerator ShowGameStep(GameStep canvasToShow)
    {
        var allCanvases = new GameStep[] { this.confirmModal, this.nextPlayerCanvas, this.newPlayerCanvas, this.inputCanvas, this.audienceCanvas, this.argumentCanvas, this.resultCanvas };
        foreach (var canvas in allCanvases)
            yield return canvas.Hide();

        yield return canvasToShow.Show();
    }
}

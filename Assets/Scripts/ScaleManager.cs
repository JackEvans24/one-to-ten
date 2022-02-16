using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [SerializeField] private GameStep nextPlayerCanvas;
    [SerializeField] private InputStep inputCanvas;
    [SerializeField] private GameStep audienceCanvas;
    [SerializeField] private ArgumentStep argumentCanvas;
    [SerializeField] private ResultStep resultCanvas;

    private Queue<string> scaleQueue = new Queue<string>();
    private CurrentTurnData currentTurn = new CurrentTurnData();

    private void Awake()
    {
        var scales = ScaleService.Instance.AllScales;

        foreach (var scale in scales)
            this.scaleQueue.Enqueue(scale);

        this.ShowNextPlayerCanvas();
    }

    public void ShowNextPlayerCanvas()
    {
        this.NewTurn();
        StartCoroutine(this.ShowGameStep(this.nextPlayerCanvas));
    }

    public void ShowInputCanvas()
    {
        this.inputCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.inputCanvas));
    }

    public void ShowAudienceCanvas()
    {
        StartCoroutine(this.ShowGameStep(this.audienceCanvas));
    }

    public void ShowArgumentCanvas()
    {
        this.currentTurn.PlayerInput = this.inputCanvas.PlayerInput;

        this.argumentCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.argumentCanvas));
    }

    public void ShowResultCanvas()
    {
        this.currentTurn.GuessedValue = this.argumentCanvas.GuessedValue;

        this.resultCanvas.UpdateGameValues(this.currentTurn);
        StartCoroutine(this.ShowGameStep(this.resultCanvas));
    }

    private void NewTurn()
    {
        var scale = this.GetNextScale();
        this.currentTurn.Reset(scale);
    }

    private string GetNextScale()
    {
        var nextScale = this.scaleQueue.Dequeue();
        this.scaleQueue.Enqueue(nextScale);

        return nextScale;
    }

    private IEnumerator ShowGameStep(GameStep canvasToShow)
    {
        var allCanvases = new GameStep[] { this.nextPlayerCanvas, this.inputCanvas, this.audienceCanvas, this.argumentCanvas, this.resultCanvas };
        foreach (var canvas in allCanvases)
            yield return canvas.Hide();

        yield return canvasToShow.Show();
    }
}

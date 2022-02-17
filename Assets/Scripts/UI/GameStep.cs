using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameStep : MonoBehaviour
{
    [Header("Canvas show/hide")]
    [SerializeField] protected CanvasGroup canvas;
    [SerializeField] protected float scaleUpDuration = 0.4f, scaleDownDuration = 0.2f;
    [SerializeField] protected Ease scaleUpEasing = Ease.OutSine, scaleDownEasing = Ease.InSine;

    public virtual IEnumerator Show()
    {
        if (this.canvas.alpha == 1 && this.canvas.transform.localScale == Vector3.one)
            yield break;

        this.canvas.interactable = true;
        this.canvas.blocksRaycasts = true;

        this.canvas.alpha = 1;

        yield return this.canvas.transform
            .DOScale(1, this.scaleUpDuration)
            .SetEase(this.scaleUpEasing)
            .WaitForCompletion();
    }

    public virtual IEnumerator Hide()
    {
        if (this.canvas.alpha == 0 && this.canvas.transform.localScale == Vector3.zero)
            yield break;

        this.canvas.interactable = false;
        this.canvas.blocksRaycasts = false;

        yield return this.canvas.transform
            .DOScale(0, this.scaleDownDuration)
            .SetEase(this.scaleDownEasing)
            .WaitForCompletion();

        this.canvas.alpha = 0;
    }

    public virtual void UpdateGameValues(CurrentTurnData turn)
    {

    }
}

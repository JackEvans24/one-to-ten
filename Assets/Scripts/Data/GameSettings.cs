using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Values")]
public class GameSettings : ScriptableObject
{
    public Color[] GameColours;

    private Queue<Color> colourQueue = new Queue<Color>();

    public Color GetNextColour()
    {
        if (this.colourQueue == null || this.colourQueue.Count < 1)
            this.InstantiateColourQueue();

        var nextColour = this.colourQueue.Dequeue();
        this.colourQueue.Enqueue(nextColour);

        return nextColour;
    }

    private void InstantiateColourQueue()
    {
        if (this.GameColours.Length < 1)
            throw new System.Exception("No colours added to Game Settings");

        foreach (var colour in this.GameColours)
            colourQueue.Enqueue(colour);
    }
}

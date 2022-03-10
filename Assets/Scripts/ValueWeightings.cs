using UnityEngine;

public class ValueWeightings : MonoBehaviour
{
    [SerializeField] private ValueWeight[] valueWeights;

    public int GetWeightedRandomValue()
    {
        var weightTotal = 0;
        foreach (var valueWeight in valueWeights)
            weightTotal += valueWeight.Weight;

        var targetValue = Random.Range(0, weightTotal);
        var currentValue = 0;

        foreach (var valueWeight in valueWeights)
        {
            currentValue += valueWeight.Weight;
            if (currentValue >= targetValue)
                return valueWeight.Value;
        }

        throw new System.Exception("No values in weights lookup");
    }
}

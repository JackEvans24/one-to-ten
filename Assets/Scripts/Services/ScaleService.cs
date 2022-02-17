using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScaleService
{
    private ScaleService() { }

    private static ScaleService instance;
    public static ScaleService Instance
    {
        get
        {
            if (instance == null)
                instance = new ScaleService();
            return instance;
        }
    }

    private Queue<string> allScales;
    public Queue<string> AllScales
    {
        get
        {
            if (this.allScales == null)
            {
                this.allScales = new Queue<string>();
                foreach (var scale in this.GetScales())
                    this.allScales.Enqueue(scale);
            }

            return this.allScales;
        }
    }

    private string[] GetScales()
    {
        var file = Resources.Load("scales") as TextAsset;

        if ((file?.text.Length ?? 0) == 0)
            return new string[0];

        var dto = JsonUtility.FromJson<ScalesDto>(file.text);
        return dto.adjectiveScales
            .Union(dto.actionScales)
            .Union(dto.placeScales)
            .OrderBy(scale => Guid.NewGuid())
            .ToArray();
    }

    [Serializable]
    class ScalesDto
    {
        public string[] adjectiveScales;
        public string[] actionScales;
        public string[] placeScales;
    }
}

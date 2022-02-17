using System;
using System.IO;
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

    private string[] allScales;
    public string[] AllScales
    {
        get
        {
            if (this.allScales == null)
                this.allScales = this.GetScales();
            return this.allScales;
        }
    }

    private const string defaultScalesPath = "Assets/StreamingAssets/default-scales.json";
    private string[] GetScales()
    {
        string json;
        using (StreamReader r = new StreamReader(defaultScalesPath))
        {
            json = r.ReadToEnd();
        }

        if (json == null)
            return new string[0];

        var dto = JsonUtility.FromJson<ScalesDto>(json);
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

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

    private Scale[] allScales;
    public Scale[] AllScales
    {
        get
        {
            if (this.allScales == null)
                this.allScales = this.GetScales();
            return this.allScales;
        }
    }

    private const string defaultScalesPath = "Assets/StreamingAssets/default-scales.json";
    private Scale[] GetScales()
    {
        string json;
        using (StreamReader r = new StreamReader(defaultScalesPath))
        {
            json = r.ReadToEnd();
        }

        if (json == null)
            return new Scale[0];

        var dto = JsonUtility.FromJson<ScalesDto>(json);
        return dto.scales.OrderBy(scale => Guid.NewGuid()).ToArray();
    }

    [Serializable]
    class ScalesDto
    {
        public Scale[] scales;
    }
}

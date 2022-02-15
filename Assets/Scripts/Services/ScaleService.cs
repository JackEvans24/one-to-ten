using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScaleService
{
    private const string defaultScalesPath = "Assets/StreamingAssets/default-scales.json";
    public Scale[] GetScales()
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

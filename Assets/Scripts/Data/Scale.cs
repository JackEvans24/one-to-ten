using System.Collections.Generic;

public class Scale
{
    public string Name { get; set; }
    public ScaleType Categories { get; set; }

    public Scale(string name)
    {
        this.Name = name;
        this.Categories = ScaleType.Default;
    }
}

public static class TemporaryScaleList
{
    public static List<Scale> Scales = new List<Scale>()
    {
        new Scale("Easy"),
        new Scale("Funny"),
        new Scale("Sexy"),
        new Scale("Stupid"),
        new Scale("Scary")
    };
}
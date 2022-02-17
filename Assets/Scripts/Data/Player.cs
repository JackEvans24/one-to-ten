using System;
using UnityEngine;

public class Player
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Color Color { get; private set; }

    public Player(string name, Color color)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Color = color;
    }
}

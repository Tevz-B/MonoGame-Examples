using System;
using Microsoft.Xna.Framework;

namespace MadDriver_v1.Scene;

public class Level : GameComponent
{
    protected Scene _scene;
    protected LevelType _type;

    protected Level(Game theGame)
        : base (theGame)
    {
        _scene = new Scene();
    }

    public Scene Scene
    {
        get => _scene;
        set => _scene = value;
    }

    public LevelType Type => _type;

    public override void Initialize()
    {
        Console.WriteLine("Loading level.");
        base.Initialize();
        this.Reset();
    }

    public virtual void Reset()
    {
        Console.WriteLine("Resetting level.");
    }

    protected override void Dispose(bool disposing)
    {
        Console.WriteLine("Unloading level.");
        base.Dispose(disposing);
    }
}
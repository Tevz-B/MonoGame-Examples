using System;
using Microsoft.Xna.Framework;

namespace Artificial_I.Artificial.Utils;

public static class SRandom
{
    private static readonly Random Random = new Random(DateTime.Now.Millisecond);
    
    public static float Float(float min, float max)
    {
        double val = (Random.NextDouble() * (max - min) + min);
        return (float)val;
    }
    
    public static float Float(float max)
    {
        double val = (Random.NextDouble() * max);
        return (float)val;
    }

    public static float Float()
    {
        double val = Random.NextDouble();
        return (float)val;
    }

    public static Vector2 Vector2(float minX, float maxX, float minY, float maxY)
    {
        return new Vector2(Float(minX, maxX), Float(minY, maxY));
    }
    
    public static Vector2 Vector2(float maxX, float maxY)
    {
        return new Vector2(Float(maxX), Float(maxY));
    }

    public static int Int(int max)
    {
        return Random.Next(max);
    }

    public static int Int()
    {
        return Random.Next();
    }
}
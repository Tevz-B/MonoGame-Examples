using Microsoft.Xna.Framework;

namespace PrimitiveBatchExample;

public class Line
{
    private Vector2 _startPoint;
    private Vector2 _endPoint;

    public Vector2 StartPoint
    {
        get => _startPoint;
        set => _startPoint = value;
    }

    public Vector2 EndPoint
    {
        get => _endPoint;
        set => _endPoint = value;
    }

    public Line(Vector2 start, Vector2 end)
    {
        StartPoint = start;
        EndPoint = end;
    }
    
}
using Microsoft.Xna.Framework;

namespace Express.Math;

public class AAHalfPlane : HalfPlane
{
    protected AxisDirection _direction;

    public AAHalfPlane(AxisDirection theDirection, float theDistance)
    {
        switch (theDirection)
        {
        default :
        case AxisDirection.PositiveX :
            _normal = Vector2.UnitX;
            break;
        case AxisDirection.NegativeX :
            _normal = -Vector2.UnitX;
            break;
        case AxisDirection.PositiveY :
            _normal = Vector2.UnitY;
            break;
        case AxisDirection.NegativeY :
            _normal = -Vector2.UnitY;
            break;
        }

        _direction = theDirection;
    }

    // public static AAHalfPlane AaHalfPlaneWithDirectionDistance(AxisDirection theDirection, float theDistance)
    // {
    //     return new AAHalfPlane(theDirection, theDistance);
    // }

    public AxisDirection Direction
    {
        get => _direction;
        set
        {
            switch (value)
            {
            default :
            case AxisDirection.PositiveX :
                _normal = Vector2.UnitX;
                break;
            case AxisDirection.NegativeX :
                _normal = -Vector2.UnitX;
                break;
            case AxisDirection.PositiveY :
                _normal = Vector2.UnitY;
                break;
            case AxisDirection.NegativeY :
                _normal = -Vector2.UnitY;
                break;
            }

        }
    }

    void SetNormal(Vector2 value)
    {
        if ((value.X == 0 && value.Y == 0) || (value.X != 0 && value.Y != 0))
        {
            throw new Exception("InvalidArgumentException", "Axis aligned half plane requires an axis aligned normal", null);
        }

        base.SetNormal(value);
        if (value.X > 0)
        {
            _direction = AxisDirectionPositiveX;
        }
        else if (value.X < 0)
        {
            _direction = AxisDirectionNegativeX;
        }
        else if (value.Y > 0)
        {
            _direction = AxisDirectionPositiveY;
        }
        else if (value.Y < 0)
        {
            _direction = AxisDirectionNegativeY;
        }

    }    
}
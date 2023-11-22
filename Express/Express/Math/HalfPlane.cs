using Microsoft.Xna.Framework;

namespace Express.Math;

public class HalfPlane
{
    protected float _distance;
    protected Vector2 _normal;

    protected HalfPlane()
    {
    }

    public HalfPlane(Vector2 theNormal, float theDistance)
    {
        this.Normal = theNormal;
        this.Distance = theDistance;
    }

    // public static HalfPlane HalfPlaneWithNormalDistance(Vector2 theNormal, float theDistance)
    // {
    //     return new HalfPlane(theNormal, theDistance);
    // }

    public float Distance
    {
        get => _distance;
        set => _distance = value;
    }

    public Vector2 Normal
    {
        get => _normal;
        set
        {
            _normal = value;
            if (_normal.LengthSquared() != 1.0f)
            {
                _normal.Normalize();
            }

        }
    }
}
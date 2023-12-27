using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Physical_Properties;
using friHockey_v4.Audio;
using friHockey_v4.Scene.Objects.Walls;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace friHockey_v4.Scene.Objects;

public class Puck : IParticle, ICoefficientOfRestitution, ICustomCollider
{
    private Vector2 _position;
    private Vector2 _velocity = new Vector2(0, 50);

    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;

    public float Radius { get; set; } = 20;

    public float Mass { get; set; } = 1;

    public float CoefficientOfRestitution { get; set; } = Constants.PuckCoefficientOfRestitution();
    
    public void CollidedWith(object item)
    {
        if (item is Mallet)
        {
            SoundEngine.Play(SoundEffectType.PuckMallet);
        }
        else if (item is RectangleWall)
        {
            SoundEngine.Play(SoundEffectType.PuckWall);
        }

    }
}
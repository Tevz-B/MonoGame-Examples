using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Composites;
using Express.Scene.Objects.Physical_Properties;
using friHockey_v5.Audio;
using friHockey_v5.SceneObjects.Walls;
using Microsoft.Xna.Framework;

namespace friHockey_v5.SceneObjects;

public class Puck : IParticle, ICoefficientOfRestitution, ICustomCollider
{
    private Vector2 _position;
    private Vector2 _velocity = new Vector2(0, 50);

    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;

    public float Radius { get; set; } = 20;

    public float Mass { get; set; } = 1;

    public float CoefficientOfRestitution { get; set; } = Constants.PuckCoefficientOfRestitution;
    
    public void CollidedWith(object item)
    {
        float pan = (_position.X - 160)/160.0f;
        if (item is Mallet)
        {
            SoundEngine.Play(SoundEffectType.PuckMallet, pan);
        }
        else if (item is RectangleWall)
        {
            SoundEngine.Play(SoundEffectType.PuckWall, pan);
        }

    }
}
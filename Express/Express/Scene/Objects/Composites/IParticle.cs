using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Express.Scene.Objects.Physical_Properties;

namespace Express.Scene.Objects.Composites;

public interface IParticle : IMovable, IMass, IParticleCollider
{
}
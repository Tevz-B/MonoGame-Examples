using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Physical_Properties;

namespace Express.Scene.Objects;

public interface IParticle : IMovable, IMass, IParticleCollider
{
}
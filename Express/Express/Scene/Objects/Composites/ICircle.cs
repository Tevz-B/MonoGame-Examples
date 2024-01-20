using Express.Scene.Objects.Physical_Properties;
using Express.Scene.Objects.Rotation;

namespace Express.Scene.Objects.Composites;

public interface ICircle : IParticle, IRotatable, IAngularMass
{
}
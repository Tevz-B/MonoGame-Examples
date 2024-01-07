using System.Linq;
using Express.Physics;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v5.Players.Human;

public class HumanPlayerKb : Player
{
    private const float Acceleration = 15f;
    private const float DecelerationFactor = 0.8f;
    
    public HumanPlayerKb(Game game, Mallet mallet, PlayerPosition position) 
        : base(game, mallet, position)
    {
    }
    
    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = _mallet.Position;
        
        var kbState = Keyboard.GetState();
        var pressedKeys = kbState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.W)){_mallet.Velocity.Y -= Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.S)){_mallet.Velocity.Y += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.A)){_mallet.Velocity.X -= Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.D)){_mallet.Velocity.X += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}

        if (_mallet.Velocity.Length() > 0f)
        {
            _mallet.Velocity *= DecelerationFactor;
        }

        MovementPhysics.SimulateMovement(_mallet, gameTime.ElapsedGameTime);
    }
}
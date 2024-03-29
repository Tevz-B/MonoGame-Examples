using System.Linq;
using Express.Physics;
using friHockey_v4.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v4.Players.Human;

public class HumanPlayerKB : Player
{
    private const float _acceleration = 15000f;
    private const float _decelerationFactor = 0.8f;
    
    public HumanPlayerKB(Game game, Mallet mallet, PlayerPosition position) 
        : base(game, mallet, position)
    {
    }
    
    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = _mallet.Position;
        
        var kbState = Keyboard.GetState();
        var pressedKeys = kbState.GetPressedKeys();
        if (pressedKeys.Contains(Keys.W)){_mallet.Velocity.Y -= _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.S)){_mallet.Velocity.Y += _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.A)){_mallet.Velocity.X -= _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}
        if (pressedKeys.Contains(Keys.D)){_mallet.Velocity.X += _acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;}

        if (_mallet.Velocity.Length() > 0f)
        {
            _mallet.Velocity *= _decelerationFactor;
        }

        MovementPhysics.SimulateMovement(_mallet, gameTime.ElapsedGameTime);
    }
}
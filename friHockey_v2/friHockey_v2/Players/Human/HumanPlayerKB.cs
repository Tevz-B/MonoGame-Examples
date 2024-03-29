using System.Collections;
using System.Linq;
using Express.Physics;
using friHockey_v2.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v2.Players.Human;

public class HumanPlayerKB : Player
{
    private const float _acceleration = 15000f;
    private const float _decelerationFactor = 0.8f;
    
    private Rectangle _movementArea; // Temporary - until we do level collision
    
    public HumanPlayerKB(Mallet mallet, ArrayList scene, PlayerPosition position, Game game) 
        : base(mallet, scene, position)
    {
        _movementArea = game.GraphicsDevice.Viewport.Bounds;
        _movementArea.Height = 360;
        if (position == PlayerPosition.Bottom)
        {
            _movementArea.Y = game.Window.ClientBounds.Height - _movementArea.Height;
        }
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
        // Remove this after implementing level collision
        if (!_movementArea.Contains(_mallet.Position))
        {
            _mallet.Position = oldPosition;
        }
    }
}
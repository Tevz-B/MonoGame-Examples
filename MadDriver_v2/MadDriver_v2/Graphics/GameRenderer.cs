using System;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using MadDriver_v2.Scene;
using MadDriver_v2.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MadDriver_v2.Graphics;

public class GameRenderer : DrawableGameComponent
{
     protected ContentManager _content;
    
    protected string[] _levelResourceNames = new string[(int)LevelType.LastType];
    
    protected Texture2D _levelBackground;
    protected Texture2D _levelShadow;
    
    protected string[] _carTypeNames = new string[(int)CarType.LastType];
    protected string[] _carDamageSuffixes = new string[(int)CarDamage.LastType];
    
    protected Sprite[,] _cars = new Sprite[
        (int)CarType.LastType, 
        (int)CarDamage.LastType
    ];
    protected AnimatedSprite[] _explosionSprite = new AnimatedSprite[7];
    
    protected SpriteBatch _spriteBatch;
    
    protected Camera _camera;
    protected Vector3 _cameraShake;
    
    protected BasicEffect _spriteEffect;
    
    protected Level _level;

    public GameRenderer(Game theGame, Level theLevel, Camera theCamera)
        : base(theGame)
    {
        _level = theLevel;
        _camera = theCamera;
        _cameraShake = new Vector3();
        
        _levelResourceNames[(int)LevelType.Suburbs] = "PREDMESTJE";
        _levelResourceNames[(int)LevelType.City] = "MESTO";
        _levelResourceNames[(int)LevelType.Highway] = "AVTOCESTA";
        _levelResourceNames[(int)LevelType.Graveyard] = "POKOPALISCE";
        _levelResourceNames[(int)LevelType.Warehouse] = "SKLADISCE";
        
        _carTypeNames[(int)CarType.FamilyBlue] = "AVTO1";
        _carTypeNames[(int)CarType.FamilyRed] = "AVTO2";
        _carTypeNames[(int)CarType.Taxi] = "TAXI";
        _carTypeNames[(int)CarType.Police] = "POLICE";
        _carTypeNames[(int)CarType.Motorbike] = "MOTOR";
        _carTypeNames[(int)CarType.Truck] = "KAMJON";
        _carTypeNames[(int)CarType.LongTruck] = "PRIKLOPNIK";
        
        _carDamageSuffixes[(int)CarDamage.None] = "";
        _carDamageSuffixes[(int)CarDamage.Light] = "'1";
        _carDamageSuffixes[(int)CarDamage.Heavy] = "'2";
        _carDamageSuffixes[(int)CarDamage.Total] = "'3";
        
        _content = theGame.Content;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteEffect = new BasicEffect(GraphicsDevice);
        _spriteEffect.TextureEnabled = true;
        _levelBackground = _content.Load<Texture2D>(_levelResourceNames[(int)_level.Type]);
        _levelShadow = _content.Load<Texture2D>($"{_levelResourceNames[(int)_level.Type]}shadow");
        for (int i = 0; i < (int)CarType.LastType; i++)
        {
            for (int j = 0; j < (int)CarDamage.LastType; j++)
            {
                Sprite sprite = new Sprite();
                string assetName = $"{_carTypeNames[i]}{_carDamageSuffixes[j]}";
                Console.WriteLine($"{assetName}");
                sprite.Texture = _content.Load<Texture2D>(assetName);
                sprite.Origin = new Vector2(sprite.Texture.Width / 2f, sprite.Texture.Height / 2f);
                _cars[i, j] = sprite;
            }
        }

        for (int j = 0; j < 7; j++)
        {
            Texture2D explosionTexture = _content.Load<Texture2D>($"Effects/explozija{j + 1}");
            _explosionSprite[j] = new AnimatedSprite(1);
            for (int i = 0; i < 16; i++)
            {
                int row = i % 4;
                int column = i / 4;
                Sprite sprite = new Sprite();
                sprite.Texture = explosionTexture;
                sprite.SourceRectangle = new Rectangle(column * 128, row * 128, 128, 128);
                sprite.Origin = new Vector2(64, 64);
                AnimatedSpriteFrame frame = AnimatedSpriteFrame.Frame(sprite, _explosionSprite[j].Duration * i / 16f);
                _explosionSprite[j].AddFrame(frame);
            }
        }
    }

    public override void Draw(GameTime gameTime)
    {
        // Shake camera with explosions
        foreach (object item in _level.Scene)
        {
            if (item is Explosion explosion)
            {
                float angle = SRandom.Float(2 * MathF.PI);
                Vector3 direction = new Vector3(MathF.Cos(angle), MathF.Sin(angle), 0);
                float distance = MathF.Abs(_camera.Position - explosion.Position.Y) / 100.0f;
                float power = 1.0f / (distance * distance + 1) * (1 - explosion.Lifetime.Percentage) * 5;
                direction *= power;
                _cameraShake += direction;
            }
        }

        _cameraShake *= 0.95f;
        
        // Update sprite effect matrices.
        _spriteEffect.View = Matrix.Multiply(_camera.View, Matrix.CreateTranslation(_cameraShake));
        _spriteEffect.Projection = _camera.Projection;
        
        Matrix inverse = Matrix.Invert(Matrix.Multiply(_spriteEffect.View, _spriteEffect.Projection));
        Vector3 topPoint = Vector3.Transform(new Vector3(0, 1, 0), (inverse));
        Vector3 bottomPoint = Vector3.Transform(new Vector3(0, -1, 0), (inverse));
        
        // Start the sprite batch.
        _spriteBatch.Begin(SpriteSortMode.Texture,
            null, null, null, null, _spriteEffect);
        
        // Draw level background.
        // Edges of tiled sprites are at n*levelBackground.height.
        int topIndex = (int)topPoint.Y / 400 - 1;
        int bottomIndex = (int)bottomPoint.Y / 400;
        for (int i = topIndex; i <= bottomIndex; i++)
        {
            _spriteBatch.Draw(_levelBackground, new Vector2(0, i * 400), Color.White);
        }

        // Draw cars
        foreach (object item in _level.Scene)
        {
            if (item is Car car)
            {
                CarDamage damage = CarDamage.None;
                if (car.Damage >= 50 && car.Damage < 75)
                {
                    damage = CarDamage.Light;
                }
                else if (car.Damage >= 75 && car.Damage < 100)
                {
                    damage = CarDamage.Heavy;
                }
                else if (car.Damage >= 100)
                {
                    damage = CarDamage.Total;
                }

                Sprite sprite = _cars[(int)car.Type, (int)damage];
                _spriteBatch.Draw(sprite.Texture,
                    car.Position, null, Color.White, 0, sprite.Origin, 1, SpriteEffects.None, 0);
            }
        }

        // Draw shadow on top.
        for (int i = topIndex; i <= bottomIndex; i++)
        {
            _spriteBatch.Draw(_levelShadow, new Vector2(0, i * 400),
                Color.Multiply(Color.White, 0.3f));
        }

        _spriteBatch.End();
        
        
        // Draw effects in additive mode.
        _spriteBatch.Begin(
            SpriteSortMode.Deferred, BlendState.Additive, null, null, null, _spriteEffect);
        foreach (object item in _level.Scene)
        {
            if (item is Explosion explosion)
            {
                int explosionIndex = explosion.Random % 5 + 2;
                Sprite sprite = _explosionSprite[explosionIndex].SpriteAtTime(explosion.Lifetime.Progress);
                int maskedEffects = explosion.Random & (int)(SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
                SpriteEffects effects = (SpriteEffects)maskedEffects;
                if (sprite is not null)
                {
                    _spriteBatch.Draw(
                        sprite.Texture, explosion.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin,
                        1.3f, effects, 0);
                }
            }
        }

        _spriteBatch.End();
    }

    protected override void UnloadContent()
    {
        _content.Unload();
    }
}
using System;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using MadDriver_v1.Scene;
using MadDriver_v1.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MadDriver_v1.Graphics;

public class GameRenderer : DrawableGameComponent
{
    protected ContentManager content;
    protected string[] levelResourceNames = new string[(int)LevelType.LastType];
    protected Texture2D levelBackground;
    protected Texture2D levelShadow;
    protected string[] carTypeNames = new string[(int)CarType.LastType];
    protected string[] carDamageSuffixes = new string[(int)CarDamage.LastType];
    protected Sprite[,] cars = new Sprite[(int)CarType.LastType, (int)CarDamage.LastType];
    protected AnimatedSprite[] explosionSprite = new AnimatedSprite[7];
    protected SpriteBatch spriteBatch;
    protected Camera camera;
    protected Vector3 cameraShake;
    protected BasicEffect spriteEffect;
    protected Level level;

    public GameRenderer(Game theGame, Level theLevel, Camera theCamera)
        : base(theGame)
    {
        level = theLevel;
        camera = theCamera;
        cameraShake = new Vector3();
        levelResourceNames[(int)LevelType.Suburbs] = "PREDMESTJE";
        levelResourceNames[(int)LevelType.City] = "MESTO";
        levelResourceNames[(int)LevelType.Highway] = "AVTOCESTA";
        levelResourceNames[(int)LevelType.Graveyard] = "POKOPALISCE";
        levelResourceNames[(int)LevelType.Warehouse] = "SKLADISCE";
        carTypeNames[(int)CarType.FamilyBlue] = "AVTO1";
        carTypeNames[(int)CarType.FamilyRed] = "AVTO2";
        carTypeNames[(int)CarType.Taxi] = "TAXI";
        carTypeNames[(int)CarType.Police] = "POLICE";
        carTypeNames[(int)CarType.Motorbike] = "MOTOR";
        carTypeNames[(int)CarType.Truck] = "KAMJON";
        carTypeNames[(int)CarType.LongTruck] = "PRIKLOPNIK";
        carDamageSuffixes[(int)CarDamage.None] = "";
        carDamageSuffixes[(int)CarDamage.Light] = "'1";
        carDamageSuffixes[(int)CarDamage.Heavy] = "'2";
        carDamageSuffixes[(int)CarDamage.Total] = "'3";
        content = new ContentManager(theGame.Services);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(this.GraphicsDevice);
        spriteEffect = new BasicEffect(this.GraphicsDevice);
        spriteEffect.TextureEnabled = true;
        levelBackground = content.Load<Texture2D>(levelResourceNames[(int)level.Type]);
        levelShadow = content.Load<Texture2D>($"{levelResourceNames[(int)level.Type]}shadow");
        for (int i = 0; i < (int)CarType.LastType; i++)
        {
            for (int j = 0; j < (int)CarDamage.LastType; j++)
            {
                Sprite sprite = new Sprite();
                string assetName = $"{carTypeNames[i]}{carDamageSuffixes[j]}";
                Console.WriteLine($"{assetName}");
                sprite.Texture = content.Load<Texture2D>(assetName);
                sprite.Origin = new Vector2(sprite.Texture.Width / 2f, sprite.Texture.Height / 2f);
                cars[i, j] = sprite;
            }
        }

        for (int j = 0; j < 7; j++)
        {
            Texture2D explosionTexture = content.Load<Texture2D>($"explozija{j + 1}");
            explosionSprite[j] = new AnimatedSprite(1);
            for (int i = 0; i < 16; i++)
            {
                int row = i % 4;
                int column = i / 4;
                Sprite sprite = new Sprite();
                sprite.Texture = explosionTexture;
                sprite.SourceRectangle = new Rectangle(column * 128, row * 128, 128, 128);
                sprite.Origin = new Vector2(64, 64);
                AnimatedSpriteFrame frame = AnimatedSpriteFrame.Frame(sprite, explosionSprite[j].Duration * i / 16f);
                explosionSprite[j].AddFrame(frame);
            }
        }
    }

    public override void Draw(GameTime gameTime)
    {
        foreach (object item in level.Scene)
        {
            if (item is Explosion explosion)
            {
                float angle = SRandom.Float(2 * MathF.PI);
                Vector3 direction = new Vector3(MathF.Cos(angle), MathF.Sin(angle), 0);
                float distance = MathF.Abs(camera.Position - explosion.Position.Y) / 100.0f;
                float power = 1.0f / (distance * distance + 1) * (1 - explosion.Lifetime.Percentage) * 5;
                direction *= power;
                cameraShake += direction;
            }
        }

        cameraShake *= 0.95f;
        spriteEffect.View = Matrix.MultiplyBy(camera.View, Matrix.CreateTranslation(cameraShake));
        spriteEffect.Projection = camera.Projection;
        Matrix inverse = Matrix.Invert(Matrix.MultiplyBy(spriteEffect.View, spriteEffect.Projection));
        Vector3 topPoint = Vector3.VectorWithXYZ(0, 1, 0).TransformWith(inverse);
        Vector3 bottomPoint = Vector3.VectorWithXYZ(0, -1, 0).TransformWith(inverse);
        spriteBatch.BeginWithSortModeBlendStateSamplerStateDepthStencilStateRasterizerStateEffect(SpriteSortModeTexture,
            null, null, null, null, spriteEffect);
        int topIndex = (int)topPoint.Y / 400 - 1;
        int bottomIndex = (int)bottomPoint.Y / 400;
        for (int i = topIndex; i <= bottomIndex; i++)
        {
            spriteBatch.Draw(levelBackground, new Vector2(0, i * 400), Color.White());
        }

        foreach (object item in level.Scene)
        {
            if (item is Car car)
            {
                CarDamage damage = CarDamageNone;
                if (car.Damage >= 50 && car.Damage < 75)
                {
                    damage = CarDamageLight;
                }
                else if (car.Damage >= 75 && car.Damage < 100)
                {
                    damage = CarDamageHeavy;
                }
                else if (car.Damage >= 100)
                {
                    damage = CarDamageTotal;
                }

                Sprite sprite = cars[car.Type, damage];
                spriteBatch.Draw(sprite.Texture,
                    car.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 1, SpriteEffects.None, 0);
            }
        }

        for (int i = topIndex; i <= bottomIndex; i++)
        {
            spriteBatch.DrawToTintWithColor(levelShadow, Vector2.VectorWithXY(0, i * 400),
                Color.MultiplyWithScalar(Color.White(), 0.3f));
        }

        spriteBatch.End();
        spriteBatch.BeginWithSortModeBlendStateSamplerStateDepthStencilStateRasterizerStateEffect(
            SpriteSortModeDeffered, BlendState.Additive(), null, null, null, spriteEffect);
        foreach (object item in level.Scene)
        {
            if (item is Explosion explosion)
            {
                int explosionIndex = explosion.Random % 5 + 2;
                Sprite sprite = explosionSprite[explosionIndex].SpriteAtTime(explosion.Lifetime.Progress);
                SpriteEffects effects =
                    explosion.Random & (SpriteEffectsFlipHorizontally | SpriteEffectsFlipVertically);
                if (sprite)
                {
                    spriteBatch.DrawToFromRectangleTintWithColorRotationOriginScaleUniformEffectsLayerDepth(
                        sprite.Texture, explosion.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin,
                        1.3f, effects, 0);
                }
            }
        }

        spriteBatch.End();
    }

    protected override void UnloadContent()
    {
        content.Unload();
    }
}
using System;
using Artificial_I.Artificial.Utils;
using Express.Scene.Objects;
using Express.Scene.Objects.Movement;
using MadDriver_v1.Scene.Objects;
using Microsoft.Xna.Framework;

namespace MadDriver_v1.Scene.Levels;

public class Intro : Level
{
    protected IntroMode _mode;
    protected enum IntroMode {
        Car1Approach,
        Car1Break,
        Car2Approach,
        Car2Break,
        Car3Approach,
        Explosion,
        Fire
    }

    public Intro(Game theGame)
        : base (theGame)
    {
        _type = LevelType.Highway;
    }

    public override void Initialize()
    {
        base.Initialize();
        Car car;
        car = new Car();
        car.Type = CarType.Truck;
        car.Damage = 0;
        car.Position.X = 160;
        car.Position.Y = 150;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.FamilyBlue;
        car.Damage = 60;
        car.Position.X = 190;
        car.Position.Y = 220;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.Motorbike;
        car.Damage = 0;
        car.Position.X = 140;
        car.Position.Y = 300;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.LongTruck;
        car.Damage = 90;
        car.Position.X = 80;
        car.Position.Y = 125;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.FamilyRed;
        car.Damage = 90;
        car.Position.X = 140;
        car.Position.Y = 600;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.Taxi;
        car.Damage = 90;
        car.Position.X = 200;
        car.Position.Y = 800;
        _scene.Add(car);
        car = new Car();
        car.Type = CarType.Police;
        car.Damage = 0;
        car.Position.X = 95;
        car.Position.Y = 1000;
        _scene.Add(car);
        _mode = IntroMode.Car3Approach;
        car = (Car)_scene[6];
        car.Velocity.Y = -250;
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (object item in _scene)
        {
            if (item is ILifetime lifetime)
            {
                lifetime.Lifetime.Update(gameTime);
            }

            if (item is IMovable movable)
            {
                movable.Position += Vector2.Multiply(movable.Velocity, dt);
            }

        }
        Car car = null;
        switch (_mode)
        {
        case IntroMode.Car1Approach :
            car = (Car)_scene[4];
            if (car.Position.Y < 420)
            {
                _mode = IntroMode.Car1Break;
            }

            break;
        case IntroMode.Car1Break :
            car = (Car)_scene[4];
            car.Velocity.Y += dt * 100;
            if (car.Velocity.Y > 0)
            {
                car.Velocity.Y = 0;
                _mode = IntroMode.Car2Approach;
                car = (Car)_scene[5];
                car.Velocity.Y = -150;
            }

            break;
        case IntroMode.Car2Approach :
            car = (Car)_scene[5];
            if (car.Position.Y < 450)
            {
                _mode = IntroMode.Car2Break;
            }

            break;
        case IntroMode.Car2Break :
            car = (Car)_scene[5];
            car.Velocity.Y += dt * 100;
            if (car.Velocity.Y > 0)
            {
                car.Velocity.Y = 0;
                _mode = IntroMode.Car3Approach;
                car = (Car)_scene[6];
                car.Velocity.Y = -250;
            }

            break;
        case IntroMode.Car3Approach :
            car = (Car)_scene[6];
            if (car.Position.Y < 200)
            {
                car.Position.Y = 200;
                car.Velocity.Y = 0;
                _mode = IntroMode.Explosion;
                for (int i = 0; i < 3; i++)
                {
                    Explosion explosion = new Explosion(gameTime);
                    car = _scene[3] as Car;
                    car.Damage = 100;
                    explosion.Position = car.Position + (new Vector2(SRandom.Float() * 40 - 20, SRandom.Float() * 80 - 40));
                    _scene.Add(explosion);
                }
            }

            break;
        default :
            break;
        }

    }

    public override void Reset()
    {
        base.Reset();
    }
}
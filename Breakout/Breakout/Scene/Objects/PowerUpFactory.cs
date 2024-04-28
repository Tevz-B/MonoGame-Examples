using System;
using Artificial_I.Artificial.Utils;
using Breakout.Scene.Objects.PowerUps;

namespace Breakout.Scene.Objects;

public static class  PowerUpFactory
{
    static int[] _frequency = new int[(int)PowerUpType.LastType];
    static int _totalFrequency;
    static PowerUpType[] _randomTypeLookup = new PowerUpType[100];
    static PowerUpFactory ()
    {
        _frequency[(int)PowerUpType.Expand] = 3;
        _frequency[(int)PowerUpType.Shrink] = 3;
        _frequency[(int)PowerUpType.Magnet] = 2;
        _frequency[(int)PowerUpType.Death] = 2;
        _frequency[(int)PowerUpType.Breakthrough] = 1;
        _frequency[(int)PowerUpType.MultiBall] = 1;
        _totalFrequency = 0;
        for (int i = 0; i < (int)PowerUpType.LastType; i++)
        {
            for (int j = 0; j < _frequency[i]; j++)
            {
                _randomTypeLookup[_totalFrequency] = (PowerUpType)i;
                _totalFrequency++;
            }
        }

    }
    public static PowerUp CreatePowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Expand:
                return new Expand();
            case PowerUpType.Shrink:
                return new Shrink();
            case PowerUpType.Magnet:
                return new Magnet();
            case PowerUpType.Breakthrough:
                return new Breakthrough();
            case PowerUpType.Death:
                return new Death();
            case PowerUpType.MultiBall:
                return new MultiBall();
            case PowerUpType.LastType:
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public static PowerUp CreateRandomPowerUp()
    {
        int index = SRandom.Int(_totalFrequency);
        PowerUpType type = _randomTypeLookup[index];
        return PowerUpFactory.CreatePowerUp(type);
    }


}
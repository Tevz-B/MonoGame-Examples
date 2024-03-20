using Artificial_I.Artificial.Utils;
using Breakout.Scene.Objects.PowerUps;

namespace Breakout.Scene.Objects;

public static class  PowerUpFactory
{
    static PowerUp[] _powerUpClasses = new PowerUp[(int)PowerUpType.LastType];
    static int[] _frequency = new int[(int)PowerUpType.LastType];
    static int _totalFrequency;
    static PowerUpType[] _randomTypeLookup = new PowerUpType[100];
    static PowerUpFactory ()
    {
        _powerUpClasses[(int)PowerUpType.Expand] = new Expand();
        _powerUpClasses[(int)PowerUpType.Shrink] = new Shrink();
        _powerUpClasses[(int)PowerUpType.Magnet] = new Magnet();
        _powerUpClasses[(int)PowerUpType.Death] = new Death();
        _powerUpClasses[(int)PowerUpType.Breakthrough] = new Breakthrough();
        _powerUpClasses[(int)PowerUpType.MultiBall] = new MultiBall();
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
        return _powerUpClasses[(int)type]; // copy if necessary
    }

    public static PowerUp CreateRandomPowerUp()
    {
        int index = SRandom.Int(_totalFrequency);
        PowerUpType type = _randomTypeLookup[index];
        return PowerUpFactory.CreatePowerUp(type);
    }


}
using System;

namespace Wall;

public static class Constants
{
    public static float MinimumBallVerticalVelocity => 100f;

    public static float MaximumBallAngle => MathF.PI / 2;

    public static int StartLives => 2;

    public static float InitialBallSpeed => 400;

    public static float LevelUpBallSpeedIncrease => 100;
}
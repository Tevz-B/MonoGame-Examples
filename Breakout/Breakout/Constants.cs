using System;

namespace Breakout;

public static class Constants
{
    public static float MinimumBallVerticalVelocity => 100f;

    public static float MaximumBallAngle => MathF.PI / 2f;

    public static int StartLives => 2;

    public static float InitialBallSpeed => 400;

    public static float LevelUpBallSpeedIncrease => 100;

    public static float BallSpeedUp => 1.01f;
    
    public static float PowerUpChance => 0.5f;
    
    public static float PowerUpSpeed => 100f;
    
    public static float InitialPaddleWidth => 150;
    
    public static float MinimumPaddleWidth => 50;
    
    public static float MaximumPaddleWidth => 400;
    
    public static float PaddleWidthChange => 50;

    public static double ChangeSizeDuration => 10;

    public static double BreakthroughDuration => 5;

    public static int MagnetPower => 5;



}
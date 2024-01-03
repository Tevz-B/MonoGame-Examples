namespace friHockey_v5;

public static class Constants
{
    // TODO convert to const data
    public static float VelocitySmoothing()
    {
        return 0.5f;
    }

    public static float PuckCoefficientOfRestitution()
    {
        return 0.9f;
    }

    public static float PuckFriction()
    {
        return 0.005f;
    }

    public static float PuckMaximumSpeed()
    {
        return 80f;
    }

    public static int WinScore()
    {
        return 1;
    }

    public const string ProgressFilePath = "FriHockeySave";
}
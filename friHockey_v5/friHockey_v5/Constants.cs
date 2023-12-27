namespace friHockey_v5;

public static class Constants
{
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
        return 0.001f;
    }

    public static float PuckMaximumSpeed()
    {
        return 800f;
    }

    public static int WinScore()
    {
        return 3;
    }

    public static string ProgressFilePath()
    {
        return "FriHockeySave";
    }
}
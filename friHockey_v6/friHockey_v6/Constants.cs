namespace friHockey_v6;

public static class Constants
{
    // Puck
    public const float PuckCoefficientOfRestitution = 0.9f;
    public const float PuckFriction =  0.001f;
    public const float PuckMaximumSpeed = 800f;
    public const float PuckMass = 1f;

    // Mallet
    public const float MalletMass = 20f;

    // Game
    public const int WinScore = 3;
    public const float VelocitySmoothing = 0.5f;
    public const string ProgressFilePath = "FriHockeySave";
}
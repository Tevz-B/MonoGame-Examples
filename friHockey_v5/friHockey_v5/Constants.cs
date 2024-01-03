namespace friHockey_v5;

public static class Constants
{
    // Puck
    public const float PuckCoefficientOfRestitution = 0.9f;
    public const float PuckFriction =  0.005f;
    public const float PuckMaximumSpeed = 80f;


    // Mallet
    public const float MalletMass = 10f;

    // Game
    public const int WinScore = 3;
    public const float VelocitySmoothing = 0.5f;
    public const string ProgressFilePath = "FriHockeySave";
}
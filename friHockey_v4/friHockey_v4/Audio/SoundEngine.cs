using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace friHockey_v4.Audio;

public class SoundEngine : GameComponent
{
    protected SoundEffect[] soundEffects = new SoundEffect[(int)SoundEffectType.LastType];
    SoundEngine instance;
    
    public SoundEngine(Game game)
        : base(game)
    {
        instance = new SoundEngine(game);
        game.Components.AddComponent(instance);
    }

    void initialize()
    {
        soundEffects[SoundEffectTypePuckMallet] = this.Game.Content.Load("PuckMallet");
        soundEffects[SoundEffectTypePuckWall] = this.Game.Content.Load("PuckWall");
        soundEffects[SoundEffectTypeLose] = this.Game.Content.Load("Lose");
        soundEffects[SoundEffectTypeWin] = this.Game.Content.Load("Win");
    }

    public void Play(SoundEffectType type)
    {
        soundEffects[type].Play();
    }

    public static void Play(SoundEffectType type)
    {
        instance.Play(type);
    }

    void Dealloc()
    {
        for (int i = 0; i < SoundEffectTypes; i++)
        {
            soundEffects[i];
        }

    }


}
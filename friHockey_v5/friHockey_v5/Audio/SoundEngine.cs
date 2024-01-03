using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace friHockey_v5.Audio;

public sealed class SoundEngine : GameComponent
{
    private SoundEffect[] _soundEffects = new SoundEffect[(int)SoundEffectType.LastType];
    private static SoundEngine _instance;

    private SoundEngine(Game game)
        : base(game)
    {
    }

    public static void Init(Game game)
    {
        _instance = new SoundEngine(game);
        game.Components.Add(_instance);
    }
    

    public static SoundEngine Instance
    {
        get
        {
            if (_instance == null)
                throw new Exception("Sound Engine not initialized.");
            return _instance;
        }
    }

    public override void Initialize()
    {
        _soundEffects[(int)SoundEffectType.PuckMallet] = Game.Content.Load<SoundEffect>("PuckMallet");
        _soundEffects[(int)SoundEffectType.PuckWall] = Game.Content.Load<SoundEffect>("PuckWall");
        _soundEffects[(int)SoundEffectType.Lose] = Game.Content.Load<SoundEffect>("Lose");
        _soundEffects[(int)SoundEffectType.Win] = Game.Content.Load<SoundEffect>("Win");
    }
    
    public static void Play(SoundEffectType type, float pan = 0f)
    {
        _instance.PlaySound(type, pan);
    }

    public void PlaySound(SoundEffectType type, float pan = 0f)
    {
        pan = Math.Clamp(pan, -1f, 1f);
        _soundEffects[(int)type].Play(1, 0, pan);
    }

}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Express.Graphics;

public class AnimatedSprite
{
    protected List<AnimatedSpriteFrame> _frames;
    protected double _duration;
    protected bool _looping;

    public AnimatedSprite(double theDuration)
    {
        _frames = new List<AnimatedSpriteFrame>();
        _duration = theDuration;
    }

    public double Duration
    {
        get => _duration;
        set => _duration = value;
    }

    public bool Looping
    {
        get => _looping;
        set => _looping = value;
    }

    void SetLoopingDuration(double duration)
    {
        _looping = true;
        this._duration = duration;
    }

    public void AddFrame(AnimatedSpriteFrame frame)
    {
        _frames.Add(frame);
        _frames.Sort((x, y) => x.Start.CompareTo(y.Start));
    }

    public Sprite SpriteAtTime(double time)
    {
        if (_looping)
        {
            int loops = (int)System.Math.Floor(time / _duration);
            time -= loops * _duration;
        }

        if (time >= _duration)
        {
            return null;
        }

        for (int i = 0; i < _frames.Count - 1; i++)
        {
            AnimatedSpriteFrame nextFrame = _frames[i + 1];
            if (nextFrame.Start > time)
            {
                AnimatedSpriteFrame frame = _frames[i];
                return frame.Sprite;
            }

        }

        AnimatedSpriteFrame frameDefault = _frames[^1]; // last index
        return frameDefault.Sprite;
    }
}
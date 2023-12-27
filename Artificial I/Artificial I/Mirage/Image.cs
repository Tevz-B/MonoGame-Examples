using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Mirage;

using System;

public class Image
{
    protected Texture2D texture;
    protected Rectangle sourceRectangle;
    protected Color color;
    protected Vector2 position;
    protected Vector2 origin;
    protected Vector2 scale;
    protected float rotation;
    protected float layerDepth;
    //
    //  Image.m
    //  Artificial I
    //
    //  Created by Matej Jan on 21.12.10.
    //  Copyright 2010 Retronator. All rights reserved.
    //
    //
    //  Image.h
    //  Artificial I
    //
    //  Created by Matej Jan on 21.12.10.
    //  Copyright 2010 Retronator. All rights reserved.
    //

    public Image(Texture2D theTexture, Vector2 thePosition)
    {
        texture = theTexture;
        position = thePosition;
        color = Color.White();
        origin = Vector2.Zero();
        scale = Vector2.One();
    }

    public Texture2D Texture
    {
        get
        {
            return texture;
        }
        set
        {
            texture = value;
        }
    }

    public Rectangle SourceRectangle
    {
        get
        {
            return sourceRectangle;
        }
        set
        {
            sourceRectangle = value;
        }
    }

    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }

    public Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }

    public Vector2 Origin
    {
        get
        {
            return origin;
        }
        set
        {
            origin = value;
        }
    }

    public Vector2 Scale
    {
        get
        {
            return scale;
        }
        set
        {
            scale = value;
        }
    }

    public float Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            rotation = value;
        }
    }

    public float LayerDepth
    {
        get
        {
            return layerDepth;
        }
        set
        {
            layerDepth = value;
        }
    }

    public void SetScaleUniform(float value)
    {
        scale.X = value;
        scale.Y = value;
    }
}

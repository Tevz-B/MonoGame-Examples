using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public interface IScene : IEnumerable<object>, IUpdateable
{
    public void Add(object item);
    public void Remove(object item);
    public void Clear();
    
    public event EventHandler ItemAdded;
    public event EventHandler ItemRemoved;
}
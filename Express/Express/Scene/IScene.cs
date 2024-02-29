using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public interface IScene : IEnumerable<object>, IUpdateable
{
    public class SceneEventArgs : EventArgs
    {
        public object Item { get; set; }
    }
    
    public void Add(object item);
    public void Remove(object item);
    public void Clear();
    
    public event EventHandler<SceneEventArgs> ItemAdded;
    public event EventHandler<SceneEventArgs> ItemRemoved;
}
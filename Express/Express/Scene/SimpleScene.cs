using System;
using System.Collections;
using System.Collections.Generic;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public class SimpleScene : GameComponent, IScene
{
    public class SimpleSceneEventArgs : EventArgs
    {
        public object Item { get; set; }
    }
    
    private List<object> _items;
    private List<SceneAction> _actions = new List<SceneAction>();

    // protected virtual EventHandler _onItemAdded;
    // protected EventHandler _onItemRemoved;

    public event EventHandler ItemAdded;
    public event EventHandler ItemRemoved;
    
    // private bool _enabled = true;
    // private int _updateOrder = 0;

    // public int UpdateOrder
    // {
    //     get => _updateOrder;
    //     set => _updateOrder = value;
    // }

    // public bool Enabled => _enabled;
    
    // public int UpdateOrder => _updateOrder;

    // public event EventHandler<EventArgs> EnabledChanged;
    // public event EventHandler<EventArgs> UpdateOrderChanged;

    IEnumerator<object> IEnumerable<object>.GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
        return _items.GetEnumerator();
    }
    
    public SimpleScene(Game game)
        : base(game)
    {
        _items = new List<object>();
    }
    
    public override void Update(GameTime gameTime)
    {
        // TODO events
        for (int i = 0; i < _actions.Count; i++)
        {
            SceneAction action = _actions[i];
            object item = action.Item;
            ISceneUser sceneUser = item as ISceneUser;
            if (action.Operation == SceneAction.SceneOperation.Add)
            {
                _items.Add(item);
                if (sceneUser is not null)
                {
                    sceneUser.Scene = this;
                    sceneUser.AddedToScene(this);
                }

                ItemAdded?.Invoke(this, new SimpleSceneEventArgs{Item = item});// GridScene uses this
            }
            else
            {
                _items.Remove(item);
                if (sceneUser is not null)
                {
                    sceneUser.Scene = null;
                    sceneUser.RemovedFromScene(this);
                }

                ItemRemoved?.Invoke(this, new SimpleSceneEventArgs{Item = item});  
            }
        }
        _actions.Clear();
        
        base.Update(gameTime);
    }

    public void Add(object item)
    {
        _actions.Add(new SceneAction(SceneAction.SceneOperation.Add, item));
    }

    public void Remove(object item)
    {
        _actions.Add(new SceneAction(SceneAction.SceneOperation.Remove, item));
    }
    
    public void Clear()
    {
        _items.Clear();
    }

    // public object this[int index]
    // {
    //     get => _items[index];
    //     set => _items[index] = value;
    // }
}
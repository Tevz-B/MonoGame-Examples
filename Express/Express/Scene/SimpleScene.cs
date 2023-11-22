using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public class SimpleScene : GameComponent, IScene
{
    private List<object> _items;
    
    private bool _enabled = true;
    private int _updateOrder;
    private int _count;
    private bool _isReadOnly;

    public bool Enabled => _enabled;
    public int UpdateOrder => _updateOrder;

    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;

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
    }
    
    public override void Update(GameTime gameTime)
    {
        // TODO events
        throw new NotImplementedException();
    }
    
    
    
    
    
    
    // IList 

    public void Add(object item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(object item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(object[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public bool Remove(object item)
    {
        return _items.Remove(item);
    }

    public int Count => _count;

    public bool IsReadOnly => _isReadOnly;

    public int IndexOf(object item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, object item)
    {
        _items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public object this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
}
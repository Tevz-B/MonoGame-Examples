using System;
using System.Collections;
using System.Collections.Generic;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public class GridScene : SimpleScene
{
    protected Dictionary<Point, ArrayList> _grid;

    public GridScene(Game game)
        : base(game)
    {
        _grid = new();
        ItemAdded += ItemAddedToParent;
        ItemRemoved += ItemRemovedFromParent;
    }

    public ArrayList GetItemsAt(Point gridCoordinate)
    {
        ArrayList itemsAtCoordinate = _grid.TryGetValue(gridCoordinate, out var value) ? value : new();
        return new ArrayList(itemsAtCoordinate);
    }

    public ArrayList GetItemsAround(Point gridCoordinate, int distance)
    {
        ArrayList itemsAround = new ArrayList();
        for (int i = gridCoordinate.X - distance; i <= gridCoordinate.X + distance; i++)
        {
            for (int j = gridCoordinate.Y - distance; j <= gridCoordinate.Y + distance; j++)
            {
                ArrayList itemsAtCoordinate = _grid.TryGetValue(new Point(i, j), out var value) ? value : new();
                itemsAround.AddRange(itemsAtCoordinate);
            }
        }

        return itemsAround;
    }

    private void ItemAddedToParent(object scene, IScene.SceneEventArgs e)
    {
        Point? gridCoordinate = CalculateGridCoordinate(e.Item);
        if (gridCoordinate.HasValue)
        {
            ArrayList itemsAtCoordinate = _grid.TryGetValue(gridCoordinate.Value, out var value) ? value : null;
            if (itemsAtCoordinate is null)
            {
                itemsAtCoordinate = new ArrayList();
                _grid[gridCoordinate.Value] = itemsAtCoordinate;
            }

            itemsAtCoordinate.Add(e.Item);
        }
    }

    private void ItemRemovedFromParent(object scene, IScene.SceneEventArgs e)
    {
        Point? gridCoordinate = CalculateGridCoordinate(e.Item);
        if (gridCoordinate.HasValue)
        {
            ArrayList itemsAtCoordinate = _grid.TryGetValue(gridCoordinate.Value, out var value) ? value : new();
            itemsAtCoordinate.Remove(e.Item);
        }
    }

    public Point? CalculateGridCoordinate(object item)
    {
        Vector2? position = null;
        if (item is Vector2 vectorItem)
        {
            position = vectorItem;
        }
        else if (item is IPosition itemWithPosition)
        {
            position = itemWithPosition.Position;
        }

        if (position.HasValue)
        {
            return new Point((int)MathF.Floor(position.Value.X), (int)MathF.Floor(position.Value.Y));
        }
        else
        {
            return null;
        }
    }

    public Point CalculateGridCoordinate(IPosition item)
    {
        return CalculateGridCoordinate(item.Position);
    }

    public Point CalculateGridCoordinate(Vector2 item)
    {
        item.Floor();
        return item.ToPoint();
    }
}
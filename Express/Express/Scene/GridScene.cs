using System;
using System.Collections;
using System.Collections.Generic;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace Express.Scene;

 public class GridScene : SimpleScene
    {
        protected Dictionary<Point, ArrayList> _grid;

        GridScene(Game theGame)
            : base (theGame)
        {
            _grid = new Dictionary<Point, ArrayList>();
            // TODO
            // base.ItemAdded.SubscribeDelegate(Delegate.DelegateWithTargetMethod(this, @selector (itemAddedToParent:eventArgs:)));
            // base.ItemRemoved.SubscribeDelegate(Delegate.DelegateWithTargetMethod(this, @selector (itemRemovedFromParent:eventArgs:)));
        }

        public ArrayList GetItemsAt(Point gridCoordinate)
        {
            ArrayList itemsAtCoordinate = _grid[gridCoordinate];
            return new ArrayList(itemsAtCoordinate);
        }

        public ArrayList GetItemsAround(Point gridCoordinate, int distance)
        {
            ArrayList itemsAround = new ArrayList();
            for (int i = gridCoordinate.X - distance; i <= gridCoordinate.X + distance; i++)
            {
                for (int j = gridCoordinate.Y - distance; j <= gridCoordinate.Y + distance; j++)
                {
                    ArrayList itemsAtCoordinate = _grid[new Point(i, j)];
                    itemsAround.AddRange(itemsAtCoordinate);
                }
            }

            return itemsAround;
        }

        // TODO
        
        // void ItemAddedToParentEventArgs(IScene scene, SceneEventArgs e)
        // {
        //     Point gridCoordinate = this.CalculateGridCoordinateForItem(e.Item);
        //     if (gridCoordinate)
        //     {
        //         ArrayList itemsAtCoordinate = _grid[gridCoordinate];
        //         if (!itemsAtCoordinate)
        //         {
        //             itemsAtCoordinate = new ArrayList();
        //             _grid.SetObjectForKey(itemsAtCoordinate, gridCoordinate);
        //         }
        //
        //         itemsAtCoordinate.Add(e.Item);
        //     }
        //
        // }
        //
        // void ItemRemovedFromParentEventArgs(IScene scene, SceneEventArgs e)
        // {
        //     Point gridCoordinate = this.CalculateGridCoordinateForItem(e.Item);
        //     if (gridCoordinate)
        //     {
        //         ArrayList itemsAtCoordinate = _grid[gridCoordinate];
        //         itemsAtCoordinate.Remove(e.Item);
        //     }
        //
        // }

        public Point? CalculateGridCoordinate(object item)
        {
            IPosition itemWithPosition = item is IPosition itemWithPos ? itemWithPos : null;
            Vector2? position = item is Vector2 ? (Vector2)item : null;
            if (itemWithPosition is  not null)
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
    }
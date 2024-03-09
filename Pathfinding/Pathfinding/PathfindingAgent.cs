using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Pathfinding;

public class PathfindingAgent : Agent, ISceneUser, ICustomCollider
{
    protected List<Vector2> _waypoints = new();
    protected GridScene _scene;
    protected List<SearchNode> _fringe = new();
    protected Dictionary<Point, int> _expandedPoints = new();

    public List<Vector2> Waypoints => _waypoints;

    public override void GoTo(Vector2 theTarget)
    {
        _waypoints.Clear();
        ArrayList gridCoordinatesToGoal = FindPathTo(_scene.CalculateGridCoordinate(theTarget));
        foreach (Point gridCoordinate in gridCoordinatesToGoal)
        {
            _waypoints.Add(new Vector2(gridCoordinate.X + 0.5f, gridCoordinate.Y + 0.5f));
        }

        _target = null;
    }

    public ArrayList FindPathTo(Point goal)
    {
        ArrayList result = new ArrayList();
        _fringe.Clear();
        _expandedPoints.Clear();
        
        // Check if goal is Obstacle
        foreach (var itemAtGoal in _scene.GetItemsAt(goal))
        {
            if (itemAtGoal is Obstacle)
            {
                return result;
            }
        }
        
        // Add initial node from which the search starts.
        SearchNode initialNode = SearchNode.Node();
        initialNode.Point = _scene.CalculateGridCoordinate(_position);
        _fringe.Add(initialNode);
        while (true)
        {
            // If fringe is empty we're in a dead end with nowhere to go. Fail!
            if (_fringe.Count == 0)
            {
                return result;
            }

            // Get the node with smallest cost.
            _fringe.Sort((x, y) => -x.TotalCost.CompareTo(y.TotalCost)); // descending or ascending
            int lastIdx = _fringe.Count - 1;
            SearchNode node = _fringe[lastIdx];
            _fringe.RemoveAt(lastIdx);
            
            // If this is the goal state return with success.
            if (node.Point == goal)
            {
                while (true)
                {
                    if (node.Parent is null)
                    {
                        return result;
                    }
                    else
                    {
                        result.Add(node.Point);
                        node = node.Parent;
                    }
                }
            }

            // Not a goal state - expand the node and add the successors to the list.
            _fringe.AddRange(ExpandGoal(node, goal));
        }
    }

    public List<SearchNode> ExpandGoal(SearchNode node, Point goal)
    {
        List<SearchNode> successors = new();
        foreach (Point point in NeighboursOf(node.Point))
        {
            int newCost = node.RealCost + 1;

            if (!_expandedPoints.ContainsKey(point) || newCost < _expandedPoints[point])
            {
                _expandedPoints[point] = newCost;
                SearchNode newNode = SearchNode.Node();
                newNode.Point = point;
                newNode.Parent = node;
                newNode.RealCost = newCost;
                newNode.HeuristicCost = Math.Abs(goal.X - point.X) + Math.Abs(goal.Y - point.Y);
                successors.Add(newNode);
            }
        }

        return successors;
    }

    public ArrayList NeighboursOf(Point point)
    {
        ArrayList neighbours = new ArrayList();
        AddPointIfClearTo(new Point(point.X - 1, point.Y), neighbours);
        AddPointIfClearTo(new Point(point.X + 1, point.Y), neighbours);
        AddPointIfClearTo(new Point(point.X, point.Y - 1), neighbours);
        AddPointIfClearTo(new Point(point.X, point.Y + 1), neighbours);
        return neighbours;
    }

    public void AddPointIfClearTo(Point point, ArrayList array)
    {
        ArrayList itemsAtPoint = _scene.GetItemsAt(point);
        if (itemsAtPoint.Count == 0 || (itemsAtPoint.Count == 1 && itemsAtPoint[0] == this))
        {
            array.Add(point);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (_target is null)
        {
            if (_waypoints.Count > 0)
            {
                base.GoTo(_waypoints.Last());
                _waypoints.RemoveAt(_waypoints.Count - 1);
            }
            else
            {
                _velocity = Vector2.Zero;
            }
        }

        base.Update(gameTime);
    }

    void ICustomCollider.CollidedWith(object item)
    {
        if (_waypoints.Count > 0)
        {
            GoTo(_waypoints[0]);
        }
    }


    public IScene Scene
    {
        get => _scene;
        set => _scene = (GridScene)value;
    }

    public void AddedToScene(IScene scene)
    {
    }

    public void RemovedFromScene(IScene scene)
    {
    }
}
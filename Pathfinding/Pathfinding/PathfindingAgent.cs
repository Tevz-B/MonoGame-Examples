using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Express.Scene;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace Pathfinding;

public class PathfindingAgent : Agent, ISceneUser
{
        protected List<Vector2> waypoints = new();
        protected GridScene scene;
        protected List<SearchNode> fringe = new();
        protected Dictionary<Point, int> expandedPoints = new();

        public ArrayList Waypoints => waypoints;

        void GoTo(Vector2 theTarget)
        {
            waypoints.Clear();
            ArrayList gridCoordinatesToGoal = this.FindPathTo(scene.CalculateGridCoordinate(theTarget));
            foreach (Point gridCoordinate in gridCoordinatesToGoal)
            {
                waypoints.Add(new Vector2(gridCoordinate.X + 0.5f, gridCoordinate.Y + 0.5f));
            }
            _target = null;
        }

        public ArrayList FindPathTo(Point goal)
        {
            ArrayList result = new ArrayList();
            fringe.Clear();
            expandedPoints.Clear();
            SearchNode initialNode = SearchNode.Node();
            initialNode.Point = scene.CalculateGridCoordinate(_position);
            fringe.Add(initialNode);
            while (true)
            {
                if (fringe.Count == 0)
                {
                    return result;
                }

                fringe.Sort((x,y) => x.TotalCost.CompareTo(y)); // descending or ascending
                int lastIdx = fringe.Count - 1;
                SearchNode node = fringe[lastIdx];
                fringe.RemoveAt(lastIdx);
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

                fringe.AddRange(this.ExpandGoal(node, goal));
            }
        }

        public List<SearchNode> ExpandGoal(SearchNode node, Point goal)
        {
            List<SearchNode> successors = new();
            foreach (Point point in this.NeighboursOf(node.Point))
            {
                int newCost = node.RealCost + 1;
                
                if (!expandedPoints.ContainsKey(point) || newCost < expandedPoints[point])
                {
                    expandedPoints[point] = newCost;
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
            this.AddPointIfClearTo(new Point(point.X - 1, point.Y), neighbours);
            this.AddPointIfClearTo(new Point(point.X + 1, point.Y), neighbours);
            this.AddPointIfClearTo(new Point(point.X, point.Y - 1), neighbours);
            this.AddPointIfClearTo(new Point(point.X, point.Y + 1), neighbours);
            return neighbours;
        }

        public void AddPointIfClearTo(Point point, ArrayList array)
        {
            ArrayList itemsAtPoint = scene.GetItemsAt(point);
            if (itemsAtPoint.Count == 0 || (itemsAtPoint.Count == 1 && itemsAtPoint[0] == this))
            {
                array.Add(point);
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (_target is null)
            {
                if (waypoints.Count > 0)
                {
                    base.GoTo(waypoints.Last());
                    waypoints.RemoveAt(waypoints.Count - 1);
                }
                else
                {
                    _velocity = Vector2.Zero;
                }

            }

            base.Update(gameTime);
        }

        void CollidedWithItem(object item)
        {
            if (waypoints.Count > 0)
            {
                this.GoTo(waypoints[0]);
            }
        }


}
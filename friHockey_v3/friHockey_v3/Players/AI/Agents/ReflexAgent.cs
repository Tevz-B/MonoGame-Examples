using System.Collections;
using friHockey_v3.Scene;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Players.AI.Agents;

public class ReflexAgent : AIPlayer
{
        protected float _attackFactor;

        protected ReflexAgent(Game theGame, Mallet theMallet, Level theLevel, PlayerPosition thePosition)
            : base(theGame, theMallet, theLevel, thePosition)
        {
        }
        
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Read percepts.
            var defenseDangers = GetDefenseDangers();
            int defenseSpotIndex = 0;
            float defenseSpotDanger = 0;
            // Find the largest danger
            for (int i = 0; i < defenseDangers.Count; i++)
            {
                float danger = defenseDangers[i];
                if (danger > defenseSpotDanger)
                {
                    defenseSpotIndex = i;
                    defenseSpotDanger = danger;
                }

            }
            // Calculate offense opportunity.
	
            // The faster the pack, the less offensive you should be.
            float offense = 800 - _level.Puck.Velocity.Length();
            
            // Fade out offense from 150 to 250 y coordinate.
            float side = (250 - _level.Puck.Position.Y) / 100;
            if (side > 1) side = 1;

            if (side < 0) side = 0;

            offense *= side;

            // Fade out offense from 0 to 100 vertical puck velocity
            float sideDirection = 1 - _level.Puck.Velocity.Y / 100;
            if (sideDirection > 1) sideDirection = 1;

            if (sideDirection < 0) sideDirection = 0;

            offense *= sideDirection;
            
            // Don't go into offense if puck is moving away faster than you.
            if (_attackSpeed < _level.Puck.Velocity.Y) offense = 0;

            // If we have no offense or if the most dangerous spot is bigger then offense, weighted with opponent's aggressiveness. 
            if (offense <= 0 || defenseSpotDanger > offense * _attackFactor)
            {
                Vector2 defenseSpot = _level.DefenseSpots[defenseSpotIndex];
                Vector2 offset = Vector2.Normalize(_level.Puck.Position - defenseSpot) * _mallet.Radius * 2f;
                Vector2 defenseTarget = offset + defenseSpot;
                
                MoveTowards(defenseTarget);
            }
            else
            {
                // Find best offense spot
                var offenseWeaknesses = GetOffenseWeaknesses();
                int offenseSpotIndex = 0;
                float offenseSpotWeakness = 0;
                for (int i = 0; i < offenseWeaknesses.Count; i++)
                {
                    float weakness = offenseWeaknesses[i];
                    if (weakness > offenseSpotWeakness * 1.05)
                    {
                        offenseSpotIndex = i;
                        offenseSpotWeakness = weakness;
                    }

                }

                Vector2 offenseSpot = _level.OffenseSpots[offenseSpotIndex];
                
                // calculate where the puck should go after collision
                Vector2 desiredPuckDirection = Vector2.Normalize(offenseSpot - _level.Puck.Position);
                Vector2 puckDifference = _level.Puck.Position - _mallet.Position;
                float distance = puckDifference.Length();
                
                // Try to get behind the puck from the desired direction;
                Vector2 offset = desiredPuckDirection * -distance * 0.9f;
                Vector2 offenseTarget = offset + _level.Puck.Position;
                
                AttackTowards(offenseTarget);
            }

        }

}
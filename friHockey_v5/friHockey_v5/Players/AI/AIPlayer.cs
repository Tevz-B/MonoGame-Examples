using System.Collections.Generic;
using friHockey_v5.Players.AI.Opponents;
using friHockey_v5.Scene;
using friHockey_v5.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI;

public class AIPlayer : Player
{
    protected Level _level;
    // AI properties
    protected string _name = "";
    protected float _speed;
    protected float _attackSpeed;
    protected List<string> _quotes = new List<string>();
    private bool _attack;

    private Vector2 _target;

    public ref Level Level => ref _level;

    public ref Vector2 Target => ref _target;

    public List<string> Quotes => _quotes;
    
    public string Name => _name;
    
    public static OpponentType OpponentType() => 0;

    public static LevelType LevelType()
    {
        return 0;
    }

    public static string PortraitPath()
    {
        return null;
    }

    public static string HiddenPortraitPath()
    {
        return null;
    }

    public static string FullPortraitPath()
    {
        return null;
    }

    public AIPlayer(Game theGame, Mallet theMallet, Level theLevel, PlayerPosition thePosition)
        : base (theGame, theMallet, thePosition)
    {
        _level = theLevel;
    }

    public List<float> GetDefenseDangers()
    {
        var defenseDangers = new List<float>(_level.DefenseSpots.Count);
        foreach (Vector2 defenseSpot in _level.DefenseSpots)
        {
            Vector2 difference = defenseSpot - _level.Puck.Position;
            float distanceDanger = difference.Length();
            difference.Normalize();
            float velocityDanger = Vector2.Dot(_level.Puck.Velocity, difference);
            float danger = 500 - distanceDanger + (velocityDanger / distanceDanger) * 1000;
            if (danger < 0) danger = 0;

            defenseDangers.Add(danger);
        }
        return defenseDangers;
    }

    public List<float> GetOffenseWeaknesses()
    {
        var offenseWeaknesses = new List<float>(_level.OffenseSpots.Count);
        foreach (Vector2 offenseSpot in _level.OffenseSpots)
        {
            Vector2 myDifference = offenseSpot - _level.TopMallet.Position;
            float myDistance = myDifference.Length();
            Vector2 opponentDifference = offenseSpot - _level.BottomMallet.Position;
            float opponentDistance = opponentDifference.Length();
            float weakness = opponentDistance / myDistance;
            offenseWeaknesses.Add(weakness);
        }
        return offenseWeaknesses;
    }

    // Actions
    public void MoveTowards(Vector2 theTarget)
    {
        this.MoveTowardsAttack(theTarget, false);
    }

    public void AttackTowards(Vector2 theTarget)
    {
        this.MoveTowardsAttack(theTarget, true);
    }

    public void MoveTowardsAttack(Vector2 theTarget, bool isAttack)
    {
        _target = theTarget;
        _attack = isAttack;
        // Make sure we don't cross the middle.
        if (_target.Y > 250)
            _target.Y = 250;
        
        // Don't go into walls.
        if (_target.X < 30)
            _target.X = 30;
        
        if (_target.X > 290)
            _target.X = 290;
        
        // Don't move too close to the edge.
        if (_target.Y < 60)
            _target.Y = 60;
        
        // Don't block puck in corner.
        if (_level.Puck.Position.Y < 60 && (_level.Puck.Position.X < 50 || _level.Puck.Position.X > 260))
            _target.X = 160;
    }

    public override void Update(GameTime gameTime)
    {
        var target = _target;
        Vector2 difference = target - _mallet.Position;
        float distance = difference.Length();
        float maxMove = (_attack ? _attackSpeed : _speed) * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        if (distance < maxMove)
        {
            _mallet.Position = target;
        }
        else
        {
            difference.Normalize();
            difference *= maxMove;
            _mallet.Position += difference;
        }
    }
}
using System.Collections.Generic;
using friHockey_v5.Level;
using friHockey_v5.Players.AI.Opponents;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI;

public abstract class AIPlayer : Player
{
    // Define in derived class!
    // public const string Name = "Iceman";
    // public const string PortraitPath = "iceman-small";
    // public const string HiddenPortraitPath = "iceman-hidden";
    // public const string FullPortraitPath = "iceman";
    public const LevelType LevelType = friHockey_v5.Level.LevelType.Hockey;
    
    protected LevelBase _levelBase;
    // AI properties
    protected float _speed;
    protected float _attackSpeed;
    protected List<string> _quotes = new List<string>();
    private bool _attack;

    protected OpponentType _opponentType;

    private Vector2 _target;

    public ref LevelBase LevelBase => ref _levelBase;

    public ref Vector2 Target => ref _target;

    public List<string> Quotes => _quotes;
    
    public OpponentType OpponentType => _opponentType;

    public virtual LevelType GetLevelType()
    {
        return LevelType;
    }

    protected AIPlayer(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base (theGame, theMallet, thePosition)
    {
        _levelBase = theLevelBase;
    }

    public List<float> GetDefenseDangers()
    {
        var defenseDangers = new List<float>(_levelBase.DefenseSpots.Count);
        foreach (Vector2 defenseSpot in _levelBase.DefenseSpots)
        {
            Vector2 difference = defenseSpot - _levelBase.Puck.Position;
            float distanceDanger = difference.Length();
            difference.Normalize();
            float velocityDanger = Vector2.Dot(_levelBase.Puck.Velocity, difference);
            float danger = 500 - distanceDanger + (velocityDanger / distanceDanger) * 1000;
            if (danger < 0) danger = 0;

            defenseDangers.Add(danger);
        }
        return defenseDangers;
    }

    public List<float> GetOffenseWeaknesses()
    {
        var offenseWeaknesses = new List<float>(_levelBase.OffenseSpots.Count);
        foreach (Vector2 offenseSpot in _levelBase.OffenseSpots)
        {
            Vector2 myDifference = offenseSpot - _levelBase.TopMallet.Position;
            float myDistance = myDifference.Length();
            Vector2 opponentDifference = offenseSpot - _levelBase.BottomMallet.Position;
            float opponentDistance = opponentDifference.Length();
            float weakness = opponentDistance / myDistance;
            offenseWeaknesses.Add(weakness);
        }
        return offenseWeaknesses;
    }

    // Actions
    protected void MoveTowards(Vector2 theTarget)
    {
        MoveTowardsAttack(theTarget, false);
    }

    protected void AttackTowards(Vector2 theTarget)
    {
        MoveTowardsAttack(theTarget, true);
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
        if (_levelBase.Puck.Position.Y < 60 && (_levelBase.Puck.Position.X < 50 || _levelBase.Puck.Position.X > 260))
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
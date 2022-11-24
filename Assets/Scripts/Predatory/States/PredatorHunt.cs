using UnityEngine;

namespace Predatory.States{
    public class PredatorHunt : PredatorState{
        private readonly Entity _target;

        public PredatorHunt(Entity context, Entity target) : base(context){
            _target = target;
        }
        
        public override void Enter(){
            Debug.Log(_context.gameObject.name + ": Hunt: Start");
        }

        public override void Update(){
            Debug.Log(_context.gameObject.name + ": Hunt: Update");

            _context.Agent.SetDestination(_target.Position);
        }

        public override void Exit(){
            Debug.Log(_context.gameObject.name + ": Hunt: Exit");
        }

        public override void CheckTransitions(){
            if (Vector3.Distance(_context.Position, _target.Position) >= _context.SightRange){
                if (Prey.GetPreysInRange(_context, out var inRange) == 0)
                    _context.CurrentState = new PredatorIdle(_context);
                else
                    _context.CurrentState = new PredatorHunt(_context, inRange[0]);
            }
            else if (Vector3.Distance(_target.Position, _context.Position) <= _context.AttackRange)
                _context.CurrentState = new PredatorAttack(_context, _target);
        }
    }
}
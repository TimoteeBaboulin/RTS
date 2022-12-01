using UnityEngine;

namespace Predatory.States{
    public class HuntPrey : State{
        private readonly Entity _target;

        public HuntPrey(Entity context, Entity target) : base(context){
            _target = target;
        }

        public override void Enter(){ }

        public override void Update(){
            if (_target != null) _context.Agent.SetDestination(_target.Position);
        }

        public override void Exit(){
            _context.Agent.SetDestination(_context.Position);
        }

        public override void CheckTransitions(){
            if (_context.IsPredatorClose){
                _context.CurrentState = new RunAway(_context);
            }
            else if (_context.GetFood(out var foods) > 0){
                _context.CurrentState = new HuntMeal(_context, foods[0]);
            }
            else if (_target == null || Vector3.Distance(_context.Position, _target.Position) > _context.SightRange){
                if (_context.GetPreys(out var preys) > 0)
                    _context.CurrentState = new HuntPrey(_context, preys[0]);
                else
                    _context.CurrentState = new Idle(_context);
            }
            else if (Vector3.Distance(_target.Position, _context.Position) <= _context.AttackRange){
                _context.CurrentState = new Attack(_context, _target);
            }
        }
    }
}
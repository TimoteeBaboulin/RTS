using UnityEngine;

namespace Predatory.States{
    public class HuntMeal : State{
        private readonly Food _target;

        public HuntMeal(Entity context, Food target) : base(context){
            _target = target;
        }

        public override void Enter(){
            _context.Agent.SetDestination(_target.Position);
        }

        public override void Update(){ }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_context.GetPredators(out var predators) > 0){
                _context.CurrentState = new RunAway(_context);
                return;
            }
            
            if (_target == null){
                if (_context.GetFood(out var foods) > 0)
                    _context.CurrentState = new HuntMeal(_context, foods[0]);
                else if (_context.GetPreys(out var preys) > 0)
                    _context.CurrentState = new HuntPrey(_context, preys[0]);
                else
                    _context.CurrentState = new Idle(_context);
            }
            else if (Vector3.Distance(_context.Position, _target.Position) <= _context.AttackRange)
                _context.CurrentState = new EatMeal(_context, _target);
        }
    }
}
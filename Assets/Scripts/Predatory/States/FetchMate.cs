using UnityEngine;

namespace Predatory.States{
    public class FetchMate : State{
        private readonly Entity _mate;

        public FetchMate(Entity context, Entity mate) : base(context){
            _mate = mate;
        }

        public override void Enter(){ }

        public override void Update(){
            if (_mate != null) _context.Agent.SetDestination(_mate.Position);
        }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_context.GetPredators(out var inRange) > 0){
                _context.CurrentState = new RunAway(_context);
            }
            else if (_context.IsHungry || _mate == null || _mate.IsHungry){
                _context.CurrentState = new Patrol(_context);
            }
            else if (Vector3.Distance(_context.Position, _mate.Position) < _context.AttackRange){
                _context.CurrentState = new Mate(_context, _mate);
                _mate.CurrentState = new Mate(_mate, _context);
            }
        }
    }
}
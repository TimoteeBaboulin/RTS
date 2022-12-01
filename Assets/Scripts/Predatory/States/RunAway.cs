using UnityEngine.AI;

namespace Predatory.States{
    public class RunAway : State{
        public RunAway(Entity context) : base(context){ }

        public override void Enter(){ }

        public override void Update(){
            if (_context.GetPredators(out var inRange) == 0) return;

            var direction = _context.Position - inRange[0].Position;
            if (NavMesh.SamplePosition(_context.Position + direction.normalized, out var hit, 5, NavMesh.AllAreas))
                _context.Agent.SetDestination(hit.position);
        }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_context.IsPredatorClose) return;
            if (_context.GetFood(out var foods) > 0)
                _context.CurrentState = new HuntMeal(_context, foods[0]);
            else if (_context.GetPreys(out var preys) > 0)
                _context.CurrentState = new HuntPrey(_context, preys[0]);
            else
                _context.CurrentState = new Idle(_context);
        }
    }
}
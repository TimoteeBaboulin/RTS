namespace Predatory.States{
    public class Idle : State{
        public Idle(Entity context) : base(context){ }
        
        public override void Enter(){ }

        public override void Update(){ }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_context.GetPredators(out var predators) > 0)
                _context.CurrentState = new RunAway(_context);
            else if (_context.GetFood(out var foods) > 0)
                _context.CurrentState = new HuntMeal(_context, foods[0]);
            else if (_context.GetPreys(out var preys) > 0)
                _context.CurrentState = new HuntPrey(_context, preys[0]);
            else if (_context.IsHungry) _context.CurrentState = new Patrol(_context);
        }
    }
}
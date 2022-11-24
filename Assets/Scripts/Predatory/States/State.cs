namespace Predatory.States{
    public abstract class State{
        protected Entity _context;

        protected State(Entity context){
            _context = context;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();

        public abstract void CheckTransitions();
    }

    public abstract class PredatorState : State{
        protected PredatorState(Entity context) : base(context){}

        protected bool CheckForFood(){
            if (Meat.GetFoodInRange(_context).Count > 0)
                return true;
            
            return false;
        }

        protected bool CheckForPrey(){
            if (Prey.GetPreysInRange(_context, out var inRange) > 0){
                _context.CurrentState = new PredatorHunt(_context, inRange[0]);
                return true;
            }

            return false;
        }
    }
}
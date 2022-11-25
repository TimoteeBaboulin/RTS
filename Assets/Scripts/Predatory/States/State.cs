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
}
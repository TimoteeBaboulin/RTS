using UnityEngine;

namespace Predatory.States{
    public class Mate : State{
        private readonly Entity _mate;
        private float _timer;

        public Mate(Entity context, Entity mate) : base(context){
            _mate = mate;
        }

        public override void Enter(){ }

        public override void Update(){
            _timer += Time.deltaTime;
        }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (!(_timer >= 2f)) return;

            Object.Instantiate(_context.gameObject, _context.Position, Quaternion.identity);
            _context.CurrentState = new Idle(_context);
            _mate.CurrentState = new Idle(_mate);
        }
    }
}
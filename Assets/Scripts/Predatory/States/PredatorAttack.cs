using UnityEngine;

namespace Predatory.States{
    public class PredatorAttack : PredatorState{
        private float _timer;
        private readonly Entity _target;

        public PredatorAttack(Entity context, Entity target) : base(context){
            _target = target;
        }
        
        public override void Enter(){ }

        public override void Update(){
            if (_target == null) return;
            
            if (_timer >= 1){
                _timer = 0;
                _target.CurrentHealth--;
            }

            _timer += Time.deltaTime;
        }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_target != null) return;
            if (Prey.GetPreysInRange(_context, out var inRange) > 0)
                _context.CurrentState = new PredatorHunt(_context, inRange[0]);
            else
                _context.CurrentState = new PredatorIdle(_context);
        }
    }
}
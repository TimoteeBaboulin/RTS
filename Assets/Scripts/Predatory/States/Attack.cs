using UnityEngine;

namespace Predatory.States{
    public class Attack : State{
        private readonly Entity _target;
        private float _timer;

        public Attack(Entity context, Entity target) : base(context){
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
            if (_context.IsPredatorClose){
                _context.CurrentState = new RunAway(_context);
            }
            else if (_target == null || Vector3.Distance(_context.Position, _target.Position) > _context.AttackRange){
                if (_context.GetFood(out var foods) > 0)
                    _context.CurrentState = new HuntMeal(_context, foods[0]);
                if (_context.GetPreys(out var preys) > 0)
                    _context.CurrentState = new HuntPrey(_context, preys[0]);
                else
                    _context.CurrentState = new Idle(_context);
            }
        }
    }
}
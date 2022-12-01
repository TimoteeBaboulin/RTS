using UnityEngine;

namespace Predatory.States{
    public class EatMeal : State{
        private readonly Food _food;
        private float _timer;

        public EatMeal(Entity context, Food food) : base(context){
            _food = food;
        }

        public override void Enter(){
            _context.Agent.SetDestination(_context.Position);
        }

        public override void Update(){
            _timer += Time.deltaTime;
        }

        public override void Exit(){ }

        public override void CheckTransitions(){
            if (_context.IsPredatorClose){
                _context.CurrentState = new RunAway(_context);
                return;
            }

            if (_food != null){
                if (!(_timer >= _food.EatTime)) return;

                _context.CurrentStomach += _food.Nourishment;
                Object.Destroy(_food.gameObject);
            }

            if (_context.GetFood(out var foods) > 0)
                _context.CurrentState = new HuntMeal(_context, foods[0]);
            else if (_context.GetPreys(out var preys) > 0)
                _context.CurrentState = new HuntPrey(_context, preys[0]);
            else
                _context.CurrentState = new Idle(_context);
        }
    }
}
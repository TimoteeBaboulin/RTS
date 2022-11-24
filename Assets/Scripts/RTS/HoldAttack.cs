using UnityEngine;

namespace RTS{
    public class HoldAttack : State{
        private readonly Entity _goal;
        private float _timer;

        public HoldAttack(Entity context, Entity goal) : base(context){
            _goal = goal;
        }

        public override void Start(){
            Debug.Log("HoldAttack: Start");
        }

        public override void Update(){
            Debug.Log("HoldAttack: Start");

            if (_timer >= 1){
                _goal.Health -= 3;
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }

        public override void Quit(){
            Debug.Log("HoldAttack: Start");
        }

        public override void CheckTransitions(){
            if (CheckPlayerInputs()) return;

            var inAttackRange = Entity.GetEntitiesInRange(_context, _context.AttackRange);

            if (inAttackRange.Count == 0)
                _context.CurrentState = new Hold(_context);

            if (_goal == null && inAttackRange.Count > 0)
                _context.CurrentState = new HoldAttack(_context, inAttackRange[0]);
        }
    }
}
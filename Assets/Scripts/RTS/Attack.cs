using UnityEngine;

namespace RTS{
    public class Attack : State{
        private readonly Entity _goal;
        private float _timer;

        public Attack(Entity context, Entity goal) : base(context){
            _goal = goal;
        }

        public override void Start(){
            Debug.Log("Attack: Start");
        }

        public override void Update(){
            Debug.Log("Attack: Update");

            if (_timer >= 1){
                _timer = 0;
                _goal.Health -= 3;
            }

            _timer += Time.deltaTime;
        }

        public override void Quit(){
            Debug.Log("Attack: Quit");
        }

        public override void CheckTransitions(){
            if (CheckPlayerInputs()) return;

            if (_goal != null && Vector3.Distance(_context.transform.position, _goal.transform.position) <=
                _context.AttackRange) return;

            if (Entity.GetEntitiesInRange(_context, _context.AttackRange).Count > 0)
                _context.CurrentState = new Attack(_context, Entity.GetEntitiesInRange(_context, _context.AttackRange)[0]);
            else if (Entity.GetEntitiesInRange(_context).Count > 0)
                _context.CurrentState = new Chase(_context, Entity.GetEntitiesInRange(_context)[0]);
            else
                _context.CurrentState = new Idle(_context);
        }
    }
}
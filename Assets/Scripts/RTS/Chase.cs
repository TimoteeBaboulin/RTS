using UnityEngine;

namespace RTS{
    public class Chase : State{
        private readonly Entity _goal;

        public Chase(Entity context, Entity goal) : base(context){
            _goal = goal;
        }

        public override void Start(){
            Debug.Log("Chase: Start");
        }

        public override void Update(){
            Debug.Log("Chase: Update");

            _context.Agent.SetDestination(_goal.transform.position);
        }

        public override void Quit(){
            _context.Agent.SetDestination(_context.transform.position);
        }

        public override void CheckTransitions(){
            if (CheckPlayerInputs()) return;

            var contextPos = _context.transform.position;
            var goalPos = _goal.transform.position;

            if (Vector3.Distance(contextPos, goalPos) <= _context.AttackRange)
                _context.CurrentState = new Attack(_context, _goal);
            else if (Vector3.Distance(contextPos, goalPos) > _context.SightRange)
                _context.CurrentState = new Idle(_context);
        }
    }
}
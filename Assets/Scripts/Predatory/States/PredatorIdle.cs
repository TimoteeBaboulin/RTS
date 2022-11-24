using UnityEngine;

namespace Predatory.States{
    public class PredatorIdle : PredatorState{
        public PredatorIdle(Entity context) : base(context){ }
        
        public override void Enter(){
            Debug.Log(_context.gameObject.name + ": Idle: Start");

            _context.Agent.SetDestination(_context.Position);
        }

        public override void Update(){
            Debug.Log(_context.gameObject.name + ": PredIdle: Update");
        }

        public override void Exit(){
            Debug.Log(_context.gameObject.name + ": Idle: Exit");
        }

        public override void CheckTransitions(){
            if (Prey.GetPreysInRange(_context, out var inRange) > 0)
                _context.CurrentState = new PredatorHunt(_context, inRange[0]);
        }
    }
}
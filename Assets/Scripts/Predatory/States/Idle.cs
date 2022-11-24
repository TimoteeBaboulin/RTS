using UnityEngine;

namespace Predatory.States{
    public class Idle : State{
        public Idle(Entity context) : base(context){ }

        public override void Enter(){
            Debug.Log(_context.gameObject.name + ": Idle: Start");
        }

        public override void Update(){
            Debug.Log(_context.gameObject.name + ": Idle: Update");
        }

        public override void Exit(){
            Debug.Log(_context.gameObject.name + ": Idle: Exit");
        }

        public override void CheckTransitions(){
        }
    }
}
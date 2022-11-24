using UnityEngine;

namespace RTS{
    public class Idle : State{
        //Base constructor
        public Idle(Entity context) : base(context){ }

        public override void Start(){
            Debug.Log("Idle: Start");
        }

        public override void Update(){
            Debug.Log("Idle: Update");
        }

        public override void Quit(){
            Debug.Log("Idle: Quit");
        }

        public override void CheckTransitions(){
            if (CheckPlayerInputs()) return;

            if (Entity.GetEntitiesInRange(_context).Count > 0)
                _context.CurrentState = new Chase(_context, Entity.GetEntitiesInRange(_context)[0]);
        }
    }
}
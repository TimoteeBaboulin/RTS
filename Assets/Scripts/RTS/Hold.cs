using UnityEngine;

namespace RTS{
    public class Hold : State{
        public Hold(Entity context) : base(context){ }

        public override void Start(){
            _context.Agent.SetDestination(_context.transform.position);
        }

        public override void Update(){
            Debug.Log("Hold: Update");
        }

        public override void Quit(){
            Debug.Log("Hold: Quit");
        }

        public override void CheckTransitions(){
            if (CheckPlayerInputs()) return;

            if (Entity.GetEntitiesInRange(_context, _context.AttackRange).Count > 0)
                _context.CurrentState =
                    new HoldAttack(_context, Entity.GetEntitiesInRange(_context, _context.AttackRange)[0]);
        }
    }
}
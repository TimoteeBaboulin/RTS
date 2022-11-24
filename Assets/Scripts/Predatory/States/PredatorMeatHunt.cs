using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Predatory.States{
    public class PredatorMeatHunt : PredatorState{
        private Food _target;

        public PredatorMeatHunt(Entity context, Food target) : base(context){
            _target = target;
        }
        public override void Enter(){
            _context.Agent.SetDestination(_target.Position);
        }

        public override void Update(){
        }

        public override void Exit(){
        }

        public override void CheckTransitions(){
            if (Vector3.Distance(_context.Position, _target.Position) > _context.SightRange){
                if (CheckForFood()) return;
                if (CheckForPrey()) return;
            } else if (Vector3.Distance(_context.Position, _target.Position) <= _context.AttackRange)
                Debug.Log("Go to meat eating");
        }
    }
}
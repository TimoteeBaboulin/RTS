﻿using UnityEngine;

namespace Predatory.States{
    public class Patrol : State{
        private float _timer = 2;
        public Patrol(Entity context) : base(context){ }
        public override void Enter(){ }

        public override void Update(){
            if (_timer >= 2){
                Vector3 direction = new Vector3(Random.Range(-1f, 1f) * Entity.PatrolRange, 0, Random.Range(-1f, 1f) * Entity.PatrolRange);
                _context.Agent.SetDestination(_context.Position + direction);
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }

        public override void Exit(){
            _context.Agent.SetDestination(_context.Position);
        }

        public override void CheckTransitions(){
            if (_context.GetPredators(out var predators) > 0)
                _context.CurrentState = new RunAway(_context);
            else if (_context.GetFood(out var foods) > 0) 
                _context.CurrentState = new HuntMeal(_context, foods[0]);
            else if (_context.GetPreys(out var preys) > 0)
                _context.CurrentState=new HuntPrey(_context, preys[0]);
        }
    }
}
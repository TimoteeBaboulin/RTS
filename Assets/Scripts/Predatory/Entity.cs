using System;
using System.Collections.Generic;
using Predatory.States;
using UnityEngine;
using UnityEngine.AI;

namespace Predatory{
    public abstract class Entity : MonoBehaviour{
        public static readonly float PatrolRange = 10;

        [SerializeField] private GameObject _meatPrefab;
        
        //Utils
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent; 
        
        public Vector3 Position => transform.position;
        
        [Header("Ranges")]
        //Range used to determine who he can see
        [SerializeField] private float _sightRange;
        public float SightRange => _sightRange;
        //Range used to determine if the entity can deal damage to his target
        [SerializeField] private float _attackRange;
        public float AttackRange => _attackRange;
        
        private State _currentState;
        public State CurrentState{
            get => _currentState;
            set{
                _currentState?.Exit();
                _currentState = value;
                _currentState.Enter();
            }
        }
        
        [Header("Stats")]
        [SerializeField] private int _currentHealth;
        public int CurrentHealth{
            get => _currentHealth;
            set{
                if (value <= 0) Destroy(gameObject);
                else _currentHealth = value;
            }
        }

        [SerializeField] private int _maxStomach;
        [SerializeField] private int _currentStomach;
        public int CurrentStomach{
            get => _currentStomach;
            set{
                if (value > _maxStomach) _currentStomach = value;
                else if (value <= 0) _currentStomach = 0;
                else _currentStomach = value;
            }
        }

        public bool IsHungry => _currentStomach <= _maxStomach / 2;

        public abstract int GetPreys(out List<Entity> inRange);
        public abstract int GetPredators(out List<Entity> inRange);
        public abstract int GetFood(out List<Food> inRange);

        private void Awake(){
            CurrentState = new Idle(this);
            _agent = GetComponent<NavMeshAgent>();
            
            InvokeRepeating(nameof(HungerDrop), 1, 1);
        }

        private void Update(){
            CurrentState.Update();
            CurrentState.CheckTransitions();
        }

        private void OnDestroy(){
            Instantiate(_meatPrefab, Position, Quaternion.identity);
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, _sightRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, _attackRange);
        }

        private void HungerDrop(){
            if (_currentStomach == 0) CurrentHealth--;
            CurrentStomach--;
        }
    }
}
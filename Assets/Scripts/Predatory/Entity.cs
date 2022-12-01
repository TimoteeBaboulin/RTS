using System.Collections.Generic;
using Predatory.States;
using UnityEngine;
using UnityEngine.AI;

namespace Predatory{
    public abstract class Entity : MonoBehaviour{
        public static readonly float PatrolRange = 10;

        [SerializeField] private GameObject _meatPrefab;

        [Header("Ranges")]
        //Sight Range
        [SerializeField]
        private float _sightRange;

        //Attack Range
        [SerializeField] private float _attackRange;

        [Header("Stats")]
        //Speed
        [SerializeField]
        private float _speed;

        //Health
        [SerializeField] private int _currentHealth;

        //Stomach (Hunger)
        [SerializeField] private int _maxStomach;
        [SerializeField] private int _currentStomach;

        //Utils

        //Current State
        private State _currentState;

        private bool _isQuitting;
        public NavMeshAgent Agent{ get; private set; }

        public Vector3 Position => transform.position;
        public float SightRange => _sightRange;
        public float AttackRange => _attackRange;

        public State CurrentState{
            get => _currentState;
            set{
                _currentState?.Exit();
                _currentState = value;
                _currentState.Enter();
            }
        }

        public float Speed => _speed;

        public int CurrentHealth{
            get => _currentHealth;
            set{
                if (value <= 0) Destroy(gameObject);
                else _currentHealth = value;
            }
        }

        public int CurrentStomach{
            get => _currentStomach;
            set{
                if (value > _maxStomach) _currentStomach = _maxStomach;
                else if (value <= 0) _currentStomach = 0;
                else _currentStomach = value;
            }
        }

        public bool IsHungry => _currentStomach <= _maxStomach * 0.8f;
        public bool IsFullStomach => _currentStomach >= _maxStomach;
        public bool IsPredatorClose => CheckPredatorClose();

        //Unity methods
        private void Awake(){
            CurrentState = new Idle(this);
            Agent = GetComponent<NavMeshAgent>();

            InvokeRepeating(nameof(HungerDrop), 1, 1);
        }

        private void Update(){
            CurrentState.Update();
            CurrentState.CheckTransitions();
        }

        private void OnDestroy(){
            if (_isQuitting) return;

            Instantiate(_meatPrefab, Position, Quaternion.identity);
        }

        private void OnApplicationQuit(){
            _isQuitting = true;
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, _sightRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, _attackRange);
        }

        //Public methods
        public abstract int GetPreys(out List<Entity> inRange);
        public abstract int GetPredators(out List<Entity> inRange);
        public abstract bool CheckPredatorClose();
        public abstract int GetFood(out List<Food> inRange);
        public abstract int GetMate(out Entity mate);

        //Private methods
        private void HungerDrop(){
            if (_currentStomach == 0) CurrentHealth--;
            CurrentStomach--;
        }
    }
}
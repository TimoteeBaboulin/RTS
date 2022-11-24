using Predatory.States;
using UnityEngine;
using UnityEngine.AI;

namespace Predatory{
    public abstract class Entity : MonoBehaviour{
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
        
        [SerializeField] private int _currentHealth;
        public int CurrentHealth{
            get => _currentHealth;
            set{
                if (value <= 0) Destroy(gameObject);
                else _currentHealth = value;
            }
        }
        
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent; 
        
        public Vector3 Position => transform.position;
        
        private void Awake(){
            CurrentState = new Idle(this);
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update(){
            CurrentState.Update();
            CurrentState.CheckTransitions();
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Position, _sightRange);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, _attackRange);
        }
    }
}
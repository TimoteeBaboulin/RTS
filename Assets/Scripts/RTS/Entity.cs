using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace RTS{
    public class Entity : MonoBehaviour{
        private static readonly List<Entity> _entities = new();

        [SerializeField] [Range(1, 4)] private int _team = 1;

        [SerializeField] private float _attackRange;

        [SerializeField] private float _sightRange;
        private State _currentState;

        private int _health;

        public State CurrentState{
            get => _currentState;
            set{
                _currentState?.Quit();
                _currentState = value;
                _currentState.Start();
            }
        }

        public int Health{
            get => _health;
            set{
                if (value <= 0) Destroy(gameObject);
                _health = value > 10 ? 10 : value;
            }
        }

        public bool IsPlayer => _team == 1;
        public float AttackRange => _attackRange;
        public float SightRange => _sightRange;

        public NavMeshAgent Agent => GetComponent<NavMeshAgent>();

        private void Awake(){
            CurrentState = new Idle(this);
            _entities.Add(this);
        }

        private void Update(){
            CurrentState.Update();
            CurrentState.CheckTransitions();
        }

        private void OnDestroy(){
            _entities.Remove(this);
        }

        private void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SightRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        /// <summary>
        ///     GetEntities in a circle around baseEntity of range range
        /// </summary>
        /// <param name="baseEntity"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static List<Entity> GetEntitiesInRange(Entity baseEntity, float range = 10){
            if (range <= 0) throw new ArgumentOutOfRangeException(nameof(range));
            return _entities
                .Where(obj =>
                    obj != baseEntity && Vector3.Distance(baseEntity.transform.position, obj.transform.position) <= range)
                .OrderBy(obj => Vector3.Distance(baseEntity.transform.position, obj.transform.position)).ToList();
        }

        public static List<Entity> GetEntitiesInRange(Entity baseEntity){
            return GetEntitiesInRange(baseEntity, baseEntity.SightRange);
        }
    }
}
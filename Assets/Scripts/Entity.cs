using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour{
    private State _currentState;
    public State CurrentState{
        get => _currentState;
        set{
            _currentState?.Quit();
            _currentState = value;
            _currentState.Start();
        }
    }

    private int _health;
    public int Health{
        get => _health;
        set{
            if (value <= 0) Destroy(gameObject);
            _health = value > 10 ? 10 : value;
        }
    }

    [SerializeField] private float _attackRange;
    public float AttackRange => _attackRange;
    
    public NavMeshAgent Agent => GetComponent<NavMeshAgent>();

    private static readonly List<Entity> _entities = new();

    /// <summary>
    /// GetEntities in a circle around baseEntity of range range
    /// </summary>
    /// <param name="baseEntity"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static List<Entity> GetEntitiesInRange(Entity baseEntity, float range = 10){
        return _entities
            .Where(obj =>
                obj != baseEntity && Vector3.Distance(baseEntity.transform.position, obj.transform.position) <= range)
            .OrderBy(obj => Vector3.Distance(baseEntity.transform.position, obj.transform.position)).ToList();
    }

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
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
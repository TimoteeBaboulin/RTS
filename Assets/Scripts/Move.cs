using UnityEngine;
using UnityEngine.AI;

public class Move : State{
    private Vector3 _goal;

    public Move(Entity context) : base(context){ }
    
    public override void Start(){
        Debug.Log("Move: Start");
        
        NavMeshHit goal;
        NavMesh.SamplePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), out goal, 20, NavMesh.AllAreas);
        _goal = goal.position;
        _context.Agent.SetDestination(_goal);
    }

    public override void Update(){
        Debug.Log("Move: Update");
    }

    public override void Quit(){
        Debug.Log("Move: Quit");
    }

    public override void CheckTransitions(){
        if (Vector3.Distance(_context.transform.position, _goal) <= 0.5f)
            _context.CurrentState = new Idle(_context);
        else if (Input.GetMouseButtonDown(0))
            _context.CurrentState = new Idle(_context);
    }
}
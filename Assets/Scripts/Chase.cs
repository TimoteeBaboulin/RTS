using UnityEngine;

public class Chase : State{
    private Entity _goal;

    public Chase(Entity context, Entity goal) : base(context){
        _goal = goal;
    }
    
    public override void Start(){
        Debug.Log("Chase: Start");
    }

    public override void Update(){
        Debug.Log("Chase: Update");

        _context.Agent.SetDestination(_goal.transform.position);
    }

    public override void Quit(){
        Debug.Log("Chase: Quit");
    }

    public override void CheckTransitions(){
        if (Vector3.Distance(_goal.transform.position, _context.transform.position) <= _context.AttackRange)
            _context.CurrentState = new Attack(_context);
    }
}
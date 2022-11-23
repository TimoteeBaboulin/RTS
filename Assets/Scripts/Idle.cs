using UnityEngine;

public class Idle : State{
    //Base constructor
    public Idle(Entity context) : base(context){ }
    
    public override void Start(){
        Debug.Log("Idle: Start");
    }

    public override void Update(){
        Debug.Log("Idle: Update");
    }

    public override void Quit(){
        Debug.Log("Idle: Quit");
    }

    public override void CheckTransitions(){
        if (Input.GetMouseButtonDown(0))
            _context.CurrentState = new Move(_context);
        else if (Entity.GetEntitiesInRange(_context).Count > 0)
            _context.CurrentState = new Attack(_context);
        
    }
}
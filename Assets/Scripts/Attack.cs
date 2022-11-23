using UnityEngine;

public class Attack : State{
    private float _timer;
    public Attack(Entity context) : base(context){ }
    public override void Start(){
        Debug.Log("Attack: Start");
    }

    public override void Update(){
        Debug.Log("Attack: Update");

        if (_timer >= 1){
            _timer = 0;
            Entity.GetEntitiesInRange(_context)[0].Health -= 3;
        }
        
        _timer += Time.deltaTime;
    }

    public override void Quit(){
        Debug.Log("Attack: Quit");
    }

    public override void CheckTransitions(){
        if (Entity.GetEntitiesInRange(_context).Count == 0)
            _context.CurrentState = new Idle(_context);
        else if (Input.GetMouseButtonDown(0))
            _context.CurrentState = new Move(_context);
    }
}
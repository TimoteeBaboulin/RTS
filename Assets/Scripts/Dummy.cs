using System;

public class Dummy : Entity{
    private void Start(){
        CurrentState = new DeadIdle(this);
    }
}
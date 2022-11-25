using UnityEngine;

public abstract class Food : MonoBehaviour{
    public int Nourishment;
    public float EatTime;

    public Vector3 Position => transform.position;
}
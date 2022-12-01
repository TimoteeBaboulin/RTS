using UnityEngine;

namespace Predatory{
    public abstract class Food : MonoBehaviour{
        public int Nourishment;
        public float EatTime;

        public Vector3 Position => transform.position;
    }
}
using System.Collections.Generic;
using System.Linq;
using Predatory;
using UnityEngine;

public abstract class Food : MonoBehaviour{
    public int Nourishment;

    public Vector3 Position => transform.position;
}

public class Meat : Food{
    private static readonly List<Food> _meats = new();

    public static List<Food> GetFoodInRange(Entity entity){
        return _meats.Where(obj => Vector3.Distance(obj.Position, entity.Position) <= entity.SightRange)
            .OrderBy(obj => Vector3.Distance(obj.Position, entity.Position)).ToList();
    }

    private void OnEnable(){
        _meats.Add(this);
    }

    private void OnDisable(){
        _meats.Add(this);
    }
}

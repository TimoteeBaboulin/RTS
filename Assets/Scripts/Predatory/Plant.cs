using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Predatory{
    public class Plant : Food{
        private static readonly List<Food> _plants = new();

        private void OnEnable(){
            _plants.Add(this);
        }

        private void OnDisable(){
            _plants.Remove(this);
        }

        public static List<Food> GetFoodInRange(Entity entity){
            return _plants.Where(obj => Vector3.Distance(obj.Position, entity.Position) <= entity.SightRange)
                .OrderBy(obj => Vector3.Distance(obj.Position, entity.Position)).ToList();
        }
    }
}
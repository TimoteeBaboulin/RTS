using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Predatory{
    public class Meat : Food{
        private static readonly List<Food> _meats = new();

        private void OnEnable(){
            _meats.Add(this);
        }

        private void OnDisable(){
            _meats.Remove(this);
        }

        public static List<Food> GetFoodInRange(Entity entity){
            return _meats.Where(obj => Vector3.Distance(obj.Position, entity.Position) <= entity.SightRange)
                .OrderBy(obj => Vector3.Distance(obj.Position, entity.Position)).ToList();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Predatory.States;
using UnityEngine;

namespace Predatory{
    public class Prey : Entity{
        private static readonly List<Entity> Preys = new();

        /// <summary>
        /// Base method used to get Preys in SightRange of baseEntity
        /// Use baseEntity.GetPreys(out var inRange) for polymorphic uses
        /// </summary>
        /// <param name="baseEntity"></param>
        public static List<Entity> GetPreysInRange(Entity baseEntity){
            return Preys
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= baseEntity.SightRange && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
        }
        public static List<Entity> GetPreysInRange(Entity baseEntity, float range){
            return Preys
                                   .Where(obj =>
                                       Vector3.Distance(baseEntity.Position, obj.Position) <= range && baseEntity != obj)
                                   .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
        }

        private void OnEnable(){
            Preys.Add(this);
            CurrentState = new Idle(this);
        }

        private void OnDisable(){
            Preys.Remove(this);
        }

        public override int GetPreys(out List<Entity> inRange){
            inRange = new List<Entity>();
            return inRange.Count;
        }

        public override int GetPredators(out List<Entity> inRange){
            inRange = Predator.GetPredatorsInRange(this);
            return inRange.Count;
        }

        public override int GetFood(out List<Food> inRange){
            inRange = Plant.GetFoodInRange(this);
            return inRange.Count;
        }
    }
}

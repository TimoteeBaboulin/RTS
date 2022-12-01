using System.Collections.Generic;
using System.Linq;
using Predatory.States;
using UnityEngine;

namespace Predatory{
    public class Predator : Entity{
        private static readonly List<Entity> Predators = new();

        //Unity methods
        private void Start(){
            CurrentState = new Idle(this);
        }

        private void OnEnable(){
            Predators.Add(this);
        }

        private void OnDisable(){
            Predators.Remove(this);
        }

        //Static methods
        /// <summary>
        ///     Base method used to get Predators in SightRange of baseEntity
        /// </summary>
        /// <param name="baseEntity"></param>
        public static List<Entity> GetPredatorsInRange(Entity baseEntity){
            return Predators
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= baseEntity.SightRange && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
        }

        /// <summary>
        ///     Base method used to get Predators in SightRange of baseEntity
        /// </summary>
        /// <param name="baseEntity"></param>
        /// <param name="inRange">Already existing List of Entity to get the list</param>
        /// <returns> Return inRange.Count</returns>
        /// >
        public static int GetPredatorsInRange(Entity baseEntity, out List<Entity> inRange){
            inRange = Predators
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= baseEntity.SightRange && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();

            return inRange.Count;
        }

        public static int GetPredatorsInRange(Entity baseEntity, out List<Entity> inRange, float range){
            inRange = Predators
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= range && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();

            return inRange.Count;
        }

        //Public methods
        public override int GetPreys(out List<Entity> inRange){
            inRange = Prey.GetPreysInRange(this);
            return inRange.Count;
        }

        public override int GetPredators(out List<Entity> inRange){
            inRange = new List<Entity>();
            return inRange.Count;
        }

        public override bool CheckPredatorClose(){
            return false;
        }

        public override int GetFood(out List<Food> inRange){
            inRange = Meat.GetFoodInRange(this);
            return inRange.Count;
        }

        public override int GetMate(out Entity mate){
            var entities = GetPredatorsInRange(this);
            mate = entities.FirstOrDefault();
            return entities.Count;
        }
    }
}
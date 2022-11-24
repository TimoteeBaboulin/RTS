using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Predatory{
    public class Prey : Entity{
        public static readonly List<Entity> Preys = new();

        /// <summary>
        /// Base method used to get Preys in SightRange of baseEntity
        /// </summary>
        /// <param name="baseEntity"></param>
        public static List<Entity> GetPreysInRange(Entity baseEntity){
            return Preys
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= baseEntity.SightRange && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
        }

        /// <summary>
        /// Base method used to get Preys in SightRange of baseEntity
        /// </summary>
        /// <param name="baseEntity"></param>
        /// <param name="inRange">Already existing List of Entity to get the list</param>
        /// <returns> Return inRange.Count</returns>>
        public static int GetPreysInRange(Entity baseEntity, out List<Entity> inRange){
            inRange = Preys
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= baseEntity.SightRange && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
            return inRange.Count;
        }
        
        public static int GetPreysInRange(Entity baseEntity, out List<Entity> inRange, float range){
            inRange = Preys
                .Where(obj =>
                    Vector3.Distance(baseEntity.Position, obj.Position) <= range && baseEntity != obj)
                .OrderBy(obj => Vector3.Distance(obj.Position, baseEntity.Position)).ToList();
            return inRange.Count;
        }
        
        private void OnEnable(){
            Preys.Add(this);
        }

        private void OnDisable(){
            Preys.Remove(this);
        }
    }
}

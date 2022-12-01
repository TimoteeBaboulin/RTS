using UnityEngine;
using Random = UnityEngine.Random;

public class EntitySpawner : MonoBehaviour{
   [SerializeField] private GameObject _predatorPrefab;
   [SerializeField] private GameObject _preyPrefab;
   [SerializeField] private GameObject _plantPrefab;

   private void Start(){
      for (int i = 0; i < 10; i++){
         int x = Random.Range(-9, 10);
         int y = Random.Range(-9, 10);
         Instantiate(_predatorPrefab, new Vector3(x, 0, y), Quaternion.identity);
         
         x = Random.Range(-9, 10);
         y = Random.Range(-9, 10);
         Instantiate(_preyPrefab, new Vector3(x, 0, y), Quaternion.identity);
      }

      for (int i = 0; i < 100; i++){
         int x = Random.Range(-19, 20);
         int y = Random.Range(-19, 20);
         Instantiate(_plantPrefab, new Vector3(x, 0, y), Quaternion.identity);
      }
   }
}

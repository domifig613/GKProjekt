using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICreator : MonoBehaviour
{
    [SerializeField] private List<Route> routes;
    [SerializeField] private List<GameObject> carsPrefabs;
    [SerializeField] private GameObject carControllerPrefab;
    [SerializeField] private List<int> routesIndexCounts;
    [SerializeField] private GameObject controllerParent;
    [SerializeField] private GameObject carParent;
     
    private void Awake()
    {
        for (int i = 0; i < routes.Count; i++)
        {
            List<int> occupiedIndexes = new List<int>();
            int maxIndex = routes[i].PointsLenght;

            for (int j = 0; j < routesIndexCounts[i]; j++)
            {
                int index;

                do
                {
                    index = Random.Range(0, maxIndex);
                } 
                while (occupiedIndexes.Contains(index));

                occupiedIndexes.Add(index);

                GameObject car = Instantiate(carsPrefabs[Random.Range(0, carsPrefabs.Count)]);
                car.transform.SetParent(carParent.transform);

                GameObject carController = Instantiate(carControllerPrefab);
                carController.GetComponent<AIController>().Init(routes[i], car.GetComponent<AIInputCarController>(), index);
                carController.transform.SetParent(controllerParent.transform);
            }
        }
    }
}

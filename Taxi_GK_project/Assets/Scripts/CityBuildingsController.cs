using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuildingsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildingsTransform;
    [SerializeField] private Transform playerCarTransform;
    [SerializeField] private int refreshRate;

    private void Start()
    {
        StartCoroutine(ChangeBuildingsVisibleStateCor());
    }

    public void SetNewCarTransform(Transform transform)
    {
        playerCarTransform = transform;
    }

    private IEnumerator ChangeBuildingsVisibleStateCor()
    {
        while(true)
        {
            foreach (var building in buildingsTransform)
            {
                if(building.transform.position.z > playerCarTransform.position.z)
                {
                    building.SetActive(false);
                }
                else
                {
                    building.SetActive(true);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}

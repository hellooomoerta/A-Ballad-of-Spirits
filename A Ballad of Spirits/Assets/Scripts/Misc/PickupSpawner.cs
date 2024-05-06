using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab, healthGlobePrefab, staminaGlobePrefab;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 9);

        if (randomNum == 1)
        {
            int randomAmountOfGold = Random.Range(1, 4);

            for(int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }

        if (randomNum == 2)
        {
            Instantiate(healthGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            Instantiate(staminaGlobePrefab, transform.position, Quaternion.identity);
        }

        if (randomNum == 4)
        {
            Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
        }

        if (randomNum > 4)
        {
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public float platformLevelAmount = 20;
    public float platformsPerLevel = 10;
    public GameObject platform;
    public GameObject target;
    public int targetChance = 10;
    public GameObject player;
    private float currentHeightMilestone = 0;

    void Update()
    {
        if (player.transform.position.y > currentHeightMilestone * (platformLevelAmount + 1) * 3 - 1)
        {
            generatePlatforms(currentHeightMilestone * platformLevelAmount, (currentHeightMilestone + 1) * platformLevelAmount);
            currentHeightMilestone++;
        }
    }

    void generatePlatforms(float platformStartLevel, float platformEndLevel)
    {
        for (float i = platformStartLevel; i < platformEndLevel; i++)
        {
            for (int j = 0; j < platformsPerLevel; j++)
            {
                Vector3 platformPosition = new Vector3(Random.Range(-45, 45), (i + 1) * 6 + Random.Range(-1f, 1f), Random.Range(-45, 45));
                GameObject platformClone = Instantiate(platform);
                platformClone.transform.position += platformPosition;
                if (Random.Range(0, targetChance) == 0)
                {
                    Vector3 targetPosition = new Vector3(Random.Range(-45, 45), (i + 2) * 6 + Random.Range(-1f, 1f), Random.Range(-45, 45));
                    GameObject targetClone = Instantiate(target);
                    targetClone.transform.position += targetPosition;
                    targetClone.transform.rotation = Quaternion.Euler(0, 90 * Random.Range(0, 2), 0);
                }
            }
        }
    }
}

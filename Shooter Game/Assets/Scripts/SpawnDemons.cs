/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{

    public static SpawnDemons spawnDemons;

    [SerializeField]
    private GameObject demonRed;

    [SerializeField]
    private GameObject demonBlue;

    [SerializeField]
    private float demonInterval = 3.5f;

    [SerializeField]
    private Vector3[] spawnPositions;

    private int currentSpawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnDemons = this;

        spawnPositions = new Vector3[]
        {
            new Vector3(20.65914f, 0.18f, -2),
            new Vector3(20.65914f, 0.18f, 0f),
            new Vector3(20.65914f, 0.18f, 2f)
        };

        StartCoroutine(SpawnEnemy(demonInterval));
    }

    private IEnumerator SpawnEnemy(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            int randomIndex = Random.Range(0, spawnPositions.Length);
            GameObject demonPrefab = GetRandomDemonPrefab(); 

            Vector3 spawnPosition = spawnPositions[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);

            GameObject newEnemy = Instantiate(demonPrefab, spawnPosition, spawnRotation, this.gameObject.transform);

        }
    }

    private GameObject GetRandomDemonPrefab()
    {
        
        int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            return demonRed; 
        }
        else
        {
            return demonBlue; 
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
    public static SpawnDemons spawnDemons;

    [SerializeField]
    private GameObject demonRed;

    [SerializeField]
    private GameObject demonBlue;

    [SerializeField]
    private float demonInterval = 3.5f;

    [SerializeField]
    private Vector3[] spawnPositions;

    private int currentSpawnIndex = 0;
    private float timePassed = 0f;
    private float demonBlueProbability = 0f;
    private float demonBlueIncrement = 0.05f;
    private float maxDemonBlueProbability = 1f;
    private float timeToMaxProbability = 120f; // 2 minutos

    // Start is called before the first frame update
    void Start()
    {
        spawnDemons = this;

        spawnPositions = new Vector3[]
        {
            new Vector3(20.65914f, 0.18f, -2),//0
            new Vector3(20.65914f, 0.18f, 0f),//1
            new Vector3(20.65914f, 0.18f, 2f) //2
        };

        StartCoroutine(SpawnEnemy(demonInterval));
        StartCoroutine(UpdateDemonBlueProbability());
    }

    private IEnumerator SpawnEnemy(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            int randomIndex = Random.Range(0, spawnPositions.Length);
            GameObject demonPrefab = GetRandomDemonPrefab();

            Vector3 spawnPosition = spawnPositions[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);

            GameObject newEnemy = Instantiate(demonPrefab, spawnPosition, spawnRotation, this.gameObject.transform);
        }
    }

    public void UpdateSpawnPosition(string name)
    {
        if (name == "Chicken_0")
        {
            Vector3 positionToRemove = new Vector3(20.65914f, 0.18f, -2);

            List<Vector3> tempList = new List<Vector3>(spawnPositions);
            tempList.Remove(positionToRemove);
            spawnPositions = tempList.ToArray();
        }
        else
        if (name == "Chicken_1")
        {
            Vector3 positionToRemove = new Vector3(20.65914f, 0.18f, 0f);

            List<Vector3> tempList = new List<Vector3>(spawnPositions);
            tempList.Remove(positionToRemove);
            spawnPositions = tempList.ToArray();
        }else
        if (name == "Chicken_2")
        {
            Vector3 positionToRemove = new Vector3(20.65914f, 0.18f, 2f); 

            List<Vector3> tempList = new List<Vector3>(spawnPositions);
            tempList.Remove(positionToRemove);
            spawnPositions = tempList.ToArray();
        }
    }

    private GameObject GetRandomDemonPrefab()
    {
        float randomValue = Random.value;

        if (randomValue <= demonBlueProbability)
        {
            return demonBlue;
        }
        else
        {
            return demonRed;
        }
    }

    private IEnumerator UpdateDemonBlueProbability()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            timePassed += 1f;
            demonBlueProbability = Mathf.Min(timePassed / timeToMaxProbability, maxDemonBlueProbability);
            if (timePassed >= 60f)
            {
                demonInterval = 2f; 
            }

            if (timePassed >= timeToMaxProbability)
            {
                demonInterval = 1f; 
            }
        }
    }
}



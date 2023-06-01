using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
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

            int randomIndex = Random.Range(0, spawnPositions.Length); // Generar un índice aleatorio

            GameObject demonPrefab = GetRandomDemonPrefab(); // Obtener el prefab de demonio aleatorio

            Vector3 spawnPosition = spawnPositions[randomIndex];
            Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);

            GameObject newEnemy = Instantiate(demonPrefab, spawnPosition, spawnRotation, this.gameObject.transform);

            /*
            currentSpawnIndex++;

            if (currentSpawnIndex >= spawnPositions.Length)
            {
                currentSpawnIndex = 0;
            }*/
        }
    }

    private GameObject GetRandomDemonPrefab()
    {
        // Generar un número aleatorio para determinar qué demonio instanciar
        int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            return demonRed; // Retorna el prefab de demonRed si el número aleatorio es 0
        }
        else
        {
            return demonBlue; // Retorna el prefab de demonBlue si el número aleatorio es 1
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
    [SerializeField]
    private GameObject demonRed;

    [SerializeField]
    private float demonRedInterval = 3.5f;

    [SerializeField]
    private Vector3[] spawnPositions;

    private int currentSpawnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions = new Vector3[]
        {
            new Vector3(20.65914f, 0.18f, 0.13f),
            new Vector3(20.65914f, 0.18f, -1.625f),
            new Vector3(20.65914f, 0.18f, 2.15f)
        };

        StartCoroutine(SpawnEnemy(demonRedInterval, demonRed));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemy(float interval, GameObject demon)
    {
        yield return new WaitForSeconds(interval);

        int randomIndex = Random.Range(0, spawnPositions.Length); // Generar un índice aleatorio

        Vector3 spawnPosition = spawnPositions[randomIndex];

        Quaternion spawnRotation = Quaternion.Euler(0f, -90f, 0f);

        GameObject newEnemy = Instantiate(demon, spawnPosition, spawnRotation, this.gameObject.transform);

        StartCoroutine(SpawnEnemy(interval, newEnemy));
    }
}

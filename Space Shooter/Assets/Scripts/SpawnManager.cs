using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] powerUps;



    private bool isPlayerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutineEnemy());
        StartCoroutine(SpawnRoutinePowerup());
    }

    IEnumerator SpawnRoutineEnemy()
    {
        while(isPlayerAlive)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(-9.1f, 9.1f), 7, 0);
            Instantiate(enemyPrefab , enemyPosition , Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }    
    }

    IEnumerator SpawnRoutinePowerup()
    {
        while (isPlayerAlive)
        {
            Vector3 tripleShotPosition = new Vector3(Random.Range(-9.1f, 9.1f), 7, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerup], tripleShotPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(10,16));
        }
        
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        //infinite while loop
            //Instantiate enemy prefab
            //yield for 5 seconds

        while(_stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, position, Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;

            
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f), 7f, 0);

            int randomPowerup = Random.Range(0, 3);

            Instantiate(powerups[randomPowerup], position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(7f, 10f));                //7 10 arasýnda yap random range ile
        }

        
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
        Destroy(_enemyContainer.gameObject);
    }
}

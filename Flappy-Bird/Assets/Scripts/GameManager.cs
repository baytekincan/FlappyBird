using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawmInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPipes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnPipes()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-3f,3f),
                transform.position.z);
            Instantiate(pipePrefab, spawnPos, Quaternion.identity);
            spawmInterval = Random.Range(1f, 3f);
            yield return new WaitForSeconds(spawmInterval);
        }
            
    }
}

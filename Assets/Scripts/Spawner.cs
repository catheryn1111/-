using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] float radius = 50;
    
    private float timer = 0;
    
    void Start()
    {
        print("Корутин запущен");
        StartCoroutine(WaitAndSpawn());
        print("Корутин начал работу");
    }

    void Update()
    {

    }

    private IEnumerator WaitAndSpawn()
    {
        while (true) 
        {
            yield return new WaitForSecondsRealtime(0.5f);

            GameObject buf = Instantiate(prefab);
            float x = Random.Range(-radius, radius);
            float z = Random.Range(-radius, radius);
            buf.transform.position = transform.position + new Vector3(x, 1, z);

        }

    }
}

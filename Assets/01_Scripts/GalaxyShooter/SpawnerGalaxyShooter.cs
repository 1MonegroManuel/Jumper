using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerGalaxyShooter : MonoBehaviour
{

    public float timeBtwSpawn = 0.5f;
    float timer = 0;
    public Transform leftPoint;
    public Transform rightPoint;
    public List<GameObject> enemyPrefabs;
    public GameObject boss;
    public static SpawnerGalaxyShooter instance;
    int state = 1;
    int enemysForState = 4;
    int aux = 0;
    int numberStates = 3;
    bool bossspawn = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (state < numberStates)
        {
            SpawnEnemy();
        }
        else if(bossspawn)
        {
            Instantiate(boss, new Vector3(0, 4, 0), Quaternion.Euler(0, 0, 180));
            bossspawn = false;
        }
    }

    void SpawnEnemy()
    {
        if (timer < timeBtwSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (aux <= enemysForState)
            {
                timer = 0;
                float x = Random.Range(leftPoint.position.x, rightPoint.position.x);
                int enemy = Random.Range(0, enemyPrefabs.Count);
                Vector3 newPos = new Vector3(x, transform.position.y, 0);
                Instantiate(enemyPrefabs[enemy], newPos, Quaternion.Euler(0, 0, 180));
                aux++;
            }
            else
            {
                state ++;
                aux = 0;
            }
            
        }
    }
}

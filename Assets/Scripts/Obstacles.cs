using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    GameObject rockPrefab;
    [SerializeField]
    GameObject brainPrefab;
    [SerializeField]
    int maxNumObstacles = 3;
    [SerializeField]
    float obstacleSpeed = 3;
    [SerializeField]
    float maxSpawnDelayTime = 1;

    float currentSpawnDelayTime=0;

    [SerializeField]
    float maxBrainSpawnDelayTime = 20;
    float currentBrainSpawnDelayTime = 0;
    [SerializeField]
    int maxNumBrains= 1;

    List<GameObject> unusedObstacles = new List<GameObject>();
    List<GameObject> unusedBrains= new List<GameObject>();
    List<GameObject> activeObjects = new List<GameObject>();
    int activeRocks = 0;
    int activeBrains = 0;
    Bounds bounds;

    void Update()
    {
        bounds = Camera.main.OrthographicBounds();
        float speedup = GameManager.Instance.GetSpeedup();
        SpawnObstacles();
        MoveObjects(speedup);
        SpawnBrains();
    }

    private void MoveObjects(float speedup) {
        foreach (GameObject obj in activeObjects) {
            obj.transform.Translate(0, -obstacleSpeed*speedup*Time.deltaTime, 0);
            if(obj.transform.position.y<-bounds.extents.y) {
                // remove obstacle
                obj.SetActive(false);
            }
        }

        // cleanup list
        for (int i = activeObjects.Count-1; i>=0; i--) {
            if(!activeObjects[i].activeSelf) {
                if (activeObjects[i].name.StartsWith("Rock")) {
                    unusedObstacles.Add(activeObjects[i]);
                    activeRocks--;
                }
                if (activeObjects[i].name.StartsWith("Brain")) {
                    unusedBrains.Add(activeObjects[i]);
                    activeBrains--;
                }
            activeObjects.RemoveAt(i);
            }
        }
    }

    private void SpawnObstacles() {
        currentSpawnDelayTime-=Time.deltaTime;
        if(activeRocks < maxNumObstacles && currentSpawnDelayTime<0) {
            currentSpawnDelayTime = Random.Range(0.5f, maxSpawnDelayTime);

            if(unusedObstacles.Count==0) {
                unusedObstacles.Add(Instantiate<GameObject>(rockPrefab, transform));
            }

            GameObject newRock = unusedObstacles[0];
            float x = Random.Range(-bounds.extents.x, bounds.extents.x);
            newRock.transform.position = new Vector3(x, bounds.extents.y, 0);
            newRock.SetActive(true);
            activeObjects.Add(newRock);
            activeRocks++;
            unusedObstacles.RemoveAt(0);
        }
    }
    private void SpawnBrains() {
        currentBrainSpawnDelayTime -= Time.deltaTime;
        if (activeBrains< maxNumBrains && currentBrainSpawnDelayTime < 0) {
            currentBrainSpawnDelayTime = Random.Range(0.5f, maxBrainSpawnDelayTime);

            if (unusedBrains.Count == 0) {
                unusedBrains.Add(Instantiate<GameObject>(brainPrefab, transform));
            }

            GameObject newBrain = unusedBrains[0];
            float x = Random.Range(-bounds.extents.x, bounds.extents.x);
            newBrain.transform.position = new Vector3(x, bounds.extents.y, 0);
            newBrain.SetActive(true);
            activeObjects.Add(newBrain);
            activeBrains++;
            unusedBrains.RemoveAt(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstacle;
    public float respawnTime = .5f;
    private Vector3 screenBounds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(satWave());
    }
    private void SpawnObstacle()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Screen.height, Camera.main.transform.position.z));
        int obs = Random.Range(0, obstacle.Length);
        int yPosRange = Random.Range(0, 2);
        float yPos = 3;
        switch(yPosRange)
        {
            case 0:
                yPos = 2.5f;
                if (obs == 1)
                    yPos += 1.5f;
                GameObject a = Instantiate(obstacle[obs], new Vector3(screenBounds.x + 60, yPos, 0), Quaternion.identity) as GameObject;
                break;
            case 1:
                yPos = -2.5f;
                if (obs == 1)
                    yPos -= 1.5f;
                GameObject b = Instantiate(obstacle[obs], new Vector3(screenBounds.x + 60, yPos, 0), Quaternion.identity) as GameObject;
                b.transform.Rotate(0,0,180);
                break;
        }

    }
    IEnumerator satWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnObstacle();
        }
    }

}

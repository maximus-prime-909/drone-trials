using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlanet : MonoBehaviour
{
    [SerializeField]
    GameObject[] planet;
    public float respawnTime = 5f;
    private Vector3 screenBounds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(satWave());
    }
    private void SpawnPlanet()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Screen.height, Camera.main.transform.position.z));
        int obs = Random.Range(0, planet.Length);
        float yPos = Random.Range(-5, 5);
        GameObject a = Instantiate(planet[obs], new Vector3(screenBounds.x + 60, yPos, 2), Quaternion.identity) as GameObject;

    }
    IEnumerator satWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnPlanet();
        }
    }

}

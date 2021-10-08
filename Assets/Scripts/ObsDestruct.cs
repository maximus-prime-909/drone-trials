using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsDestruct : MonoBehaviour
{
    [SerializeField]
    string tagForSpawned = "Spawned";
    [SerializeField]
    float destroyAfter = 20f;
    float timer = 0f;
    void Start()
    {
        StartCoroutine(changeTag());
    }

    IEnumerator changeTag()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.tag = tagForSpawned;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyAfter)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagForSpawned))
        {
            Destroy(this.gameObject);
        }
    }
}

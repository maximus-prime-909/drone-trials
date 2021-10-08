using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticle : MonoBehaviour
{
    [SerializeField] GameObject winParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(winParticles, transform.position, Quaternion.identity);
        }
    }
}

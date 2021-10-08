using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDrone : MonoBehaviour
{
    public GameObject drone;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(drone.transform.position.x, transform.position.y, transform.position.z);
        
    }
}

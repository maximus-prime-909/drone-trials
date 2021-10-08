using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
   public void DroneSelected(int id)
    {
        PlayerPrefs.SetInt("drone_id", id);
    }
}
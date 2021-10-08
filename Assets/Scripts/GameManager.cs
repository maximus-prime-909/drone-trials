using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int selectedDroneId;
    [SerializeField]
    GameObject[] selectDrone;

    public Transform playerSpawn;

    public FollowDrone fd;

    private void Awake()
    {
        if (instance == null)
        {
            //First run, set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        selectedDroneId = PlayerPrefs.GetInt("drone_id");
        Debug.Log("Drone ID" + selectedDroneId);
    }
    void Start()
    {
        Time.timeScale = 1.0f;

        GameObject go = new GameObject();
        fd = FindObjectOfType<FollowDrone>();

        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        Debug.Log(playerSpawn.position);

        switch(selectedDroneId)
        {
            case 1:
                go= Instantiate(selectDrone[0], playerSpawn.position, Quaternion.Euler(0, 25, 0));
                break;

            case 2:
                go = Instantiate(selectDrone[1], playerSpawn.position, Quaternion.Euler(0, 0, 0));
                break;

            case 3:
               go = Instantiate(selectDrone[2], playerSpawn.position, Quaternion.Euler(-10, 0, 0));
                break;
        }

        fd.drone = go;

    }



}

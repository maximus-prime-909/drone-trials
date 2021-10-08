using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DisallowMultipleComponents] //Allows only once per game object
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    //todo remove from inspector later
    [Range(0,1)][SerializeField] float movementFactor;  //0 for not moved, 1 for fully moved

    Vector3 startingPos;
   
    void Start()
    {
        startingPos = transform.position;
    }

    
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //grows constantly from 0, i.e if the game runs for 10sec, then the object will move 5 times and so on

        const float tau = Mathf.PI * 2f; // Value of tau i.e 2*PI
        float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to 1.

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector ;
        transform.position = startingPos + offset;
        
    }
}

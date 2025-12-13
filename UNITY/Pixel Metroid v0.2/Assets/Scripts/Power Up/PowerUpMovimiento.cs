using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovimiento : MonoBehaviour
{
    public int speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.back, speed * Time.deltaTime);
    }
}

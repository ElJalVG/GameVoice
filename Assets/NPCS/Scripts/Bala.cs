using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidad*Time.deltaTime,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NpcDos")
        {
            Destroy(gameObject);
        }
    }
}

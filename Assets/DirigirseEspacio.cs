using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DirigirseEspacio : MonoBehaviour
{

    public Transform Lugar;
    public bool Verificar = false;
    public GameObject Character;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = Lugar.position;
    }

    void Update()
    {
         if(!Verificar)
        {
        agent.destination = Lugar.position;
        }
    }
   
    private void OnCollisionEnter(Collision other) 
    {
        Collider Cubo = GetComponent<Collider>();
        /*if(Cubo.CompareTag("Lugar"))
        {
            Verificar = true;

        }*/
    }
}

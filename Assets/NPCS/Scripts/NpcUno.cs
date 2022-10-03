using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = System.Random;
using  UnityEngine.UI;

public class NpcUno : MonoBehaviour
{
    public int vidaNpcUno=10;
    public NpcDos npcDos;
    public GameObject NpcUnoObj;
    private float rango;
    private int contadorAtaque = 0;
    private bool ataquedoble = false;

    public Text VidaNpcUnoText;

    public Text NoAtaqueText;

    public GameObject noataqueObj;
    public GameObject PrefabBala;
    public Transform puntoDisparo;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(vidaNpcUno<=0)
        {
            Destroy(NpcUnoObj);
        }
        VidaNpcUnoText.text=vidaNpcUno.ToString();
    }
    public void QuitarVidaNpcDos()
    {
        rango = UnityEngine.Random.Range(1f, 2.5f);
        Debug.Log((rango));
        if (rango <= 2f)
        {
            if (contadorAtaque <=2)
            {
                Instantiate(PrefabBala,puntoDisparo.transform.position,puntoDisparo.transform.rotation);
                npcDos.vidaNpcDos -= 1;
                contadorAtaque += 1;
                Debug.Log(("Ataque NpcUno: " + contadorAtaque));
            }
            if (contadorAtaque == 3)
            {
                npcDos.vidaNpcDos -= 1;
                contadorAtaque = 0;
            }
        }
        if (rango >= 2f)
        {
            contadorAtaque = 0;
            Debug.Log(("No se puede atacar a NpcDos"));
            NoAtaqueText.text = "NO PUEDES ATACAR PASA EL TURNO";
            noataqueObj.SetActive(true);
            StartCoroutine((TextDesactivar()));

        }
    }

    IEnumerator TextDesactivar()
    {
        yield return new WaitForSeconds(2);
        noataqueObj.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class NpcDos : MonoBehaviour
{
    public int vidaNpcDos=10;
    public NpcUno npcUno;
    public GameObject NpcDosObj;
    private float rango;
    private int contadorAtaque = 0;
    private bool ataquedoble = false;
    public Text VidaNpcDosText;
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
        if(vidaNpcDos<=0)
        {
            Destroy(NpcDosObj);
        }
        VidaNpcDosText.text=vidaNpcDos.ToString();
    }
    public void QuitarVidaNpcUno()
    {
        rango = UnityEngine.Random.Range(1f, 2.5f);
        Debug.Log((rango));
        if (rango <= 2f)
        {
            if (contadorAtaque <=2)
            {
                Instantiate(PrefabBala,puntoDisparo.transform.position,puntoDisparo.transform.rotation);
                npcUno.vidaNpcUno -= 1;
                contadorAtaque += 1;
                Debug.Log(("Ataque NpcDos: " + contadorAtaque));
                
            }
            if (contadorAtaque == 3)
            {
                npcUno.vidaNpcUno -= 1;
                contadorAtaque = 0;
            }
        }
        if (rango >= 2f)
        {
            contadorAtaque = 0;
            Debug.Log(("NO PUEDES ATACAR PASA EL TURNO"));
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

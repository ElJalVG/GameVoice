using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayesClassifierExample2 : MonoBehaviour {

	public enum BCLabel
	{
		Rojo,
		Celeste,
		Amarillo
	}

	public int numAttributes;
	[HideInInspector] public int numExamplesRojo;
	[HideInInspector] public int numExamplesCeleste;
	[HideInInspector] public int numExamplesAmarillo;

	[HideInInspector] public List<bool> attrCountRojo;
	[HideInInspector] public List<bool> attrCountCeleste;
	[HideInInspector] public List<bool> attrCountAmarillo;

	public bool[] attributesTrainRojo;
	public bool[] attributesTrainCeleste;
	public bool[] attributesTrainAmarillo;
	public bool[] attributesTest;

	[Header("Prefabs")]
	public GameObject prefabAmarillo;
	public GameObject prefabRojo;
	public GameObject prefabCeleste;

	[Header("Listas")]
	public List<GameObject> puntosCeleste;
	public List<GameObject> puntosRojo;
	public List<GameObject> puntosAmarillo;

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InitClassifier();
		}
	}

	public void InitClassifier()
	{
		attrCountAmarillo.Clear();
		attrCountCeleste.Clear();
		attrCountRojo.Clear();
		
		numExamplesAmarillo = 0;
		numExamplesCeleste = 0;
		numExamplesRojo = 0;
		
		updateClassifier (attributesTrainAmarillo, BCLabel.Amarillo);
		updateClassifier (attributesTrainCeleste, BCLabel.Celeste);
		updateClassifier (attributesTrainRojo, BCLabel.Rojo);
		
		Predict (attributesTest);
	}

	public void updateClassifier(bool[] attributes, BCLabel label)
	{
		if (label == BCLabel.Amarillo)
		{
			numExamplesAmarillo++;
			attrCountAmarillo.AddRange (attributes);
		}
		if (label == BCLabel.Celeste)
		{
			numExamplesCeleste++;
			attrCountCeleste.AddRange(attributes);
		}
		if(label == BCLabel.Rojo)
		{
			numExamplesRojo++;
			attrCountRojo.AddRange(attributes);
		}
	}

	public float Probabilities(ref bool[] attributes, bool[] counts, float m, float n)
	{
		float prior = m / (m + n);
		float p = 1f;
		for(int i=0; i<numAttributes; i++)
		{
			p /= m;
			if(attributes[i])
			{
				p *= counts [i].GetHashCode ();
			}
			else
			{
				p *= m - counts [i].GetHashCode ();
			}
		}
		return prior * p;
	}

	public bool Predict(bool[] attributes)
	{
		
		float celeste = Probabilities (ref attributes, attrCountCeleste.ToArray (), numExamplesCeleste, numExamplesRojo);
		float amarillo = Probabilities (ref attributes, attrCountAmarillo.ToArray (), numExamplesAmarillo, numExamplesRojo);
		float rojo = Probabilities (ref attributes, attrCountRojo.ToArray (), numExamplesRojo, numExamplesCeleste);
		
		Debug.Log("Amarillo: " + amarillo);
		Debug.Log("Azul: " + celeste);
		Debug.Log("Rojo: " + rojo);
		if(celeste>0)
		{
			GameObject punto=puntosCeleste[0];
			foreach(var puntoCeleste in puntosCeleste)
			{
				if(!puntoCeleste.GetComponent<Punto>().ocupado){
					punto=puntoCeleste;
					puntoCeleste.GetComponent<Punto>().ocupado = true;
					break;
				}
				
			}
			
			GameObject npc = Instantiate(prefabCeleste);
			npc.GetComponent<DirigirseEspacio>().Lugar = punto.transform;
			return true;
		}
		if(rojo > 0)
		{
			GameObject punto=puntosRojo[0];
			foreach(var puntoRojo in puntosRojo)
			{
				if(!puntoRojo.GetComponent<Punto>().ocupado){
					punto=puntoRojo;
					puntoRojo.GetComponent<Punto>().ocupado = true;
					break;
				}
				
			}
			
			GameObject npc = Instantiate(prefabRojo);
			npc.GetComponent<DirigirseEspacio>().Lugar = punto.transform;
			return true;
		}
		if(amarillo>0)
		{
			GameObject punto=puntosAmarillo[0];
			foreach(var puntoAmarillo in puntosAmarillo)
			{
				if(!puntoAmarillo.GetComponent<Punto>().ocupado){
					punto=puntoAmarillo;
					puntoAmarillo.GetComponent<Punto>().ocupado = true;
					break;
				}
				
			}
			
			GameObject npc = Instantiate(prefabAmarillo);
			npc.GetComponent<DirigirseEspacio>().Lugar = punto.transform;
			return true;
		}
		
		//Debug.Log ("NEGATIVE");
		return false;
	}

	public void example()
    {
		Debug.Log("dsfsdfssf");
    }
}

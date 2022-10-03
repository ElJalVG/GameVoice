using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayesClassifierExample : MonoBehaviour {

	public enum BCLabel
	{
		Amarillo,
		Rojo,
		Azul,
		Verde
	}

	public int numAttributes;
	[HideInInspector] public int numExamplesRojo;
	[HideInInspector] public int numExamplesAmarillo;
	[HideInInspector] public int numExamplesVerde;
	[HideInInspector] public int numExamplesAzul;

	[HideInInspector] public List<bool> attrCountRojo;
	[HideInInspector] public List<bool> attrCountAmarillo;
	[HideInInspector] public List<bool> attrCountVerde;
	[HideInInspector] public List<bool> attrCountAzul;

	public bool[] attributesTrainRojo;
	public bool[] attributesTrainAmarillo;
	public bool[] attributesTrainVerde;
	public bool[] attributesTrainAzul;
	public bool[] attributesTest;

	[Header("Prefabs")]
	public GameObject prefabAmarillo;
	public GameObject prefabRojo;
	public GameObject prefabAzul;
	public GameObject prefabVerde;
	
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
		attrCountAzul.Clear();
		attrCountRojo.Clear();
		attrCountVerde.Clear();
		
		numExamplesAmarillo = 0;
		numExamplesAzul = 0;
		numExamplesRojo = 0;
		numExamplesVerde = 0;
		
		updateClassifier (attributesTrainAmarillo, BCLabel.Amarillo);
		updateClassifier (attributesTrainAzul, BCLabel.Azul);
		updateClassifier (attributesTrainRojo, BCLabel.Rojo);
		updateClassifier (attributesTrainVerde, BCLabel.Verde);
		
		Predict (attributesTest);
	}

	public void updateClassifier(bool[] attributes, BCLabel label)
	{
		if (label == BCLabel.Amarillo)
		{
			numExamplesAmarillo++;
			attrCountAmarillo.AddRange (attributes);
		}
		else if (label == BCLabel.Azul)
		{
			numExamplesAzul++;
			attrCountAzul.AddRange(attributes);
		}
		else if(label == BCLabel.Rojo)
		{
			numExamplesRojo++;
			attrCountRojo.AddRange(attributes);
		}
		else
		{
			numExamplesVerde++;
			attrCountVerde.AddRange(attributes);
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
		
		float amarillo = Probabilities (ref attributes, attrCountAmarillo.ToArray (), numExamplesAmarillo, numExamplesAzul);
		float azul = Probabilities (ref attributes, attrCountAzul.ToArray (), numExamplesAzul, numExamplesAmarillo);
		float rojo = Probabilities (ref attributes, attrCountRojo.ToArray (), numExamplesRojo, numExamplesVerde);
		float verde = Probabilities (ref attributes, attrCountVerde.ToArray (), numExamplesVerde, numExamplesRojo);
		
		Debug.Log("Amarillo: " + amarillo);
		Debug.Log("Azul: " + azul);
		Debug.Log("Rojo: " + rojo);
		Debug.Log("Verde: " + amarillo);
		if(azul>0)
		{
			Instantiate(prefabAzul);
			return true;
		}
		else if(rojo > 0)
		{
			Instantiate(prefabRojo);
		}
		
		//Debug.Log ("NEGATIVE");
		return false;
	}
}

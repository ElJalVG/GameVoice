using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayesClassifier : MonoBehaviour {

	public enum BCLabel
	{
		POSITIVE,
		NEGATIVE
	}

	public int numAttributes;
	public int numExamplesPositive;
	public int numExamplesNegative;

	public List<bool> attrCountPositive;
	public List<bool> attrCountNegative;

	public bool[] attributesTrainNeg;
	public bool[] attributesTrainPos;
	public bool[] attributesTest;
	
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
		attrCountPositive = new List<bool> ();
		attrCountNegative = new List<bool> ();
		numExamplesNegative = 0;
		numExamplesPositive = 0;
		updateClassifier (attributesTrainPos, BCLabel.POSITIVE);
		updateClassifier (attributesTrainNeg, BCLabel.NEGATIVE);
		Predict (attributesTest);
	}

	public void updateClassifier(bool[] attributes, BCLabel label)
	{
		if (label == BCLabel.POSITIVE)
		{
			numExamplesPositive++;
			attrCountPositive.AddRange (attributes);
		}
		else
		{
			numExamplesNegative++;
			attrCountNegative.AddRange (attributes);
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
		float x = Probabilities (ref attributes, attrCountPositive.ToArray (), numExamplesPositive, numExamplesNegative);
		float y = Probabilities (ref attributes, attrCountNegative.ToArray (), numExamplesNegative, numExamplesPositive);
		if(x>=y)
		{
			Debug.Log ("POSITIVE");
			return true;
		}
		Debug.Log ("NEGATIVE");
		return false;
	}
}

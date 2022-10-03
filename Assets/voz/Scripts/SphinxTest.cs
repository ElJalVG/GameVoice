using UnityEngine;
using System.Collections;
using System;
using System.Text;
using UnityEngine.UI;
public class SphinxTest : MonoBehaviour {
	string str;
	[SerializeField]
	GameObject cat;
	[SerializeField]
	GameObject dog;
	[SerializeField]
	GameObject human;
	[SerializeField]
	GameObject horse;
	[SerializeField]
	GameObject mouse;
	[SerializeField]
	GameObject monkey;
	Text guitext;
	[SerializeField]
	Transform spawn;
	//[SerializeField] private BayesClassifierExample2 Example2;
	public NpcDos npcDos;
	public NpcUno npcUno;

	// Use this for initialization
	void Start () {
		guitext = GetComponent<Text> ();
		UnitySphinx.Init ();
		UnitySphinx.Run ();
	}

	void Update()
	{
		str = UnitySphinx.DequeueString ();
		if (UnitySphinx.GetSearchModel() == "kws")
		{
			print ("listening for keyword");
			if (str != "") {
				UnitySphinx.SetSearchModel (UnitySphinx.SearchModel.jsgf);
				guitext.text = "obedece";
				print (str);
			}
		}
		else if (UnitySphinx.GetSearchModel() == "jsgf")
		{
			print ("listening for order");
			if (str != "") 
			{
				guitext.text = str;
				char[] delimChars = { ' ' };
				string[] cmd = str.Split (delimChars);
				int numAnimals = interpretNum(cmd [0]);
				/*GameObject animal = interpretAnimal (cmd [1]);
				for (int i=0; i < numAnimals; i++) {
					Vector3 randPos = 
						new Vector3 (spawn.position.x + UnityEngine.Random.Range (-0.1f, 0.1f), 
							spawn.position.y + UnityEngine.Random.Range (-0.1f, 0.1f), 
							spawn.position.z + UnityEngine.Random.Range (-0.1f, 0.1f));
					Instantiate (animal, randPos, spawn.rotation);
				}
				//UnitySphinx.SetSearchModel (UnitySphinx.SearchModel.kws);*/
			}
		}
	}

	GameObject interpretAnimal(string animal)
	{
		GameObject a = cat;
		if (animal == "gatos")
			a = cat;
		else if (animal == "perros")
			a = dog;
		else if (animal == "caballos")
			a = horse;
		else if (animal == "humanos")
			a = human;
		else if (animal == "menos")
			a = monkey;
		else if (animal == "ratones")
			a = mouse;
		return a;
	}

	int interpretNum(string num)
	{
		int i = 0;
		if (num == "uno")
			npcUno.QuitarVidaNpcDos();
		if (num == "dos")
			npcDos.QuitarVidaNpcUno();
		return i;
	}
	private void Example()
    {
		Debug.Log("HaceCaso");
    }
}
using UnityEngine;
using System.Collections;

public class TextHandler : MonoBehaviour {

	//Public variables
	public string text;

	//Private variables
	AudioSource bleepSound;

	void Awake(){
		bleepSound = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}

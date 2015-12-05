using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextHandler : MonoBehaviour {

	public enum Pitches {
		Low = 1,
		High = 2
	}

	public enum States {
		Unloaded, //Array is not loaded with text yet
		Loaded, //Array is loaded with text, but we're not displaying yet
		Talking, //We are revealing character-by-character to display a new line of dialogue
		Displaying, //We are done going through characters for a line
		Complete //We're done displaying all lines in given array of text
	}

	//Public variables
	public States state; //What state are we in? (See enum of states)
	public Text textBox; //The Textbox we want to use to display dialogue
	public string[] text; //The text to be displayed
	public float rate;  //The number of characters/second to be revealed

	//Private variables
	int stringIndex; //Index of string in array we are displaying
	int characterIndex; //Index of character to reveal
	AudioSource bleepSound; //The base "bleep" sound for revealing a character
	float timer = 0; //Timer for going character-by-character

	void Awake(){
		bleepSound = GetComponent<AudioSource>();
		//state = States.Unloaded;
		state = States.Loaded;
		bleepSound.pitch = (float)Pitches.Low;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
	}

	public void SetText(string[] newText){ //Used to update new text
		text = newText; //Set new text
		stringIndex = 0; //Reset indicies
		characterIndex = 0;
	}

	public void SetRate(float r){ //Used to set the rate of revealing characters
		rate = r;
	}

	public void DisplayNextLine(){

	}

	void UpdateCharacter(){ //Used to reveal another character
		if(timer >= rate){
			timer = 0;
			bleepSound.Play();
		}
	}

	
}

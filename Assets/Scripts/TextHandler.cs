using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextHandler : MonoBehaviour {

	public enum States {
		Unloaded, //Array is not loaded with text yet, or we have displayed all lines of text
		Loaded, //Array is loaded with text, but we're not displaying yet
		Revealing, //We are revealing character-by-character to display a new line of dialogue
		Displaying, //We are done going through characters for a line
	}

	//Public variables
	public States state; //What state are we in? (See enum of states)
	public Text textBox; //The Textbox we want to use to display dialogue
	public Voice voice; //The "voice" for this character
	public string[] text; //The text to be displayed

	//Private variables
	AudioSource source;
	int stringIndex; //Index of string in array we are displaying
	int charIndex; //Index of character to reveal
	float timer = 0; //Timer for going character-by-character

	void Awake(){
		source = GetComponent<AudioSource>();
		state = States.Unloaded;
	}

	// Use this for initialization
	void Start () {
		AudioClip sound = Resources.Load("bleep") as AudioClip;
		Debug.Log(sound);
		Voice v = new Voice(0.1f, 1, 3, sound);

		string [] examples = {"Hello world!", "Test text!"};

		SetVoice(v);
		SetText(examples);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		UpdateCharacter();

		 if (Input.GetMouseButtonUp(0)){
		 	AdvanceLine();
		 }
	}

	public void SetVoice(Voice v){
		voice = v;
		source.clip = voice.sound;
	}

	public void SetText(string[] newText){ //Used to update new text
		text = newText; //Set new text
		stringIndex = 0; //Reset indicies
		charIndex = 0;
		state = States.Loaded;
	}

	public void AdvanceLine(){ //Called to skip revealing, or initiate revealing of next line.
		switch(state){
			case States.Loaded:
				state = States.Revealing;
			break;
			case States.Revealing:
				state = States.Displaying;
				charIndex = text[stringIndex].Length;
				UpdateText();
			break;
			case States.Displaying:
				stringIndex ++;
				if(stringIndex > text.Length - 1){
					UpdateText("");
					state = States.Unloaded;
				} else {
					state = States.Revealing;
					charIndex = 0;
				}
			break;
			case States.Unloaded:
				Debug.Log("All given text has been displayed.");
			break;
		}
	}

	public void AdvanceCharacter(){ //Called to advance character index
		charIndex ++;
		if(charIndex >= text[stringIndex].Length){
			charIndex = text[stringIndex].Length;
			state = States.Displaying;
		}
	}

	public void UpdateText(){
		UpdateText(text[stringIndex].Substring(0, charIndex));
	}

	public void UpdateText(string s){
		textBox.text = s;
	}

	void UpdateCharacter(){ //Used to reveal another character
		if(state == States.Revealing && timer >= voice.rate){
			timer = 0;
			AdvanceCharacter();
			UpdateText();
			source.pitch = Random.Range(voice.low, voice.high);
			source.Play();
		}
	}

	
}

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
		source = GetComponent<AudioSource>(); //Get the AudioSource component we're going to use to play the sound
		state = States.Unloaded; //We currently don't have any text to display
	}

	// Use this for initialization
	void Start () {
		/* Example usage, comment out to see test!
		AudioClip sound = Resources.Load("bleep") as AudioClip;
		Voice v = new Voice(0.1f, 1, 3, sound);

		string [] examples = {"Hello world!", "Test text!"};

		SetVoice(v);
		SetText(examples);
		*/
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; //Update timer every frame
		UpdateCharacter(); //Attempt to update the character

		//Replace this, or call AdvanceLine() from another class
		if (Input.GetMouseButtonUp(0)){ //On mouse click,
			AdvanceLine(); //Advance/skip line
		}
	}

	public void SetVoice(Voice v){ //Set our voice
		voice = v;
		source.clip = voice.sound;
	}

	public void SetText(string[] newText){ //Used to update new text
		text = newText; //Set new text
		stringIndex = 0; //Reset indicies
		charIndex = 0;
		state = States.Loaded; //We've been given text now, so we're ready to talk
	}

	public void AdvanceLine(){ //Called to skip revealing, or initiate revealing of next line.
		switch(state){ //Depending on what state we're in...
			case States.Loaded: //If we're loaded, start revealing the next line
				state = States.Revealing;
			break;
			case States.Revealing: //If we're currently in the process of revealing a line,
				charIndex = text[stringIndex].Length; //Set the reveal index to the end
				UpdateText(); //Update the text, effectively skipping the reveal
				state = States.Displaying; //Now we're just displaying a line
			break;
			case States.Displaying: //If we're displaying a line
				stringIndex ++; //Lets go to the next line that we've been given
				if(stringIndex > text.Length - 1){ //If we're out of bounds (out of lines)
					UpdateText(""); //Update to have a blank textbox
					state = States.Unloaded; //We're out of text, so we're "unloaded now"
				} else { //Otherwise,
					charIndex = 0; //Reset the character index
					state = States.Revealing; //And start revealing the next line
				}
			break;
			case States.Unloaded: //Out of lines/given no lines
				Debug.Log("All given text has been displayed.");
			break;
		}
	}

	public void AdvanceCharacter(){ //Called to advance character index
		charIndex ++; //Increment index
		if(charIndex >= text[stringIndex].Length){ //If we're past the end of of this line
			charIndex = text[stringIndex].Length; //Then just set it to the end
			state = States.Displaying; //Now we're displaying the whole line
		}
	}

	public void UpdateText(){ //Called to diplay from beginning to end of current line, using charIndex
		UpdateText(text[stringIndex].Substring(0, charIndex));
	}

	public void UpdateText(string s){ //Set the textbox's text
		textBox.text = s;
	}

	void UpdateCharacter(){ //Used to reveal another character
		if(state == States.Revealing && timer >= voice.rate){ //If we're in the process of revealing, and enough time has past
			timer = 0; //Reset timer
			AdvanceCharacter(); //Advance to next character
			UpdateText(); //Update the text to reflect the change
			source.pitch = Random.Range(voice.low, voice.high); //Change the pitch to something random, given the voice's range
			source.Play(); //Play the sound
		}
	}

	
}

# RPG-Text-System
A system for displaying text in Unity like in old RPG games.

## Usage

1. Add TextHandler prefab to your scene.
	![Screenshot 1](Screenshots/s1.PNG?raw=true "Screenshot 1")
2. Or, create a new object and attach the TextHandler script, and a AudioSource in the inspector
	![Screenshot 2](Screenshots/s2.PNG?raw=true "Screenshot 2")
2. Link the "Text Box" field in the TextHandler script to a textbox in the scene  
	![Screenshot 3](Screenshots/s3.PNG?raw=true "Screenshot 3")
3. In another script/object, or internally within TextHandler, create a new Voice().
	Voice() takes 4 parameters for its constructor:
		1. float rate: How time, in seconds, between each character reveal
		2. float low: The pitch of the lowest possible note (Expressed as a pitch coefficient)
		3. float high: The pitch of the highest possible note
		4. AudioClip sound: A reference to a AudioClip to be played when revealing notes. (See Unity documentation)
4. Link the Voice to TextHandler using TextHandler.SetVoice(Voice v)
	**For example: GetComponent<TextHandler>().SetVoice(new Voice(0.1f, 1, 5, sound));**
5. Give TextHandler an array of strings to display using TextHandler.SetText(string[] newText)
6. Now, call TextHandler.AdvanceLine() to start advancing lines. (By default, this is set to OnMouseUp)

## Example


## License
CC0
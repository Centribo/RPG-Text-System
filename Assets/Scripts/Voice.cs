using UnityEngine;
using System.Collections;

public class Voice {

	//Public variables
	public AudioClip sound; //The base "bleep" sound for revealing a character
	public float rate;  //The time between character reveals, in seconds
	public float low; //The lowest pitch coefficient this voice can speak
	public float high; //The highest pitch coefficient this voice can speak

	public Voice(float rate, float low, float high, AudioClip sound){
		this.rate = rate;
		this.low = low;
		this.high = high;
		this.sound = sound;
	}

	public Voice(float rate, float low, float high){
		this.rate = rate;
		this.low = low;
		this.high = high;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

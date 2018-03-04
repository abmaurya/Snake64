//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

	public Text currentScoreText,highestScoreText;
	public GameManager gMan;

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
	}

	internal void setScoreText(){
		currentScoreText.text = "Score:" + gMan.GetScore ("current");
		highestScoreText.text = "Highest Score:" + gMan.GetScore ("highest");
	}
}

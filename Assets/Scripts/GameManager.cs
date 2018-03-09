//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	int currentScore;		//Current game score
	int highestScore;		//Hieghest score yet
	FileStream gameFile;
	public Animator pMenuAnim;
	public Button resume,restart;
	GameObject currSelection;
	bool pauseMenuOpened = false;
	public EventSystem eventSys;
	public Text resumeText;

	public Image buttonSelectNotifier;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
		currentScore = 0;
		HighScoreInit ();
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetKeyUp (KeyCode.Escape)) {
			if (!pauseMenuOpened) {
				pauseMenuOpened = true;
				showPauseMenu ();
			}
			else
				hidePauseMenu ();
		}
		if (pauseMenuOpened) {
			if (eventSys.currentSelectedGameObject != null) {
				buttonSelectNotifier.GetComponent<RectTransform> ().position = 
					eventSys.currentSelectedGameObject.GetComponent<RectTransform> ().position + new Vector3 (-140, 0, 0);
				currSelection = eventSys.currentSelectedGameObject;
			} else
				eventSys.SetSelectedGameObject (currSelection);
		}
	}

	void showPauseMenu ()
	{
//		pMenuAnim.enabled = true;
		pMenuAnim.Play ("PanelIn");

		if (pauseMenuOpened) {
			resumeText.color = Color.white;
			resume.Select ();
			currSelection = resume.gameObject;
		}

		Time.timeScale = 0;
	}

	public void hidePauseMenu ()
	{
		pMenuAnim.Play ("PanelOut");
		pauseMenuOpened = false;
		Time.timeScale = 1;
		eventSys.SetSelectedGameObject (null);
	}

	void LateUpdate () {
		if (highestScore < currentScore) {
			highestScore = currentScore;
		}
	}

	void HighScoreInit(){
		if (!File.Exists (Application.persistentDataPath + "/score.sav")) {
			gameFile = File.Open (Application.persistentDataPath + "/score.sav", FileMode.OpenOrCreate);
			gameFile.WriteByte (byte.Parse ("0"));
			gameFile.Close ();
			highestScore = 0;
		} else
			highestScore = getSaveGame ();
	}

	void saveGame(int val){
		gameFile = File.Open (Application.persistentDataPath + "/score.sav", FileMode.OpenOrCreate,FileAccess.Write );
		gameFile.WriteByte(byte.Parse(highestScore.ToString()));
		gameFile.Close ();
	}

	int getSaveGame(){
		gameFile = File.Open (Application.persistentDataPath + "/score.sav", FileMode.Open,FileAccess.Read);
		int scr = gameFile.ReadByte ();
		gameFile.Close ();
		return scr;
	}

	internal void SetScore(int val){
		currentScore += val;
	}

	internal string GetScore(string choice){
		if (choice == "current")
			return currentScore.ToString ();
		else
			return highestScore.ToString ();
	}

	internal void gameOver(string str){
		saveGame (highestScore);
		print ("Game Over dumbo!!!");
		Time.timeScale = 0;
		resume.interactable = false;
		restart.Select ();
		resumeText.color = Color.gray;
		showPauseMenu ();

		////Optional Part\\\\
		if (str == "Player") {
			print ("Who eats itself!!!");
		} else
			print ("And you thought Snakes eat brick");
	}

	public void reStartGame(){
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}

	public void quitGame(){
		Application.Quit ();
	}

	void OnLevelWasLoaded(){
		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Snake64")&& !(GetComponent<FoodManager> ().enabled))
			GetComponent<FoodManager> ().enabled = true;
	}
}

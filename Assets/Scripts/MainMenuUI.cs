using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	public GameManager gMan;
	public GameObject menuSelector;
	GameObject currSelectedMenu;
	public Button startB;

	public EventSystem eventSys;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		startB.Select ();
		currSelectedMenu = startB.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (eventSys.currentSelectedGameObject != null) {
			if (eventSys.currentSelectedGameObject == startB.gameObject)
				menuSelector.transform.position = new Vector3 (-2, 2.7f, 1);
			else
				menuSelector.transform.position = new Vector3 (-2, -0.8f, 1);
			currSelectedMenu = eventSys.currentSelectedGameObject;
		} else
			eventSys.SetSelectedGameObject (currSelectedMenu);
	}

	public void startGameButton(){
		gMan.reStartGame ();
	}
		
	public void quitGameButton(){
		gMan.quitGame ();
	}
}

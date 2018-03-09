//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{

	List<GameObject> snakeNodes;

	public GameObject snakeBody;
	Transform endNodepos;
	Vector3 direction,pDire;
	string prevDirection;
	int nodeWithHead;
	public GameManager gM;
	public FoodManager fM;
	public MainUI mUI;

	// Use this for initialization
	void Start ()
	{
		snakeNodes = new List<GameObject> ();
		endNodepos = transform;
		pDire = direction = transform.forward;
		prevDirection = "forward";
		nodeWithHead = 0;
		InvokeRepeating ("MoveSnake", 0, FoodManager.level);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (prevDirection != "left" && Input.GetKeyUp (KeyCode.RightArrow)) {
				direction = transform.right;
				prevDirection = "right";
		} 
		else if (prevDirection != "right" && Input.GetKeyUp (KeyCode.LeftArrow)) {
				direction = -transform.right;
				prevDirection = "left";
		} 
		else if (prevDirection != "backward" && Input.GetKeyUp (KeyCode.UpArrow)) {
				direction = transform.forward;
				prevDirection = "forward";
		} 
		else if (prevDirection != "forward" && Input.GetKeyUp (KeyCode.DownArrow)) {
				direction = -transform.forward;
				prevDirection = "backward";
		}
	}

	void MoveSnake(){
		Vector3 tempPos;
		tempPos = transform.position;

		transform.position += 3*direction;

		if (snakeNodes.Count > 0) {
			if(nodeWithHead<=snakeNodes.Count)
				nodeWithHead++;

			for (int j = 0; j < snakeNodes.Count; j++) {
				Vector3 tempPoseBody = snakeNodes[j].transform.position;
				snakeNodes[j].transform.position = tempPos;
				tempPos = tempPoseBody;
			}
		}
		pDire = direction;
	}

	void addBody ()
	{
		snakeNodes.Add (Instantiate (snakeBody, endNodepos.position - 3f * pDire, Quaternion.identity));
		endNodepos = snakeNodes [snakeNodes.Count - 1].transform;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "food") {
			if (col.name == "Food")
				gM.SetScore (fM.normalFoodValue);
			else
				gM.SetScore (fM.superFoodValue);
			
			Destroy (col.gameObject);
			addBody ();
			FoodManager.foodEaten = true;
			mUI.setScoreText ();
			gainSpeed ();
		}
		else if (col.gameObject.tag == "Player") {
			gM.gameOver (col.gameObject.tag);
		}
			
	}

	void gainSpeed(){
		CancelInvoke ("MoveSnake");
		InvokeRepeating ("MoveSnake", 0, FoodManager.level);

	}

	void OnCollisionEnter (Collision col)
	{
		 if (col.gameObject.tag == "wall") {
			gM.gameOver (col.gameObject.tag);
		}
	}
}

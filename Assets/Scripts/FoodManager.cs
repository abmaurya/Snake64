//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {
	float xMin,xMax,zMin,zMax;		//The range for traversal/generation

	static internal bool foodEaten;		//Time to generate next food item if true
	int foodCounter;		//If reaches 5, generate SuperFood and reset it
	static internal float level = 0.2f;			//In snake Game, level defines the speed

	public GameObject gameArena;	//The area of traversal/generation
	public GameObject normalFood,superFood;							 //Sample for food generation
	internal int normalFoodValue, superFoodValue;					//To calculate scores

	// Use this for initialization
	void Start () {

		float tempx = (gameArena.transform.localScale.x / 2) - 3;
		float tempz = (gameArena.transform.localScale.z / 2) - 3;

		xMin = -tempx;
		xMax = tempx;
		zMin = -tempz;
		zMax = tempz;

		normalFoodValue = 1;
		superFoodValue = 3;
		foodEaten = false;
		foodCounter = 0;
		generateFood ();
	}
	
	// Update is called once per frame
	void Update () {
		if (foodEaten) {
//			print ("eaten");
			foodEaten = false;
			generateFood ();
		}
	}

	void generateFood(){
		if (foodCounter < 5) {
			foodCounter++;
//			print ("generate normal food");
			Instantiate (normalFood, new Vector3 (Random.Range (xMin, xMax), normalFood.transform.position.y, Random.Range (zMin, zMax)), Quaternion.identity).name = "Food";
		} else {
//			print ("generate Superfood");
			Instantiate (superFood, new Vector3 (Random.Range (xMin, xMax), superFood.transform.position.y, Random.Range (zMin, zMax)), Quaternion.identity).name = "SuperFood";
			foodCounter = 0;
			level-=0.02f;
		}
	}
}

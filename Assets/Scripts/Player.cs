using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
	
	public int pointsPerCoin = 5;
	
	//public Text coinText;
	
	private Animator animator;                  //Used to store a reference to the Player's animator component.
	private int coins;
	public int maxHealth = 100;
	public int currentHealth;

	private HealthBar healthBar;

	[HideInInspector] public bool canOpenDoor = false;

	[HideInInspector] public RoomGameManager roomGameManager;

	//Start overrides the Start function of MovingObject
	protected override void Start()
	{
		healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
		animator = GetComponent<Animator>();
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		coins = 0;

		base.Start();
	}



	//This function is called when the behaviour becomes disabled or inactive.
	private void OnDisable()
	{
		//When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
		roomGameManager.playerCoinPoints = coins;
	}


	private void Update()
	{
		//If it's not the player's turn, exit the function.
		if (!roomGameManager.playersTurn) return;

		int horizontal = 0;      //Used to store the horizontal move direction.
		int vertical = 0;        //Used to store the vertical move direction.


		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = (int)(Input.GetAxisRaw("Horizontal"));

		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = (int)(Input.GetAxisRaw("Vertical"));

		//Check if moving horizontally, if so set vertical to zero.
		if (horizontal != 0)
		{
			vertical = 0;
		}

		//Check if we have a non-zero value for horizontal or vertical
		if (horizontal != 0 || vertical != 0)
		{
			//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
			//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
			AttemptMove<Door>(horizontal, vertical);
			AttemptMove<Enemy>(horizontal, vertical);
		}
		Vector3 characterScale = transform.localScale;
		if (Input.GetAxis("Horizontal") < 0)
        {
			characterScale.x = -3.5f;

        }
		if (Input.GetAxis("Horizontal") > 0)
		{
			characterScale.x = 3.5f;

		}
		transform.localScale = characterScale;

	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D(Collider2D other)
	{
		//NOT BEING USED YET
		//Check if the tag of the trigger collided with is Exit.
		if (other.tag == "Exit")
		{
			//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
			Invoke("Restart", restartLevelDelay);

			//Disable the player object since level is over.
			enabled = false;
		}

		//NOT BEING USED YET
		//Check if the tag of the trigger collided with is Food.
		else if (other.tag == "ElementFire")
		{

			roomGameManager.elementsAvailable.Add("Fire");

			//Disable the food object the player collided with.
			other.gameObject.SetActive(false);

			//s'haurien de desactivar els altres
		}

		else if (other.tag == "Coin")
		{
			//Add pointsPerFood to the players current food total.
			coins += pointsPerCoin;
			currentHealth += pointsPerCoin;
			if (currentHealth <= 100)
			{
				currentHealth += pointsPerCoin;
				healthBar.SetHealth(currentHealth);
			}
			else
			{ 
				currentHealth = 100;
				healthBar.SetHealth(100);
			}

			//Update foodText to represent current total and notify player that they gained points
			//coinText.text = "+" + pointsPerCoin + " Coins: " + coins;

			//Disable the food object the player collided with.
			other.gameObject.SetActive(false);
		}

	}
	//AttemptMove overrides the AttemptMove function in the base class MovingObject
	//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		
		//Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
		base.AttemptMove<T>(xDir, yDir);

		//Hit allows us to reference the result of the Linecast done in Move.
		RaycastHit2D hit;

		if(Move(xDir, yDir, out hit))
        {

        }

		roomGameManager.playersTurn = false;
	}

	public void MovePhone(Vector3 moveDirection)
	{
		AttemptMove<Door>((int)moveDirection.x, (int)moveDirection.y);
		AttemptMove<Enemy>((int)moveDirection.x, (int)moveDirection.y);
	}

	//OnCantMove overrides the abstract function OnCantMove in MovingObject.
	//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
	protected override void OnCantMove<T>(T component)
	{
		if (component.tag == "Door")
        {
			Door openDoor = component as Door;
			if(canOpenDoor)
				openDoor.OpenDoor(this.gameObject);
		}
		if (component.tag == "Enemy")
		{
			Enemy enemyToAttack = component as Enemy;
			enemyToAttack.damagedByPlayer();
		}
		animator.SetTrigger("playerAct");
		//Set the attack trigger of the player's animation controller in order to play the player's attack animation.

	}


	//LoseFood is called when an enemy attacks the player.
	//It takes a parameter loss which specifies how many points to lose.
	public void LoseHp(int loss)
	{
		//Set the trigger for the player animator to transition to the playerHit animation.
		animator.SetTrigger("playerHit");

		//Subtract lost food points from the players total.
		currentHealth -= loss;

		//Update the food display with the new total.
		healthBar.SetHealth(currentHealth);

		//Check to see if game has ended.
		CheckIfGameOver();
	}


	//CheckIfGameOver checks if the player is out of food points and if so, ends the game.
	private void CheckIfGameOver()
	{
		//Check if food point total is less than or equal to zero.
		if (currentHealth <= 0)
		{
			animator.SetTrigger("playerDie");

#if UNITY_ANDROID
	saveGameInfoInAndroid();
#endif

			//Call the GameOver function of GameManager.
			roomGameManager.GameOver();
		}
	}

	private void saveGameInfoInAndroid()
    {
		AndroidJavaObject unityActivity = new AndroidJavaObject("com.unity3d.player.Backend");

		object[] parameters = new object[1];
		parameters[0] = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().SaveRooms();
		unityActivity.Call("saveGameInfo", parameters);
    }
}

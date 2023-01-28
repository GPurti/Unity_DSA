using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
	public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
	
	public int pointsPerCoin = 5;
	
	private Animator animator;                  //Used to store a reference to the Player's animator component.
	private int coins;
	public int maxHealth = 100;
	public int currentHealth;
	private HealthBar healthBar;

	private Shooting shooting;

	public static string playerId;
	public static ArrayList elementsAvailable;


	[HideInInspector] public bool canOpenDoor = false;

	[HideInInspector] public RoomGameManager roomGameManager;

	public AudioClip coinSound;
	public AudioClip changeRoomSound;
	public AudioClip gameOverSound;
	public AudioClip victorySound;
	public AudioClip swordSound;

	//Start overrides the Start function of MovingObject
	protected override void Start()
	{
		elementsAvailable = new ArrayList();
		//Player player = GameObject.FindGameObjectWithTag("")
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

		else if (other.tag == "Coin")
		{
			//Add pointsPerFood to the players current food total.
			coins += pointsPerCoin;
			currentHealth += pointsPerCoin;

			shooting = GetComponent<Shooting>();
			shooting.IncreaseAmo();

			SoundManager.instance.PlaySingle(coinSound);

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
			
			SoundManager.instance.PlaySingle(swordSound);
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

	public void SetPlayerId(string userId)
    {
		playerId = userId;
    }

	public void SetElement(string element)
	{
		elementsAvailable.Add(element);
		if (element == "Water")
		{
			SelectGadget water = GameObject.FindGameObjectWithTag("Water").GetComponent<SelectGadget>();
			water.destroyMissing();
			pointsPerCoin = 10;
		}
		else if (element == "Fire")
		{
			SelectGadget fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<SelectGadget>();
			fire.destroyMissing();
		}
		else if (element == "Earth")
		{
			SelectGadget earth = GameObject.FindGameObjectWithTag("Earth").GetComponent<SelectGadget>();
			earth.destroyMissing();
		}
		else if (element == "Cloud")
		{
			SelectGadget cloud = GameObject.FindGameObjectWithTag("Cloud").GetComponent<SelectGadget>();
			cloud.destroyMissing();
		}
	}

	private void saveGameInfoInAndroid()
    {
		AndroidJavaObject unityActivity = new AndroidJavaObject("edu.upc.dsa.andoroid_dsa.Backend");

		object[] parameters = new object[3];
		parameters[0] = playerId;
		parameters[1] = coins.ToString();
		parameters[2] = "false";
		unityActivity.Call("saveGameInfo", parameters);
    }

	public void changePlayerColor(Color color)
	{
		SpriteRenderer m_Sprite = this.GetComponent<SpriteRenderer>();
		m_Sprite.color = color;
	}

	private void ReloadGame()
	{
		foreach (string element in elementsAvailable)
		{

			if (element == "Water")
			{
				SelectGadget water = GameObject.FindGameObjectWithTag("Water").GetComponent<SelectGadget>();
				water.destroyMissing();
			}
			else if (element == "Fire")
			{
				SelectGadget fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<SelectGadget>();
				fire.destroyMissing();
			}
			else if (element == "Earth")
			{
				SelectGadget earth = GameObject.FindGameObjectWithTag("Earth").GetComponent<SelectGadget>();
				earth.destroyMissing();
				healthBar.SetMaxHealth(130);
			}
			else if (element == "Cloud")
			{
				SelectGadget cloud = GameObject.FindGameObjectWithTag("Cloud").GetComponent<SelectGadget>();
				cloud.destroyMissing();
			}
		}
	}
}

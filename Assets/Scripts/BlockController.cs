/*
 * BlockController.cs goes on every block that needs to change with player Emotion changes. 
 * In short, any block that has a behavior needs this script.
 * 
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

public class BlockController : MonoBehaviour {


	private GameObject goPlayerRef;	//Ref. to Player Object in scene.
	private PlayerControl pcPlayercontrol;	//Ref. to PlayerControl.cs Script
	private SpriteRenderer srSpriteRend; //Ref to this SpriteRenderer

	public Sprite[] sSpritelist; //Array of sprites
	public string[] stNames; //Array of names of sprites
	public int iType;	//Set before game start in the Inspector

	public GameObject goPoint1;
	public GameObject goPoint2;
	private bool bGoRight; //Bool telling node moving blocks which direction to go.
	private bool bApplyNausia;

	bool bActive;
	Vector2 v2OriPos; //Original Position Vector


    private float movementSpeed;

	// Use this for initialization
	void Start () 
	{
		srSpriteRend = this.gameObject.GetComponent<SpriteRenderer>();

		sSpritelist = Resources.LoadAll<Sprite>("Sprites");
		stNames = new string[sSpritelist.Length];

		for(int i = 0; i < stNames.Length; i++)
		{
			stNames[i] = sSpritelist[i].name;
		}

		v2OriPos = transform.position;

		movementSpeed = 1.0f + Random.Range (.01f, .1f);
		
		bGoRight = true;
		bActive = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Look for player Object
        if (!GameObject.Find("Level").GetComponent<LevelManager>().IsPlayerSpawned())
            return;
        else
        {
            // If we don't have a player object try to grab it
            if (goPlayerRef == null)
            {
                goPlayerRef = GameObject.Find("Player");
                pcPlayercontrol = goPlayerRef.GetComponent<PlayerControl>();
            }
            
        }

		if(renderer.isVisible)
		{
			bActive = true;
		}

		if(bActive)
		{
			//Check what emotion the Player is experiencing.
			switch(pcPlayercontrol.cEmote)
			{
			//Anger Bahaviors
			case 'A':
				switch(iType)
				{
				case 1: //Chase AI

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")]; //Change Sprite
            	tag = "null";

				//if(transform.position != goPlayerRef.transform.position)
				//{
					//transform.position = Vector2.MoveTowards ( transform.position, goPlayerRef.transform.position, movementSpeed * Time.deltaTime );
				//}
					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}

				break;
				case 2: //Spike Platform
					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "GGJ-repel")];
				
					if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}
           			 tag = "Deadly";

					this.gameObject.collider2D.enabled = true;

					break;
				case 3:	//Moving Saw
					//Handle Node Movement and direction Change
					//if(bGoRight)
					//{
						//Move towards the "right" node or node2 
						//if(Vector2.Distance (transform.position, goPoint2.transform.position) >= .01)
						//{
                    	//	transform.position = Vector2.MoveTowards(transform.position, goPoint2.transform.position, movementSpeed * Time.deltaTime);
						//}
						//else
						//{
							//Swap directions moving towards node1 instead
						//	bGoRight = false;
						//}
					//}
					//else
					//{
					//	if(Vector2.Distance (transform.position, goPoint1.transform.position) >= .01)
					//	{
					//		transform.position = Vector2.MoveTowards (transform.position, goPoint1.transform.position, movementSpeed * Time.deltaTime);
					//	}
					//	else
					//	{
					//		bGoRight = true;
					//	}
					//}

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];
					tag = "null";

	
					break;
				}
				break;
				//Sadness Behaviors
			case 'S':
				switch(this.iType)
				{
				case 1: //Suicide Block
					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];
					tag = "null";
					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}

					break;
				case 2: //Falling Block
					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "Sad_Falling")];
				
					transform.position = Vector2.Lerp (transform.position, new Vector2(transform.position.x,-100), .0005f); //Fall off the map

					this.gameObject.collider2D.enabled = true;

					break;
				case 3: //Slowing Block

				
					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

					tag = "null";
				
					//fDecel = 5.0f; //Deceleration value to apply to player.
					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}

				break;
			}
		break;
		//Happiness Behaviors
		case 'H':
			switch(iType)
			{
			case 1: //Tramp Block

				srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "tramp")];

				tag = "Tramp";

					if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}

				break;
			case 2: //Floating Block

				if(bGoRight)
				{
					if(Vector2.Distance (transform.position, goPoint2.transform.position) >= .01)
					{
						transform.position = Vector2.MoveTowards (transform.position, goPoint2.transform.position, movementSpeed * Time.deltaTime * 2);
					}
					else
					{
						bGoRight = false;
					}
				}
				else
				{
					if(Vector2.Distance (transform.position, goPoint1.transform.position) >= .01)
					{
						transform.position = Vector2.MoveTowards (transform.position, goPoint1.transform.position, movementSpeed * Time.deltaTime * 2);
					}
					else
					{
						bGoRight = true;
					}
				}

					this.gameObject.collider2D.enabled = true;
				srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "float")];
				tag = "Safe";
				break;
			case 3: //Accelerating
				srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "accelerator")];

				tag = "Accel";

					if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}

				break;
			}
			break;
		//Disgust Behaviors
		case 'D':
			switch(iType)
			{
			case 1: //Nausia Block

				srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}
					tag = "null";
				break;
			case 2: //Repel Block

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "repel")];


					if(Vector2.Distance (transform.position, goPlayerRef.transform.position) <= 1)
					{
						transform.position = (transform.position - goPlayerRef.transform.position).normalized * 1 + goPlayerRef.transform.position;

					}
					else if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards(transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}

					this.gameObject.collider2D.enabled = true;
				
					tag = "Safe";
					break;
				case 3:	//Cripple Block

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}
					tag = "null";

					break;
				}
				break;
			//Surprise Behaviors
			case 'U':
				switch(iType)
				{
				case 1: //Hidden Spikes

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					///	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}

					tag = "null";

				break;
			case 2: //Corporeal Block / Invisible

				srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "GGJ-corporeal")];

				this.gameObject.collider2D.enabled = false;

				if(Vector2.Distance (transform.position, v2OriPos) != 0)
				{
					transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
				}
					tag = "Safe";
				break;
			case 3: //Poison Knife Thrower

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

					/*if(Vector2.Distance (transform.position, goPlayerRef.transform.position) < 1.5 && bReloaded)
					{
						Vector2 v2Target = goPlayerRef.transform.position;

						GameObject goKnife = new GameObject("Knife");
						//goKnife.AddComponent ("CircleCollider2D");

						goKnife.transform.position = transform.position;

						goKnife.AddComponent ("SpriteRenderer");
						SpriteRenderer sr = goKnife.GetComponent<SpriteRenderer>();
						sr.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];

						goKnife.AddComponent ("KnifeCont");
						KnifeCont kcCont = goKnife.GetComponent<KnifeCont>();
						kcCont.v2TargetPos = v2Target;
						bReloaded = false;

						InvokeRepeating ("StartReload", 10, 10f);
					}*/
					//if(Vector2.Distance (transform.position, v2OriPos) != 0)
					//{
					//	transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					//}
					tag = "null";
					break;
				}
				break;
			//Fear Behaviors
			case 'F':
				switch(iType)
				{
				case 1: //Player Repel Block - Repels player

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "null-platform")];
                    //float dist = Vector2.Distance(transform.position, goPlayerRef.transform.position);
					tag = "null";
				//	if(Vector2.Distance (transform.position, v2OriPos) != 0)
				//	{
				//		transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
				//	}

               // if (dist < 2.0f)
               //{
                    //print("Push player back");
                    // Moving play back
                    
                  //  var dir = goPlayerRef.transform.position - transform.position;
                   // dir.Normalize();
                  //  goPlayerRef.rigidbody2D.AddForce(new Vector2((dir.x) * 150, 0));
                //}
					break;
				case 2: //Transparent block

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "GGJ-glass")];

					this.gameObject.collider2D.enabled = true;

					if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}
					tag = "null";
					break;
				case 3: //Illusionary Spikes

					srSpriteRend.sprite = sSpritelist[ArrayUtility.IndexOf<string>(stNames, "GGJ-Illusionary")];

					if(Vector2.Distance (transform.position, v2OriPos) != 0)
					{
						transform.position = Vector2.MoveTowards (transform.position, v2OriPos, movementSpeed * Time.deltaTime);
					}
					tag = "null";
					break;
				}
				break;
			}
		}
	}

}



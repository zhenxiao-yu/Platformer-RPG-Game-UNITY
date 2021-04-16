    using UnityEngine;
　　using System.Collections;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
　　public class Controller2D : MonoBehaviour, IController2D {
        // get UI of pause menu
        public GameObject pauseMenu;   
        // access GameManager 
        public GameManager gameManager;
        // number of enemy collisions
        public int hitsTaken = 0;
        // particle system
        public ParticleSystem dust;
　　	//Reference to the chracterController 
　　	CharacterController characterController;
　　	//For changing how fast the player fall( less gravity = jump higher + longer)
　　	public float gravity = 10;
　　	//For changing the speed of the player 
　　	public float walkSpeed = 5;
　　	//For changing how high the player could jump
　　	public float jumpHeight = 6;
　　	//How long the player is invulnerable for after taken damage
　　	float takenDamage = 0.2f;
        //Refrence to the bullet prefab
        public Rigidbody bulletPrefab;
        //referece to mine prefab
        public Rigidbody mine;
        //reference to player object
        public Transform player;
        //the direction the player is moving toward
　　	Vector3 moveDirection = Vector3.zero;
        // set a float for horizontal movement
　　	float horizontal = 0;
        //how fast the player attacks
　　	float attackRate = 0.5f;
        // how long in between attacks player can attack
　　	float coolDown;
        // boolean determins which way player looks
　　	bool lookRight = true;
        // boolean determines if player is customized
        bool custom = false;
        public int pieceCount = 6;
        // amount of EXP needed to level up initially
        private int requiredEXP = 100;
        //The player's shield
        public GameObject playerShield;
        // Decrements coin if shield is used
        int shieldCounter = 0;
        // boolean determines if game is paused
        public bool paused;
        // boolean determines if shield is used currently
        public bool shieldOn;
        // get turret of player
        public GameObject turret;
        //get jetpack of player
        public GameObject jetpack;
        //get coin magnet game object;
        public GameObject coinMagnet;
        private Vector3 localScale;
        private Animator anim;
        private Rigidbody rb;
        //player direction for animation
        private float dirX;
        // help button
        public Button help;
        
    
       

　　	
　　	void Start () {
        // get character controller and set all booleans to false except help button
　　		characterController = GetComponent<CharacterController>();
            custom = false;
            paused = false;
            pauseMenu.SetActive(false);
            shieldOn = false;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>(); 
            localScale = transform.localScale;
            help.gameObject.SetActive(true);
　　	}
　　	
　　	
　　	void Update () {
            //Animate Running
            dirX = Input.GetAxisRaw("Horizontal") * walkSpeed;
            
            if (!paused){
                help.gameObject.SetActive(true);
            } else {
                help.gameObject.SetActive(false);
            }

            if(Mathf.Abs(dirX) > 0 ){
            anim.SetBool("isRunning", true);
            }else{
            anim.SetBool("isRunning", false);
            }

            //Transforms the scaling of the character 
            Vector3 characterScale = transform.localScale;
　　		// set up horizontal player movement
　　		characterController.Move (moveDirection * Time.deltaTime);
　　		horizontal = Input.GetAxis("Horizontal");

　　		// fall if unsupported
　　		moveDirection.y -= gravity * Time.deltaTime;
            // stay static if no input
　　		if (horizontal == 0) {
　　			moveDirection.x = horizontal;		
　　		}
　　		// set up left & right movement
　　		if (horizontal > 0.01f) {
　　			lookRight = true;
　　			moveDirection.x = horizontal * walkSpeed;
　　		}
　　		
　　		if (horizontal < -0.01f) {
　　			lookRight = false;
　　			moveDirection.x = horizontal * walkSpeed;
　　		}

            //Creates dust when moving left 
            if(Input.GetAxis("Horizontal")<0){
                CreateDust();
            }

            //Creates dust when moving right 
            if(Input.GetAxis("Horizontal")>0){
               CreateDust();
            }

　　		// jump if player is touching ground
　　		if (characterController.isGrounded) {
　　			if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W)){
　　				moveDirection.y = jumpHeight;
                    anim.Play("JumpAnimation");
　　			} 
　　		} 
                          
            // allow player to fly if customized and in bounds
            if (custom){
                if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.W)){
　　				moveDirection.y = jumpHeight;
                    GameManager.playersWealth -= 2;
                    anim.Play("JumpAnimation");
　　			}
            }

　　		// BulletAttack() when F key is pressed
　　		if (Time.time >= coolDown) {
　　			if (Input.GetKeyDown (KeyCode.F)){
　　				BulletAttack ();	
　　			}
　　		}

            if (Time.time >= coolDown) {
　　			if (Input.GetKeyDown (KeyCode.E)){
　　				TripleAttack ();	
　　			}
　　		}

            // Custom() when C key is pressed and player is not already customized
            if (Input.GetKeyDown("c") && !custom){
                Custom();
                custom = true;
            }
            if (Input.GetKeyDown("p") && !paused){
                Pause();
            }
            if (Input.GetKey("r") && paused){
                Resume();
            }
            // UndoCustom() when U key is pressed and player is already customized
            if (Input.GetKeyDown("u") && custom){
                UndoCustom();
                custom = false;
            }
            // MineAttack() when M key is pressed
            if (Input.GetKeyDown("m") && characterController.isGrounded){
                if (GameManager.numOfMine > 0){
                MineAttack();
                }
            }
            // turn on player's shield in exchange for coins
            if (Input.GetKey(KeyCode.Tab)){
                if (GameManager.playersWealth >= 5 && !shieldOn){
                    shieldOn = true;
                    playerShield.SetActive(true); 
                    GameManager.playersWealth-= 5;
                }
            }
            // turn off player shield if tab key is not held
            else
            {
                shieldOn = false;
                playerShield.SetActive(false);
                
            }
        
            // handle level up with EXP
            if(GameManager.playersEXP >= requiredEXP){
                        GameManager.playersEXP = 0;
                        GameManager.maxHealth ++;
                        GameManager.playersLV ++;
                        walkSpeed += 0.5f;
                        attackRate -= 0.02f;
                        GameManager.playersHealth = GameManager.maxHealth;
                        requiredEXP += 500;
            }
            // decrease coins if shield is used
            ShieldDecreaseCoin();
            // activate turret if customized, else deactivate
            if (!custom){
                turret.SetActive(false);
                jetpack.SetActive(false);
            }

            if (custom){
                turret.SetActive(true);
                jetpack.SetActive(true);
            }
           
　　	}

        private void LateUpdate(){

            if (((lookRight) && (localScale.x < 0)) || ((!lookRight) && (localScale.x > 0))){
                localScale.x *= -1;
            }
            transform.localScale = localScale;
        }

        // pause game
        public void Pause(){
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        // resume game
        public void Resume(){
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
        // change scene (for pause menu button)
        public void ChangeScene(){
            string otherScene;
            Scene s = SceneManager.GetActiveScene();
            if (s.name == "2"){
                otherScene = "1";
            } else {
                otherScene = "2";
            }
            SceneManager.LoadScene(otherScene);
            Resume();
        }
        // decrement coin as shield is being used
        public void ShieldDecreaseCoin(){
            if (Input.GetKey(KeyCode.Tab)){
                shieldCounter++;
            }
            if (shieldCounter > 100){
                shieldCounter = 0;
                if (GameManager.playersWealth >= 3){
                    GameManager.playersWealth -= 3;
                } else if (GameManager.playersWealth >= 2){
                    GameManager.playersWealth -= 2;
                } else if (GameManager.playersWealth >= 1){
                    GameManager.playersWealth -= 1;
                }
            }
                
        }
        // shoot bullet with F key
　　	public void BulletAttack(){
　　		if (lookRight) {
　　			// shoot right if facing right
                
　　			Rigidbody bPrefab = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (Vector3.right * 500);
　　			coolDown = Time.time + attackRate;
　　				}
　　		else {
　　			// shoot left if facing left
                
　　			Rigidbody bPrefab = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (-Vector3.right * 500);
　　			coolDown = Time.time + attackRate;
　　		}
　　	}
        // triple shoot
        public void TripleAttack(){
            if (lookRight) {
　　			// shoot right if facing right
　　			Rigidbody bPrefab = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (new Vector3(1,0.25f,0) * 200);
                Rigidbody bPrefab2 = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab2.GetComponent<Rigidbody>().AddForce (Vector3.right * 200);
                Rigidbody bPrefab3 = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab3.GetComponent<Rigidbody>().AddForce (new Vector3(1,0.5f,0) * 200);
　　			coolDown = Time.time + attackRate;
　　				}
　　		else {
　　			// shoot left if facing left
　　			Rigidbody bPrefab = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab.GetComponent<Rigidbody>().AddForce (-new Vector3(1,-0.25f,0) * 200);
                Rigidbody bPrefab2 = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab2.GetComponent<Rigidbody>().AddForce (-Vector3.right * 200);
                Rigidbody bPrefab3 = Instantiate (bulletPrefab, transform.position, Quaternion.identity)as Rigidbody;
　　			bPrefab3.GetComponent<Rigidbody>().AddForce (-new Vector3(1,-0.5f,0) * 200);
　　			coolDown = Time.time + attackRate;
　　		}
            
        }


        // drops mine
        public void MineAttack(){
            // Instantiate mine where player drops it
            Vector3 mineDrop = transform.position;
            mineDrop -= new Vector3(0.7f, 0.5f, -1f);
            Instantiate (mine, mineDrop, Quaternion.identity);
            GameManager.numOfMine--;
            
        }

        
        void OnTriggerEnter(Collider other){
            //adds player health when player collides with health pickup
		    if (other.tag == "Health") {
                 if (GameManager.playersHealth < GameManager.maxHealth){
			        GameManager.playersHealth++;
                }
                GameManager.playersEXP += 50;
			    Destroy(other.gameObject);  
		    }
            // adds mine if picked up by player
             if (other.tag == "Mine Refill") {
                 if (GameManager.numOfMine < 15){
			        GameManager.numOfMine += 5;
                }
                GameManager.playersEXP += 50;
			    Destroy(other.gameObject);  
		    }

           //add player health when player collides with speed boost pickup
            if (other.tag == "Speed") {
			  StartCoroutine(increaseSpeed(5f));
                GameManager.playersEXP += 50;
			    Destroy(other.gameObject);
		    }

            //Enable coin magnet for 10 seconds
            if (other.tag == "Coin Magnet Pickup") {
			  StartCoroutine(attractCoin(10f));
                GameManager.playersEXP += 50;
			    Destroy(other.gameObject);
		    }

            //Kill player when player falls into lava
             if (other.tag == "Lava") {
                gameManager.SendMessage("PlayerDamaged",100,SendMessageOptions.DontRequireReceiver);
                gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
             }

            //push player upwards when player collides with trampoline gameobject
            if (other.tag == "Trampoline"){
                moveDirection.y = jumpHeight* 1.25f;
            }
            // increase wealth when player collides w coin
            if(other.tag == "Gold"){
                GameManager.playersWealth++;
                Destroy(other.gameObject);
            }
            // decrease health if player collides w enemy bullet
             if(other.tag == "Enemy Bullet"){
                gameManager.SendMessage("PlayerDamaged",1,SendMessageOptions.DontRequireReceiver);
                gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                Destroy(other.gameObject);
            }

            // decrease health if player collides w vertical patroller
             if(other.tag == "Flying Enemy"){
                gameManager.SendMessage("PlayerDamaged",1,SendMessageOptions.DontRequireReceiver);
                gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                GameManager.playersWealth += 10;
            }

            // slow down if hit by freeze bullet
            if(other.tag == "Freeze Bullet"){
                gameManager.controller2D.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
                StartCoroutine(decreaseSpeed(5f));
                Destroy(other.gameObject);
            }
            // increase wealth if diamond is collected
            if(other.tag == "Diamond"){
                GameManager.playersWealth += 10;
                Destroy(other.gameObject);
                GameManager.playersEXP += 5;
            }
	    }

      
	
　　	
　　	public IEnumerator TakenDamage(){
            anim.Play("HitAnimation");
            // flash object once damage is taken
　　		GetComponent<Renderer>().enabled = false;
　　		yield return new WaitForSeconds(takenDamage);
　　		GetComponent<Renderer>().enabled = true;
　　		yield return new WaitForSeconds(takenDamage);
　　		GetComponent<Renderer>().enabled = false;
　　		yield return new WaitForSeconds(takenDamage);
　　		GetComponent<Renderer>().enabled = true;
　　		yield return new WaitForSeconds(takenDamage);
　　		GetComponent<Renderer>().enabled = false;
　　		yield return new WaitForSeconds(takenDamage);
　　		GetComponent<Renderer>().enabled = true;
　　		yield return new WaitForSeconds(takenDamage);
            // improve player attributes if struggling
            attackRate += 0.05f;
            takenDamage += 0.025f;
            hitsTaken++;
            walkSpeed  = 6f;
            jumpHeight = 7f;
　　	} 

        // increase player speed
        IEnumerator increaseSpeed(float duration){
            walkSpeed = 10f;
            yield return new WaitForSeconds(duration);
            walkSpeed = 5;
        }
        // decrease player speed
         IEnumerator decreaseSpeed(float duration){
            walkSpeed = 2.5f;
            yield return new WaitForSeconds(duration);
            walkSpeed = 5;
        }

         // increase player speed
        IEnumerator attractCoin(float duration){
            coinMagnet.SetActive(true);
            yield return new WaitForSeconds(duration);
            coinMagnet.SetActive(false);
        }

       // customize player
        public void Custom() {
            // increase player invulnerability, decrease speed
            walkSpeed -= 1.5f;
            jumpHeight -= 2.5f;
            takenDamage += 0.2f;
        }
        // undo player customization
        public void UndoCustom() {
            // undo changes from Custom() method
            walkSpeed += 1.5f;
            jumpHeight += 2.5f;
            takenDamage -= 0.2f;
        }
        public void CreateDust(){
            //Plays dust animation
            dust.Play();
        }
        // pause game with button
        public void HelpClick(){
            if (!paused){
                Pause();
            } else {
                Resume();
            }
        }

        
　　}
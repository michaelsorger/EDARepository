//Michael Sorger Code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour
{
    public int spawnDelay = 0;
    public List<string> playerList = new List<string>();

    public GameObject futureLevel;
    public GameObject pastLevel;
    public GameObject presentLevel;

    private string character1;
    public Transform loc1;
    private Vector3 spawn1;

    private string character2;
    public Transform loc2;
    private Vector3 spawn2;

    private string character3;
    public Transform loc3;
    private Vector3 spawn3;

    private string character4;
    public Transform loc4;
    private Vector3 spawn4;

    public static GameMaster gm;
    public EventSystem gmEventSystem;
    private GameObject currentButton;
    private AxisEventData currentAxis;

    //timer
    public float timeBetweenInputs = 0.15f; //in seconds
    public float deadZone = 0.9f;
    private float timer = 0;

    public GameObject winScreenUI;
    public Image redWin, blueWin;

    private static int redTeamLives;
    private static int blueTeamLives;

    public static int getRedTeamLives()
    {
       return redTeamLives;
    }

    public static int getBlueTeamLives()
    {
        return blueTeamLives;
    }
   // private PlayerController _playerController;
 //   private static int redTeamPlayers = 2;
  //  private static int blueTeamPlayers = 2;

    private void Start()
    {
        gmEventSystem.enabled = false;
        Time.timeScale = 1;
        winScreenUI.SetActive(false);
        redWin.enabled = false;
        blueWin.enabled = false;
        redTeamLives = 5;
        blueTeamLives = 5;
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameMasterTag").GetComponent<GameMaster>();
        }

        character1 = PlayerPrefs.GetString("Character 1 Name");
        Debug.Log(character1);
        character2 = PlayerPrefs.GetString("Character 2 Name");
        Debug.Log(character2);
        character3 = PlayerPrefs.GetString("Character 3 Name");
        Debug.Log(character3);
        character4 = PlayerPrefs.GetString("Character 4 Name");
        Debug.Log(character4);

        instantiateLevel();

        spawn1 = new Vector3(loc1.position.x, loc1.position.y, loc1.position.z);
        spawn2 = new Vector3(loc2.position.x, loc2.position.y, loc2.position.z);
        spawn3 = new Vector3(loc3.position.x, loc3.position.y, loc3.position.z);
        spawn4 = new Vector3(loc4.position.x, loc4.position.y, loc4.position.z);

        gm.StartCoroutine(gm.respawnPlayer(character1, spawn1));
        gm.StartCoroutine(gm.respawnPlayer(character2, spawn2));
        gm.StartCoroutine(gm.respawnPlayer(character3, spawn3));
        gm.StartCoroutine(gm.respawnPlayer(character4, spawn4));

        spawnDelay = 2;
    }

    private void Update()
    {
        if(gmEventSystem.enabled == true)
        {
            if (timer <= 0)
            {
                currentAxis = new AxisEventData(EventSystem.current);
                currentButton = EventSystem.current.currentSelectedGameObject;

                if (Input.GetAxis("VerticalDpad") > deadZone) // move up
                {
                    //Debug.Log("vertical val is = " + Input.GetAxis("VerticalDpad"));
                    currentAxis.moveDir = MoveDirection.Up;
                    ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                }
                else if (Input.GetAxis("VerticalDpad") < -deadZone) // move down
                {
                    currentAxis.moveDir = MoveDirection.Down;
                    ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                }
                else if (Input.GetAxis("HorizontalDpad") > deadZone) // move right
                {
                    currentAxis.moveDir = MoveDirection.Right;
                    ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                }
                else if (Input.GetAxis("HorizontalDpad") < -deadZone) // move left
                {
                    currentAxis.moveDir = MoveDirection.Left;
                    ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
                }
                timer = timeBetweenInputs;
            }

            //timer counting down
            timer -= Time.fixedDeltaTime;
        }
    }

    public static void killPlayer(GameObject player, string name, Vector3 spawnPoint)
    {
        Vector3 newSpawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        string playerToSpawn = gm.newPlayerName(name);
        Debug.Log(playerToSpawn);
        if(playerToSpawn == "Red Brute" || playerToSpawn == "Red Gunner")
        {
            redTeamLives--;
            if(redTeamLives <= 0)
            {
                redTeamLives = 0;
                gm.EndDeathmatch("Blue"); //winner is opposing team RED
            }
            Debug.Log("red team has " + getRedTeamLives() + " lives left!");
        }
        else if(playerToSpawn == "Blue Brute" || playerToSpawn == "Blue Gunner")
        {
            blueTeamLives--;
            if (blueTeamLives <= 0)
            {
                blueTeamLives = 0;
                gm.EndDeathmatch("Red"); //winner is opposing team BLUE
            }
            Debug.Log("blue team has " + getBlueTeamLives() + " lives left!");
        }
        else
        {
            //Do nothing, should end this if block
        }
        gm.playerList.Remove(player.name);
        Destroy(player);
        gm.StartCoroutine(gm.respawnPlayer(playerToSpawn, newSpawnPoint));       
    }

    public IEnumerator respawnPlayer(string player, Vector3 spawnPoint)
    {
        yield return new WaitForSeconds(spawnDelay);
        Debug.Log("TODO, SPAWN PARTICLES");
        if (player == "Red Brute")
        {
            GameObject playerToSpawn = Instantiate(PrefabManager.Instance.RedBrutePrefab);
            playerToSpawn.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            playerToSpawn.GetComponent<AnimInputController>().Left_Joystick_Axis = PlayerPrefs.GetString(player + " left joystick axis"); //PlayerPrefs
            playerToSpawn.GetComponent<AnimInputController>().Right_Trigger_Axis = PlayerPrefs.GetString(player + " right trigger axis");
            playerToSpawn.GetComponent<AnimInputController>().A_Axis = PlayerPrefs.GetString(player + " a axis");
            playerToSpawn.GetComponent<AnimInputController>().B_Axis = PlayerPrefs.GetString(player + " b axis");
           // playerToSpawn.GetComponent<AnimInputController>().B_Axis = PlayerPrefs.GetString(player + "lt" + PlayerPrefs.GetString("Red1"));
            gm.playerList.Add(player);
        }
        else if (player == "Blue Brute")
        {
            GameObject playerToSpawn = Instantiate(PrefabManager.Instance.BlueBrutePrefab);
            playerToSpawn.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            playerToSpawn.GetComponent<AnimInputController>().Left_Joystick_Axis = PlayerPrefs.GetString(player + " left joystick axis");
            playerToSpawn.GetComponent<AnimInputController>().Right_Trigger_Axis = PlayerPrefs.GetString(player + " right trigger axis");
            playerToSpawn.GetComponent<AnimInputController>().A_Axis = PlayerPrefs.GetString(player + " a axis");
            playerToSpawn.GetComponent<AnimInputController>().B_Axis = PlayerPrefs.GetString(player + " b axis");
            gm.playerList.Add(player);
        }
        else if (player == "Red Gunner")
        {
            GameObject playerToSpawn = Instantiate(PrefabManager.Instance.RedGunnerPrefab);
            playerToSpawn.GetComponent<AnimInputController>().Left_Joystick_Axis = PlayerPrefs.GetString(player + " left joystick axis");
            playerToSpawn.GetComponent<AnimInputController>().Right_Trigger_Axis = PlayerPrefs.GetString(player + " right trigger axis");
            playerToSpawn.GetComponent<AnimInputController>().A_Axis = PlayerPrefs.GetString(player + " a axis");
            playerToSpawn.GetComponent<AnimInputController>().B_Axis = PlayerPrefs.GetString(player + " b axis");
            playerToSpawn.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            gm.playerList.Add(player);
        }
        else if (player == "Blue Gunner")
        {
            GameObject playerToSpawn = Instantiate(PrefabManager.Instance.BlueGunnerPrefab);
            playerToSpawn.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
            playerToSpawn.GetComponent<AnimInputController>().Left_Joystick_Axis = PlayerPrefs.GetString(player + " left joystick axis");
            playerToSpawn.GetComponent<AnimInputController>().Right_Trigger_Axis = PlayerPrefs.GetString(player + " right trigger axis");
            playerToSpawn.GetComponent<AnimInputController>().A_Axis = PlayerPrefs.GetString(player + " a axis");
            playerToSpawn.GetComponent<AnimInputController>().B_Axis = PlayerPrefs.GetString(player + " b axis");
            gm.playerList.Add(player);
        }
        else
        {
            Debug.Log("Character does not exist or name is not assigned correctly");
        }

    }

    public string newPlayerName(string Name)
    {
        string playerToSpawn = Name;
        if (Name == "Red Brute")
        {
            return playerToSpawn;
        }
        else if (Name == "Blue Brute")
        {
            return playerToSpawn;
        }
        else if (Name == "Red Gunner")
        {
            return playerToSpawn;
        }
        else if (Name == "Blue Gunner")
        {
            return playerToSpawn;
        }
        else
        {
            Debug.Log("Character does not exist or tag is not assigned correctly");
            return playerToSpawn;
        }
    }

    public void EndDeathmatch(string teamWinner)
    {
        Debug.Log("GAME OVER " + teamWinner + " WINS");
        if(teamWinner == "Red")
        {
            winScreenUI.SetActive(true);
            redWin.enabled = true;
            gmEventSystem.enabled = true;
        }
        else
        {
            //blue won
            winScreenUI.SetActive(true);
            blueWin.enabled = true;
            gmEventSystem.enabled = true;
        }
        Time.timeScale = 0;
    }

    private void instantiateLevel()
    {
        //future 0
        //past 1
        //present 2

        if(PlayerPrefs.GetInt("Selected Map") == 0)
        {
            Instantiate(futureLevel);
        }
        else if (PlayerPrefs.GetInt("Selected Map") == 1)
        {
            Instantiate(pastLevel);
        }
        else if (PlayerPrefs.GetInt("Selected Map") == 2)
        {
            Instantiate(presentLevel);
        }
    }
}

//   playerToSpawn.gameObject.GetComponent<Canvas>().enabled = true;
//playerToSpawn.gameObject.GetComponent<PlayerController>().HealthBar.GetComponent<Canvas>().enabled = true;
//playerToSpawn.gameObject.GetComponent<PlayerController>().playerInit();
//GameObject healthBar = playerToSpawn.GetComponent<HealthBarScript>().health_bar;
//foreach(Canvas child in playerToSpawn.GetComponent<PlayerController>().HealthBar)
//{
//   child.
//}
// 

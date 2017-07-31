//Michael Sorger Code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int spawnDelay = 0;
    public List<string> playerList = new List<string>();

    public GameObject level;

    public GameObject character1; //
    public Transform loc1;
    private Vector3 spawn1;

    public GameObject character2;
    public Transform loc2;
    private Vector3 spawn2;

    public GameObject character3;
    public Transform loc3;
    private Vector3 spawn3;

    public GameObject character4;
    public Transform loc4;
    private Vector3 spawn4;

    public static GameMaster gm;
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
        GameObject chosenLevel = (GameObject)Instantiate(level);

        spawn1 = new Vector3(loc1.position.x, loc1.position.y, loc1.position.z);
        spawn2 = new Vector3(loc2.position.x, loc2.position.y, loc2.position.z);
        spawn3 = new Vector3(loc3.position.x, loc3.position.y, loc3.position.z);
        spawn4 = new Vector3(loc4.position.x, loc4.position.y, loc4.position.z);

        gm.StartCoroutine(gm.respawnPlayer(character1.tag, spawn1));
        gm.StartCoroutine(gm.respawnPlayer(character2.tag, spawn2));
        gm.StartCoroutine(gm.respawnPlayer(character3.tag, spawn3));
        gm.StartCoroutine(gm.respawnPlayer(character4.tag, spawn4));

        spawnDelay = 2;
    }

    public static void killPlayer(GameObject player, string tag, Vector3 spawnPoint)
    {
        Vector3 newSpawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        string playerToSpawn = gm.newPlayerName(tag);
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
        gm.playerList.Remove(player.tag);
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
            Debug.Log("Character does not exist or tag is not assigned correctly");
        }

    }

    public string newPlayerName(string tagName)
    {
        string playerToSpawn = tagName;
        if (tagName == "Red Brute")
        {
            return playerToSpawn;
        }
        else if (tagName == "Blue Brute")
        {
            return playerToSpawn;
        }
        else if (tagName == "Red Gunner")
        {
            return playerToSpawn;
        }
        else if (tagName == "Blue Gunner")
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
        }
        else
        {
            //blue won
            winScreenUI.SetActive(true);
            blueWin.enabled = true;
        }
        Time.timeScale = 0;
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

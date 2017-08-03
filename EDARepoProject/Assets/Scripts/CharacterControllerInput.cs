using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterControllerInput : MonoBehaviour
{
    //Event system variables
    public EventSystem thisEventSystem;
    private GameObject currentButton;
    private AxisEventData currentAxis;
    //timer
    public float timeBetweenInputs = 0.15f; //in seconds
    public float deadZone = 0.9f;
    private float timer = 0;

    // Assign this with the prefab gameobject that has an Image component in the inspector.
    // It already stores the Image component instead of a GO.
    public Image red1CIPrefab;
    public Image red2CIPrefab;
    public Image blue1CIPrefab;
    public Image blue2CIPrefab;

    //flags to check if all are done with selection
    private bool red1Fin = false;
    private bool red2Fin = false;
    private bool blue1Fin = false;
    private bool blue2Fin = false;

    //strings to save character names upon lock in
    private string red1CharName;
    private string red2CharName;
    private string blue1CharName;
    private string blue2CharName;

    // Fill your list with image somewhere.

    //Maybe a better fix? Need 2 lists so that Top controller does not affect bottom controller carousel
    List<Sprite> red1Sprites = new List<Sprite>();
    public int red1Index = 0;

    List<Sprite> red2Sprites = new List<Sprite>();
    public int red2Index = 0;

    List<Sprite> blue1Sprites = new List<Sprite>();
    public int blue1Index = 0;

    List<Sprite> blue2Sprites = new List<Sprite>();
    public int blue2Index = 0;

    void Start()
    {
        thisEventSystem.enabled = false;

        //loading sprites into array from assets
        Sprite[] rSprites = Resources.LoadAll<Sprite>("RedSpriteUI");
        Sprite[] bSprites = Resources.LoadAll<Sprite>("BlueSpriteUI");

        //adding them to the lists I built above red
        foreach (Sprite rs in rSprites)
        {
            red1Sprites.Add(rs);
            red2Sprites.Add(rs);
        }

        //adding sprites to lists I built above blue
        foreach (Sprite bs in bSprites)
        {
            blue1Sprites.Add(bs);
            blue2Sprites.Add(bs);
        }

        // Assign the first image of the list
        red1CIPrefab.sprite = red1Sprites[red1Index];
        red2CIPrefab.sprite = red2Sprites[red2Index];
        blue1CIPrefab.sprite = blue1Sprites[blue1Index];
        blue2CIPrefab.sprite = blue2Sprites[blue2Index];
    }

    void Update()
    {
        //UPDATE ALL THE IMAGES UNTIL PLAYERS ARE SATISFIED WITH CHOICES

        //RED TOP
        if (red1Fin == false)
        {
            if(PlayerPrefs.HasKey("Red Top"))
            {
               // Debug.Log("CharacterControllerInput RedTop controller is controller # " + PlayerPrefs.GetString("Red Top"));
                if (Input.GetAxis("lj" + PlayerPrefs.GetString("Red Top")) > deadZone)
                {
                    Debug.Log("CharacterControllerInput controller " + PlayerPrefs.GetString("Red Top") + " is at value " + (Input.GetAxis("lj" + PlayerPrefs.GetString("Red Top"))));
                    NextChar(ref red1Index, red1Sprites, red1CIPrefab);
                }
                else if (Input.GetAxis("lj" + PlayerPrefs.GetString("Red Top")) < -deadZone)
                {
                    PreviousChar(ref red1Index, red1Sprites, red1CIPrefab);
                }
                else if (Input.GetAxis("a" + PlayerPrefs.GetString("Red Top")) > 0)
                {
                    LockInBox(ref red1Fin, red1CIPrefab);
                    red1CharName = red1Sprites[red1Index].name;

                    //require each character on their respected team to be unique
                    red1Sprites.RemoveAt(red1Index);
                    red2Sprites.RemoveAt(red1Index);

                    NextChar(ref red2Index,red2Sprites,red2CIPrefab);
                    for (int i = 0; i < red1Sprites.Count; i++)
                    {
                        Debug.Log("available red 1 sprites left = " + red1Sprites[i]);
                    }
                }
            }
        }

        //RED BOTTOM
        if (red2Fin == false)
        {
            if(PlayerPrefs.HasKey("Red Bottom"))
            {
                if (Input.GetAxis("lj" + PlayerPrefs.GetString("Red Bottom")) > deadZone)
                {
                    NextChar(ref red2Index, red2Sprites, red2CIPrefab);
                }
                else if (Input.GetAxis("lj" + PlayerPrefs.GetString("Red Bottom")) < -deadZone)
                {
                    PreviousChar(ref red2Index, red2Sprites, red2CIPrefab);
                }
                else if (Input.GetAxis("a" + PlayerPrefs.GetString("Red Bottom")) > 0)
                {
                    LockInBox(ref red2Fin, red2CIPrefab);
                    red2CharName = red2Sprites[red2Index].name;

                    //require each character on their respected team to be unique
                    red2Sprites.RemoveAt(red2Index);
                    red1Sprites.RemoveAt(red2Index);

                    NextChar(ref red1Index, red1Sprites, red1CIPrefab);

                    for (int i = 0; i < red2Sprites.Count; i++)
                    {
                        Debug.Log("available red 2 sprites left = " + red2Sprites[i]);
                    }
                }
            }
        }

        //BLUE TOP
        if (blue1Fin == false)
        {
            if (PlayerPrefs.HasKey("Blue Top"))
            {
              //  Debug.Log("CharacterControllerInput BlueTop controller is controller # " + PlayerPrefs.GetString("Blue Top"));
                if (Input.GetAxis("lj" + PlayerPrefs.GetString("Blue Top")) > deadZone)
                {
                    NextChar(ref blue1Index, blue1Sprites, blue1CIPrefab);
                }
                else if (Input.GetAxis("lj" + PlayerPrefs.GetString("Blue Top")) < -deadZone)
                {
                    PreviousChar(ref blue1Index, blue1Sprites, blue1CIPrefab);
                }
                else if (Input.GetAxis("a" + PlayerPrefs.GetString("Blue Top")) > 0)
                {
                    LockInBox(ref blue1Fin, blue1CIPrefab);
                    blue1CharName = blue1Sprites[blue1Index].name;

                    //require each character on their respected team to be unique
                    blue1Sprites.RemoveAt(blue1Index);
                    blue2Sprites.RemoveAt(blue1Index);

                    NextChar(ref blue2Index, blue2Sprites, blue2CIPrefab);

                    for (int i = 0; i < blue1Sprites.Count; i++)
                    {
                        Debug.Log("available blue 1 sprites left = " + blue1Sprites[i]);
                    }
                }
            }
        }

        //BLUE BOTTOM
        if (blue2Fin == false)
        {
            if (PlayerPrefs.HasKey("Blue Bottom"))
            {
                if (Input.GetAxis("lj" + PlayerPrefs.GetString("Blue Bottom")) > deadZone)
                {
                    NextChar(ref blue2Index, blue2Sprites, blue2CIPrefab);
                }
                else if (Input.GetAxis("lj" + PlayerPrefs.GetString("Blue Bottom")) < -deadZone)
                {
                    PreviousChar(ref blue2Index, blue2Sprites, blue2CIPrefab);
                }
                else if (Input.GetAxis("a" + PlayerPrefs.GetString("Blue Bottom")) > 0)
                {
                    LockInBox(ref blue2Fin, blue2CIPrefab);
                    blue2CharName = blue2Sprites[blue2Index].name;

                    //require each character on their respected team to be unique
                    blue2Sprites.RemoveAt(blue2Index);
                    blue1Sprites.RemoveAt(blue2Index);

                    NextChar(ref blue1Index, blue1Sprites, blue1CIPrefab);

                    for (int i = 0; i < blue2Sprites.Count; i++)
                    {
                        Debug.Log("available blue 2sprites left = " + blue2Sprites[i]);
                    }
                }
            }
        }

        //IF ALL ARE LOCKED IN
        //red1Fin && red2Fin && blue1Fin && blue2Fin
        if (red1Fin && blue1Fin)
        {
            thisEventSystem.enabled = true;
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
    

    public void NextChar(ref int index, List<Sprite> colorSprites, Image boxPrefab)
    {
        if (index == colorSprites.Count - 1)
        {
            index = 0;
            boxPrefab.sprite = colorSprites[index];
        }
        else
        {
            index++;
            boxPrefab.sprite = colorSprites[index];
        }
    }

    public void PreviousChar(ref int index, List<Sprite> colorSprites, Image boxPrefab)
    {
        if (index == 0)
        {
            index = colorSprites.Count - 1;
            boxPrefab.sprite = colorSprites[index];
        }
        else
        {
            index--;
            boxPrefab.sprite = colorSprites[index];
        }
    }

    public void LockInBox(ref bool boxLoc, Image imageToChange)
    {
        //Cosmetic changes, lock the controls, change opacity of image
        boxLoc = true;
        Color c = imageToChange.color;
        c.a = .5f; //oh so you thought this would be 1-255?? you could not be more wrong lmfao
        Debug.Log("Changed color ");
        imageToChange.color = c;
    }

    public void onGoButton()
    {
        Debug.Log("onGoButton clicked! Red1 character name is: " + red1CharName);
        Debug.Log("onGoButton clicked! Red2 character name is: " + red2CharName);
        Debug.Log("onGoButton clicked! Blue1 character name is: " + blue1CharName);
        Debug.Log("onGoButton clicked! Blue2 character name is: " + blue2CharName);

        //writing character names to be spawned up to playerPrefs to be accessed
        PlayerPrefs.SetString("Character 1 Name", red1CharName);
        PlayerPrefs.SetString("Character 2 Name", red2CharName);
        PlayerPrefs.SetString("Character 3 Name", blue1CharName);
        PlayerPrefs.SetString("Character 4 Name", blue2CharName);


        //write all axis for all characters using playerPrefs
        //For Red Top player
        if (PlayerPrefs.HasKey("Red Top"))
        {
            PlayerPrefs.SetString(red1CharName + " left joystick axis", "lj" + PlayerPrefs.GetString("Red Top"));
            PlayerPrefs.SetString(red1CharName + " right trigger axis", "rt" + PlayerPrefs.GetString("Red Top"));
            PlayerPrefs.SetString(red1CharName + " a axis", "a" + PlayerPrefs.GetString("Red Top"));
            PlayerPrefs.SetString(red1CharName + " b axis", "b" + PlayerPrefs.GetString("Red Top"));
        }

        //For Red Bot player
        if (PlayerPrefs.HasKey("Red Bottom"))
        {
            PlayerPrefs.SetString(red2CharName + " left joystick axis", "lj" + PlayerPrefs.GetString("Red Bottom"));
            PlayerPrefs.SetString(red2CharName + " right trigger axis", "rt" + PlayerPrefs.GetString("Red Bottom"));
            PlayerPrefs.SetString(red2CharName + " a axis", "a" + PlayerPrefs.GetString("Red Bottom"));
            PlayerPrefs.SetString(red2CharName + " b axis", "b" + PlayerPrefs.GetString("Red Bottom"));
        }

        //For Blue Top player
        if (PlayerPrefs.HasKey("Blue Top"))
        {
            PlayerPrefs.SetString(blue1CharName + " left joystick axis", "lj" + PlayerPrefs.GetString("Blue Top"));
            PlayerPrefs.SetString(blue1CharName + " right trigger axis", "rt" + PlayerPrefs.GetString("Blue Top"));
            PlayerPrefs.SetString(blue1CharName + " a axis", "a" + PlayerPrefs.GetString("Blue Top"));
            PlayerPrefs.SetString(blue1CharName + " b axis", "b" + PlayerPrefs.GetString("Blue Top"));
        }

        //For Blue Bot player
        if (PlayerPrefs.HasKey("Blue Bottom"))
        {
            PlayerPrefs.SetString(blue2CharName + " left joystick axis", "lj" + PlayerPrefs.GetString("Blue Bottom"));
            PlayerPrefs.SetString(blue2CharName + " right trigger axis", "rt" + PlayerPrefs.GetString("Blue Bottom"));
            PlayerPrefs.SetString(blue2CharName + " a axis", "a" + PlayerPrefs.GetString("Blue Bottom"));
            PlayerPrefs.SetString(blue2CharName + " b axis", "b" + PlayerPrefs.GetString("Blue Bottom"));
        }
    }
}

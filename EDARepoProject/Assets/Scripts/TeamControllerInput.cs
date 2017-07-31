using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TeamControllerInput : MonoBehaviour
{

    private GameObject currentButton;
    private AxisEventData currentAxis;
    public EventSystem thisEventSystem;

    //controllers to move
    public GameObject x1;
    public GameObject x2;
    public GameObject x3;
    public GameObject x4;

    /*
    public RectTransform xStartPos1;
    public RectTransform xStartPos2;
    public RectTransform xStartPos3;
    public RectTransform xStartPos4;
    */

    private Vector3 midPos1;
    private Vector3 midPos2;
    private Vector3 midPos3;
    private Vector3 midPos4;

    public GameObject redTop;
    public GameObject redBottom;
    public GameObject blueTop;
    public GameObject blueBottom;

    private bool rTopIsTaken = false; //red top box... etc
    private bool rBotIsTaken = false;
    private bool bTopIsTaken = false;
    private bool bBotIsTaken = false;

    private bool x1Fin = false;
    private bool x2Fin = false;
    private bool x3Fin = false;
    private bool x4Fin = false;

    private bool allFin = false;
    //timer
    public float timeBetweenInputs = 0.15f; //in seconds
    public float deadZone = 0.5f;
    private float timer = 0;

    void Start()
    {
        thisEventSystem.enabled = false;

        //assign original mid positions for each of the xbox controllers
        midPos1 = new Vector3(x1.transform.position.x, x1.transform.position.y, x1.transform.position.z);
        midPos2 = new Vector3(x2.transform.position.x, x2.transform.position.y, x2.transform.position.z);
        midPos3 = new Vector3(x3.transform.position.x, x3.transform.position.y, x3.transform.position.z);
        midPos4 = new Vector3(x4.transform.position.x, x4.transform.position.y, x4.transform.position.z);

        PlayerPrefs.DeleteAll();

        //defaults at start of team select screen
        //The specified names of the character do not matter as their sprites do not show up
        //only worried about initializing axis
        PlayerPrefs.SetString("Red Brute left joystick axis", "lj1");
        PlayerPrefs.SetString("Red Brute right trigger axis", "rt1");
        PlayerPrefs.SetString("Red Brute a axis", "a1");
        PlayerPrefs.SetString("Red Brute b axis", "b1");

       // PlayerPrefs.SetString("Red1 Character", "Brute");
        //PlayerPrefs.SetString("Red1 Controller", "1");

        PlayerPrefs.SetString("Blue Brute left joystick axis", "lj2");
        PlayerPrefs.SetString("Blue Brute right trigger axis", "rt2");
        PlayerPrefs.SetString("Blue Brute a axis", "a2");
        PlayerPrefs.SetString("Blue Brute b axis", "b2");

        //PlayerPrefs.SetString("Blue1 Character", "Brute");
        //PlayerPrefs.SetString("Blue1 Controller", "2");

        PlayerPrefs.SetString("Red Gunner left joystick axis", "lj3");
        PlayerPrefs.SetString("Red Gunner right trigger axis", "rt3");
        PlayerPrefs.SetString("Red Gunner a axis", "a3");
        PlayerPrefs.SetString("Red Gunner b axis", "b3");

       // PlayerPrefs.SetString("Red2 Character", "Gunner");
       //PlayerPrefs.SetString("Red2 Controller", "3");

        PlayerPrefs.SetString("Blue Gunner left joystick axis", "lj4");
        PlayerPrefs.SetString("Blue Gunner left joystick axis", "rt4");
        PlayerPrefs.SetString("Blue Gunner a axis", "a4");
        PlayerPrefs.SetString("Blue Gunner b axis", "b4");

       // PlayerPrefs.SetString("Blue2 Character", "Gunner");
        //PlayerPrefs.SetString("Blue2 Controller", "4");
    }

    // Update is called once per frame
    void Update()
    {
        if (x1Fin == false)
        {
            bool x1IsGood = moveControllerByXAxis("lj1", x1, midPos1);
            if (isSelected("a1", ref x1Fin, x1IsGood, x1))
            {
                x1Fin = true;
            }
        }
        if (x2Fin == false)
        {
            bool x2IsGood = moveControllerByXAxis("lj2", x2, midPos2);
            if (isSelected("a2", ref x2Fin, x2IsGood, x2))
            {
                x2Fin = true;
            }
        }
        if (x3Fin == false)
        {
            bool x3IsGood = moveControllerByXAxis("lj3", x3, midPos3);
            if (isSelected("a3", ref x3Fin, x3IsGood, x3))
            {
                x3Fin = true;
            }
        }
        if (x4Fin == false)
        {
            bool x4IsGood = moveControllerByXAxis("lj4", x4, midPos4);
            if (isSelected("a4", ref x4Fin, x4IsGood, x4))
            {
                x4Fin = true;
            }
        }

        //if (x1Fin && x2Fin && x3Fin && x4Fin)
        if (x1Fin)
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

    private bool moveControllerByXAxis(string axisName, GameObject xController, Vector3 midPos)
    {
        //Debug.Log("moveControllerByXAxis" + " axisName = " + axisName);
        //move controller to the left
        bool isMoved = true;
        //in middle
        
        if (Input.GetAxis(axisName) < -deadZone)
        {
            if (xController.transform.position.x == midPos.x)
            {
                if (rTopIsTaken == false)
                {
                    xController.transform.position = new Vector3(redTop.transform.position.x, redTop.transform.position.y, redTop.transform.position.z);
                    rTopIsTaken = true;
                    isMoved = true;
                    return isMoved;
                }
                else if (rBotIsTaken == false)
                {
                    xController.transform.position = new Vector3(redBottom.transform.position.x, redBottom.transform.position.y, redBottom.transform.position.z);
                    rBotIsTaken = true;
                    isMoved = true;
                    return isMoved;
                }
                else
                {
                    Debug.Log("This side is full of controllers");
                    isMoved = false;
                    return isMoved;
                }
            }

            //controller is on blue team, move back to mid
            else if(xController.transform.position.x > midPos.x && Vector3.Distance(xController.transform.position, blueTop.transform.position) < .1f) //check is blue top is taken
            {
                xController.transform.position = new Vector3(midPos.x, midPos.y, midPos.z);
                bTopIsTaken = false;
                isMoved = false;
                return isMoved;
            }
            else if (xController.transform.position.x > midPos.x && Vector3.Distance(xController.transform.position, blueBottom.transform.position) < .1f) //blue bot check
            {
                xController.transform.position = new Vector3(midPos.x, midPos.y, midPos.z);
                bBotIsTaken = false;
                isMoved = false;
                return isMoved;
            }

            //already on red, can't move left
            else
            {
               // Debug.Log("Can't move left any farther");
                isMoved = true;
                return isMoved;
            }
        }

        //move controller to the right, check if someone is already there
        if (Input.GetAxis(axisName) > deadZone)
        {
            //in middle
            if (xController.transform.position.x == midPos.x)
            {
                if (bTopIsTaken == false)
                {
                    xController.transform.position = new Vector3(blueTop.transform.position.x, blueTop.transform.position.y, blueTop.transform.position.z);
                    bTopIsTaken = true;
                    isMoved = true;
                    return isMoved;
                }
                else if (bBotIsTaken == false)
                {
                    xController.transform.position = new Vector3(blueBottom.transform.position.x, blueBottom.transform.position.y, blueBottom.transform.position.z);
                    bBotIsTaken = true;
                    isMoved = true;
                    return isMoved;
                }
                else
                {
                    Debug.Log("This side is full of controllers");
                    isMoved = false;
                    return isMoved;
                }
            }

            //controller is on red team, move back to mid
            else if (xController.transform.position.x < midPos.x && Vector3.Distance(xController.transform.position, redTop.transform.position) < .1f) //check for top
            {
                xController.transform.position = new Vector3(midPos.x, midPos.y, midPos.z);
                rTopIsTaken = false;
                isMoved = false;
                return isMoved;
            }
            else if (xController.transform.position.x < midPos.x && Vector3.Distance(xController.transform.position, redBottom.transform.position) < .1f) //check for bot
            {
                xController.transform.position = new Vector3(midPos.x, midPos.y, midPos.z);
                rBotIsTaken = false;
                isMoved = false;
                return isMoved;
            }
            else
            {
                //Debug.Log("Can't move right any farther");
                isMoved = true;
                return isMoved;
            }
        }
        if (xController.transform.position != midPos)
        {
            isMoved = true;
            return isMoved;
        }
        return false;
    }

    private bool isSelected(string buttonAxis, ref bool xBoxFin, bool inGoodPosition, GameObject xController)
    {
        if(Input.GetAxis(buttonAxis) > 0 && inGoodPosition == true)
        {
            Color c = xController.GetComponent<Image>().color;
            c.a = .5f; //oh so you thought this would be 1-255?? you could not be more wrong lmfao
            Debug.Log("Changed color ");
            xController.GetComponent<Image>().color = c;
            xBoxFin = true;
            return xBoxFin;
        }
        else
        {
            return false;
        }
    }

    //onGoButton on teamSelect
    public void onGoButton()
    {
        //CONTROLLER 1
        if(Vector3.Distance(x1.transform.position, redTop.transform.position) < .1f)
        {
            //x1 is red Top
            PlayerPrefs.SetString("Red Top", "1");
        }
        else if (Vector3.Distance(x1.transform.position, redBottom.transform.position) < .1f)
        {
            //x1 is red bottom
            PlayerPrefs.SetString("Red Bottom", "1");
        }
        else if (Vector3.Distance(x1.transform.position, blueTop.transform.position) < .1f)
        {
            //x1 is blue top
            PlayerPrefs.SetString("Blue Top", "1");
        }
        else if (Vector3.Distance(x1.transform.position, blueBottom.transform.position) < .1f)
        {
            //x1 is blue bottom
            PlayerPrefs.SetString("Blue Bottom", "1");
        }

        //CONTROLLER 2
        if (Vector3.Distance(x2.transform.position, redTop.transform.position) < .1f)
        {
            //x2 is red Top
            PlayerPrefs.SetString("Red Top", "2");
        }
        else if (Vector3.Distance(x2.transform.position, redBottom.transform.position) < .1f)
        {
            //x2 is red bottom
            PlayerPrefs.SetString("Red Bottom", "2");
        }
        else if (Vector3.Distance(x2.transform.position, blueTop.transform.position) < .1f)
        {
            //x2 is blue top
            PlayerPrefs.SetString("Blue Top", "2");
        }
        else if (Vector3.Distance(x2.transform.position, blueBottom.transform.position) < .1f)
        {
            //x2 is blue bottom
            PlayerPrefs.SetString("Blue Bottom", "2");
        }

        //CONTROLLER 3
        if (Vector3.Distance(x3.transform.position, redTop.transform.position) < .1f)
        {
            //x3 is red Top
            PlayerPrefs.SetString("Red Top", "3");
        }
        else if (Vector3.Distance(x3.transform.position, redBottom.transform.position) < .1f)
        {
            //x3 is red bottom
            PlayerPrefs.SetString("Red Bottom", "3");
        }
        else if (Vector3.Distance(x3.transform.position, blueTop.transform.position) < .1f)
        {
            //x3 is blue top
            PlayerPrefs.SetString("Blue Top", "3");
        }
        else if (Vector3.Distance(x3.transform.position, blueBottom.transform.position) < .1f)
        {
            //x3 is blue bottom
            PlayerPrefs.SetString("Blue Bottom", "3");
        }

        //CONTROLLER 4
        if (Vector3.Distance(x4.transform.position, redTop.transform.position) < .1f)
        {
            //x4 is red Top
            PlayerPrefs.SetString("Red Top", "4");
        }
        else if (Vector3.Distance(x4.transform.position, redBottom.transform.position) < .1f)
        {
            //x4 is red bottom
            PlayerPrefs.SetString("Red Bottom", "4");
        }
        else if (Vector3.Distance(x4.transform.position, blueTop.transform.position) < .1f)
        {
            //x4 is blue top
            PlayerPrefs.SetString("Blue Top", "4");
        }
        else if (Vector3.Distance(x4.transform.position, blueBottom.transform.position) < .1f)
        {
            //x4 is blue bottom
            PlayerPrefs.SetString("Blue Bottom", "4");
        }
    }
}

//playerPrefs goes here
//OnGoButton of characterSelect
/*
PlayerPrefs.SetString(array[Red Brute] + "left joystick axis", "lj" + getInt(playerPrefs("Red Top"));
PlayerPrefs.SetString("Red Brute left joystick axis", "lj1");
PlayerPrefs.SetString("Red Brute right trigger axis", "rt1");
PlayerPrefs.SetString("Red Brute a axis", "a1");
PlayerPrefs.SetString("Red Brute b axis", "b1");
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBounds : MonoBehaviour
{
    private Transform playerTransform;  //reference to the player transform
    bool isVisible = true;              //true if the player is inside camera bounds

    public GameObject pointer;          //reference pointer game object (set in inspector. one per player)
    private Transform pointerTransform; //reference to the player pointer's transform

    private Vector2 screenMax, screenMin;   //hold max and min screen values

    public enum posType { topRight, topLeft, top, right, left, other }; //legibility

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.GetComponent<Transform>();   //create the reference

        pointerTransform = pointer.transform;   //create the reference
        pointer.SetActive(false);               //pointer starts inactive (player starts inside the camera range,I think. subject to change)

        screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));    //max camera range
        screenMin = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));                           //min camera range
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisible)     //if the player is out of bounds
        {
            posType currentPosition = getPositionType();    //gets current player position (refer back to the enum)
            setPointerPosition(currentPosition);            //sets pointer's position
            setPointerRotation(currentPosition);            //sets pointer's rotation
        }
    }

    private void OnBecameInvisible()        //when object leaves the camera bounds
    {
        pointer.SetActive(true);    //pointer becomes active
        isVisible = false;          //player is not visible anymore
    }

    private void OnBecameVisible()          //when object comes back into the camera bounds
    {
        pointer.SetActive(false);   //pointer becomes hidden
        isVisible = true;           //player is now visible again
    }

    private posType getPositionType()       //gets current player position (refer back to the enum)
    {
        posType pos;

        if (playerTransform.position.y > screenMax.y)   //out of the screen (top)
        {
            if (playerTransform.position.x > screenMax.x)           //also out on the right
            {
                pos = posType.topRight;
            }
            else if (playerTransform.position.x < screenMin.x)      //also out on the left
            {
                pos = posType.topLeft;
            }
            else                                                    //not out on the horizontal axis
            {
                pos = posType.top;
            }
        }
        else                                            //not out on the top
        {
            if (playerTransform.position.x > screenMax.x)       //out of the screen (right)
            {
                pos = posType.right;
            }
            else if (playerTransform.position.x < screenMin.x)   //out of the screen (left)
            {
                pos = posType.left;
            }
            else
            {
                pos = posType.other;
            }
        }
        return pos;
    }

    private void setPointerPosition(posType pos)    //sets position, based on the position variable
    {
        Vector2 newPosition;    //new pointer position

        switch (pos)
        {
            case (posType.topRight):
                newPosition = new Vector2(screenMax.x - 1, screenMax.y - 1);
                break;

            case (posType.topLeft):
                newPosition = new Vector2(screenMin.x + 1, screenMax.y - 1);
                break;

            case (posType.top):
                newPosition = new Vector2(playerTransform.position.x, screenMax.y - 1);
                break;

            case (posType.right):
                newPosition = new Vector2(screenMax.x - 1, playerTransform.position.y);
                break;

            case (posType.left):
                newPosition = new Vector2(screenMin.x + 1, playerTransform.position.y);
                break;

            default:
                newPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
                break;
        }
        pointerTransform.position = newPosition;    //set new position
    }

    private void setPointerRotation(posType pos)
    {
        switch (pos)
        {
            case posType.topRight:
                pointerTransform.rotation = Quaternion.Euler(0, 0, 45);
                break;
            case posType.topLeft:
                pointerTransform.rotation = Quaternion.Euler(0, 0, 135);
                break;
            case posType.top:
                pointerTransform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case posType.right:
                pointerTransform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case posType.left:
                pointerTransform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            default:
                break;
        }
    }
}

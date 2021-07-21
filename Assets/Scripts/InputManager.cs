using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    PointerEventData pointerEventData;
    [SerializeField] EventSystem eventSystem;

    [SerializeField] BallController Launcher;
       

    void Update()
    {
        //Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);

        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Use RaycastAll to capture pointerEventData (including UI) 
        EventSystem.current.RaycastAll(pointerEventData, results);

        if (results.Count > 0) 
        { 
            // if you hit anything under the HUD game object parent
            if (results[0].gameObject.tag == "HUD")
            {
                Launcher.gameObject.GetComponent<BallController>().enabled = false;
            }
        }
        Launcher.gameObject.GetComponent<BallController>().enabled = true;
    }
}

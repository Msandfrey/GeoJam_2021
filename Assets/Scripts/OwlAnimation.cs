using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlAnimation : MonoBehaviour
{
    [SerializeField]
    OwlController owl;
    [SerializeField]
    float flapSpeed = .1f;
    //owl parts
    [SerializeField]
    GameObject owlHead;
    [SerializeField]
    GameObject owlBody;
    [SerializeField]
    GameObject owlWingFront;
    [SerializeField]
    GameObject owlWingBack;
    [SerializeField]
    GameObject owlTail;
    [SerializeField]
    GameObject owlFeet;

    //owl head spritesheets
    [SerializeField]
    Sprite[] owlBlink;
    [SerializeField]
    Sprite[] owlTurnHead;

    //rotations
    [SerializeField]
    Quaternion FrontWingToRotUp;
    [SerializeField]
    Quaternion FrontWingToRotDown;
    [SerializeField]
    Quaternion BackWingToRotUp;
    [SerializeField]
    Quaternion BackWingToRotDown = Quaternion.identity;
    [SerializeField]
    Quaternion TailToRotFront;
    [SerializeField]
    Quaternion TailToRotBack;

    Quaternion TargetFrontWing = Quaternion.identity;
    Quaternion TargetBackWing = Quaternion.identity;
    Quaternion TargetTail = Quaternion.identity;

    Quaternion currFrontWing;
    Quaternion currBackWing;
    Quaternion currTail;

    Coroutine flyCoroutineRef;

    // Start is called before the first frame update
    void Start()
    {
        //FrontWingToRotUp = new Quaternion(0, 0, 10, owlWingFront.transform.rotation.w);
        //FrontWingToRotDown = new Quaternion(0, 0, -10, owlWingFront.transform.rotation.w);
        //BackWingToRotUp = new Quaternion(0, 0, 10, owlWingBack.transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFlying()
    {
        //call fly coroutine
        flyCoroutineRef = StartCoroutine(FlyBabyFly());
    }
    public void StopFlying()
    {
        //stop fly coroutine
        StopCoroutine(flyCoroutineRef);
        ResetAllRotations();
    }
    IEnumerator FlyBabyFly()
    {
        //choose target for front wing
        if(TargetFrontWing == FrontWingToRotUp)
        {
            TargetFrontWing = FrontWingToRotDown;
        }
        else
        {
            TargetFrontWing = FrontWingToRotUp;
        }
        //choose target for back wing
        if (TargetBackWing == BackWingToRotUp)
        {
            TargetBackWing = BackWingToRotDown;
        }
        else
        {
            TargetBackWing = BackWingToRotUp;
        }
        //choose target for tail
        if (TargetTail == TailToRotFront)
        {
            TargetTail = TailToRotBack;
        }
        else
        {
            TargetTail = TailToRotFront;
        }

        currFrontWing = owlWingFront.transform.rotation;
        currBackWing = owlWingBack.transform.rotation;
        currTail = owlTail.transform.rotation;

        while (true)
        {
            owlWingFront.transform.rotation = Quaternion.Lerp(currFrontWing, TargetFrontWing, Time.time * flapSpeed);
            yield return null;
        }
    }
    void ResetAllRotations()
    {
        owlWingFront.transform.rotation = Quaternion.identity;
        owlWingBack.transform.rotation = Quaternion.identity;
        owlTail.transform.rotation = Quaternion.identity;
    }
}

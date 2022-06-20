using UnityEngine;
using System;

[System.Serializable]
public class FloraSlideState : FloraControllerState
{
    public float speed;
    float rotDelay = 0.15f;
    public float sustain;
    public float decay;

    Vector3 direction;
    public Vector3 onD, offD;
    string nextAnim = "";
    float timeIn = 0;
    float cspeed = 0;
    Quaternion rotation;
    bool jumping = false;
    public override void OnStateEnter()
    {
        timeIn = 0;
        cspeed = speed;
        direction = con.moveInputVector;
        rotation = con.tform.rotation;
        con.Motor.SetCapsuleDimensions(onD.x, onD.y, onD.z);
        nextAnim = "slide";
        jumping = false;
    }

    public override void OnStateExit()
    {
        con.Motor.SetCapsuleDimensions(offD.x, offD.y, offD.z);
    }

    public override bool BypassAnimation(float deltaTime)
    {
        if (!jumping && con.jumpanim)
        {
            nextAnim = "slide_jump";
        }
        if (nextAnim.Length > 0)
        {
            con.animgr.SetState(nextAnim);
            nextAnim = "";
        }
        return true;
    }

    public virtual bool BypassAnimationExitCallback(string state)
    {
        if (state.Equals("slide_jump_land"))
        {
            con.SetControllerState(FLORASTATE.DEFAULT);
        }
        return true;
    }


    public override bool BypassRotation(ref Quaternion currentRotation, float deltaTime)
    {
        currentRotation = Quaternion.LookRotation(direction, con.Motor.CharacterUp);
        return true;
    }

    public override bool BypassInputMovement(ref Vector3 currentVelocity, float deltaTime)
    {
        if (jumping)
        {
            if (con.Motor.GroundingStatus.IsStableOnGround)
            {
                nextAnim="slide_jump_land";
            }
            currentVelocity = cspeed * direction * Time.deltaTime;
        }
        else
        {
            if(!jumping && !con.Motor.GroundingStatus.IsStableOnGround &&currentVelocity.y < 0){
                con.SetControllerState(FLORASTATE.DEFAULT);
            }
            else{
            timeIn += deltaTime;

            if (timeIn > sustain && cspeed > 0)
            {
                cspeed -= decay * Time.deltaTime;
                if (cspeed <= 0)
                {
                    con.SetControllerState(FLORASTATE.DEFAULT);
                }

            }
            else
            {
                cspeed = 0;
            }
            currentVelocity = cspeed * direction * Time.deltaTime;
            }
        }
        return true;
    }
}
using UnityEngine;
using System;

public class FloraAnimationState : ActorAnimationState {
    FloraAnimationManager con;
    
    public FLORASTATE onEnterControllerState=FLORASTATE.NONE, onExitControllerState=FLORASTATE.NONE;

    public void SetController(FloraAnimationManager fmc){
        con=fmc;
    }

    protected override void ChStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if(onEnterControllerState != FLORASTATE.NONE){
            con.SetControllerState(onEnterControllerState);
        }
    }

    protected override void ChExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if(onExitControllerState!=FLORASTATE.NONE){
            con.SetControllerState(onExitControllerState);
        }
    }
}
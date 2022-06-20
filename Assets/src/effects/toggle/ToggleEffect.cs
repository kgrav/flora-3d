using UnityEngine;
using System;

public abstract class ToggleEffect : NVComponent{
    public bool startActive;
    public string label;
    void Awake(){
        if(startActive){
            SetActive(true);
        }
    }
    protected bool active;
    public void SetActive(bool toggle){
        bool _active = active;
        active = toggle;
        if(active != _active){
            OnSwitchActivity(toggle);
        }
    }

    protected virtual void WhileActive(){}

    void Update(){
        if(active){
            WhileActive();
        }
    }

    protected virtual void OnSwitchActivity(bool newActivity){}
}
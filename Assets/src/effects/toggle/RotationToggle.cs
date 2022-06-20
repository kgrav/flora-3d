using UnityEngine;
using System;

public class RotationToggle : ToggleEffect{
    public Vector3 euler;
    public bool transformEuler;

    void Start(){
        if(transformEuler)
            euler = tform.TransformDirection(euler);
    }
    protected override void WhileActive(){
        tform.rotation = Quaternion.Euler(euler*Time.deltaTime)*tform.rotation;
    }
}
using System;
using UnityEngine;

public class EffectsTable : MonoBehaviour {
    
    static EffectsTable _fxTable;
    public static EffectsTable fxTable {get{if(!_fxTable) _fxTable = FindObjectOfType<EffectsTable>(); return _fxTable;}}
    
    public Effect[] effects;
    

    public static void TriggerEffect(int effectKey, Vector3 position, Vector3 direction){
        fxTable.effects[effectKey].Trigger(position, direction);
    }
}
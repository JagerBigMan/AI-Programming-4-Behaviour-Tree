using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class HasDroneAlertCT : ConditionTask<Transform>
{
    protected override bool OnCheck()
    {
        return DroneComboSystem.alertActive;
    }
}
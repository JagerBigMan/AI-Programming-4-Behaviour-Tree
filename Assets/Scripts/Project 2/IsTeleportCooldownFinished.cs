using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class IsTeleportCooldownFinished : ConditionTask<Transform>
{
    public BBParameter<float> teleportCooldownEndTime;

    protected override bool OnCheck()
    {
        return Time.time >= teleportCooldownEndTime.value;
    }
}
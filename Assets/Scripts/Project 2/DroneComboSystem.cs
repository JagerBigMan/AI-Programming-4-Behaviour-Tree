using UnityEngine;

public static class DroneComboSystem
{
    public static bool alertActive = false;
    public static Vector3 alertPosition = Vector3.zero;

    public static bool playerFrozen = false;

    public static void TriggerAlert(Vector3 position)
    {
        alertActive = true;
        alertPosition = position;
        playerFrozen = true;
    }

    public static void ClearAlert()
    {
        alertActive = false;
    }

    public static void UnfreezePlayer()
    {
        playerFrozen = false;
    }
}
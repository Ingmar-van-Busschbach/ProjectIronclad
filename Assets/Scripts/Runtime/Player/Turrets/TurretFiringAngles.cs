using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretFiringAngles", menuName = "ScriptableObjects/Player/TurretFiringAngles", order = 1)]
public class TurretFiringAngles : ScriptableObject
{
    public List<Struct_FireAngleLimit> verticalFiringAngles = new();
    public Vector2 horizontalFiringAngles;
    public Vector2 rotationSpeed;

    public Vector2 GetVerticalAngles(float currentAngle, bool inclusive = false)
    {
        Vector2 firingAngles = new Vector2(-180, 180);
        foreach(Struct_FireAngleLimit fireAngleLimit in verticalFiringAngles)
        {
            firingAngles = GetSmallestFiringAngle(fireAngleLimit, firingAngles, currentAngle, inclusive);
        }
        return firingAngles;
    }

    private Vector2 GetAngles(Struct_FireAngleLimit fireAngleLimit, float currentAngle, bool inclusive = false)
    {
        if (inclusive)
        {
            if (currentAngle >= fireAngleLimit.horizontalAngles.x && currentAngle <= fireAngleLimit.horizontalAngles.y)
            {
                return fireAngleLimit.verticalAngles;
            }
        }
        else
        {
            if (currentAngle > fireAngleLimit.horizontalAngles.x && currentAngle < fireAngleLimit.horizontalAngles.y)
            {
                return fireAngleLimit.verticalAngles;
            }
        }
        return new Vector2(-180, 180);
    }

    private Vector2 GetSmallestFiringAngle(Struct_FireAngleLimit fireAngleLimit, Vector2 currentVector, float currentAngle, bool inclusive = false)
    {
        Vector2 newAngles = GetAngles(fireAngleLimit, currentAngle, inclusive);
        if (newAngles.x > currentVector.x)
        {
            currentVector.x = newAngles.x;
        }
        if (newAngles.y < currentVector.y)
        {
            currentVector.y = newAngles.y;
        }
        return currentVector;
    }
}

[System.Serializable]
public struct Struct_FireAngleLimit
{
    public Vector2 horizontalAngles;
    public Vector2 verticalAngles;
}

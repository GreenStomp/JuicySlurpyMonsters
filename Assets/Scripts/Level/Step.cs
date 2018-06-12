using System;
using UnityEngine;
using SOPRO;
/// <summary>
/// Class used to navigate correctly through the platform manager's platforms
/// </summary>
[Serializable]
public class Step
{
    [Serializable]
    public class Data
    {
        [NonSerialized]
        public Platform Plat;

        public float Percentage;

        public bool IsSwitchingLanes;
        public int CurrentLane;
        public float LaneLerpPercentage;
        public int DestinationLane;

        [NonSerialized]
        public Vector3 LocalPosition;
        [NonSerialized]
        public Vector3 LocalForward;
        [NonSerialized]
        public Vector3 LocalUp;

        public float ForwardPrecision;

        public Data()
        {
            ForwardPrecision = 0.00001f;
        }
    }

    public PlatformManager Manager;

    public SOVariableFloat TotalDistanceWalked;

    public SOVariableUint TotalPlatformsPassed;

    public SOVariableUint SpecialPlatformsPassed;
    /// <summary>
    /// Calculates next step data
    /// </summary>
    /// <param name="walker">obj that required step, used for SpecialPlatform callbacks</param>
    /// <param name="totalMovement">total amount of movement to do</param>
    /// <param name="data">data from which to start next step. Will be overwritten with updated values</param>
    /// <returns>true if current platform has changed</returns>
    public bool CalculateNextStep(Transform walker, float totalMovement, Data data)
    {
        if (!data.Plat)
            data.Plat = Manager.FirstPlatform;

        if (data.IsSwitchingLanes && data.CurrentLane == data.DestinationLane)
        {
            data.IsSwitchingLanes = false;
            data.LaneLerpPercentage = 0f;
        }

        if (data.IsSwitchingLanes)
            return SwitchingLaneNextStep(walker, totalMovement, data);
        else
            return NormalNextStep(walker, totalMovement, data);
    }
    /// <summary>
    /// Resets values of Step
    /// </summary>
    public void Reset(Data data, bool resetStepDataStructure = true, bool resetStats = true)
    {
        data.Plat = Manager.FirstPlatform;

        if (resetStepDataStructure)
        {
            data.CurrentLane = data.Plat.Lanes.Length / 2;
            data.DestinationLane = data.CurrentLane;
            Lane lane = data.Plat.Lanes[data.CurrentLane];
            data.LocalForward = lane.StartLocalDirection;
            data.IsSwitchingLanes = false;
            data.LaneLerpPercentage = 0f;
            data.Percentage = 0f;
            data.LocalPosition = lane.LocalCurve.Start;
        }

        if (resetStats)
        {
            if (TotalDistanceWalked)
                TotalDistanceWalked.Value = 0f;
            if (TotalPlatformsPassed)
                TotalPlatformsPassed.Value = 0;
            if (SpecialPlatformsPassed)
                SpecialPlatformsPassed.Value = 0;
        }
    }

    private bool NormalNextStep(Transform walker, float totalMovement, Data data)
    {
        Lane lane = data.Plat.Lanes[data.CurrentLane];

        if (!Mathf.Approximately(0.0125f, lane.LocalCurve.InverseLength))
            Application.Quit();

        //converto la speedScaled in percentuale rispetto la lunghezza della curva
        float movementPercentage = totalMovement * lane.LocalCurve.InverseLength;
        //calcolo la percentuale risultante dal movimento
        float newPercentage = data.Percentage + movementPercentage;

        //Se la percentuale supera 1f significa che ho superato la piattaforma corrente, per cui setto lo step alla fine della piattaforma e richiedo alla prossima piattaforma di calcolare lo step rimanente
        if (newPercentage > 1f)
        {
            //Richiamo evento Exit della special platform e update counters
            if (data.Plat.SpecialEffect != null)
            {
                if (SpecialPlatformsPassed)
                    SpecialPlatformsPassed.Value++;
                data.Plat.SpecialEffect.OnExit(walker, data.Percentage);
            }
            if (TotalPlatformsPassed)
                TotalPlatformsPassed.Value++;

            newPercentage -= 1f;
            data.Plat = data.Plat.Next;
            data.Percentage = 0f;

            float overMovement = newPercentage * lane.LocalCurve.Length;

            //Update distance counter
            if (TotalDistanceWalked)
                TotalDistanceWalked.Value += totalMovement - overMovement;

            //Richiamo evento Enter della special platform
            if (data.Plat.SpecialEffect != null)
                data.Plat.SpecialEffect.OnEnter(walker, newPercentage);

            CalculateNextStep(walker, overMovement, data);
            return true;
        }

        //Update distance counter
        if (TotalDistanceWalked)
            TotalDistanceWalked.Value += totalMovement;

        //calcolo la posizione finale
        Vector3 newCenter = lane.LocalCurve.GetPoint(newPercentage);

        //calcolo la tangente dato il Center appena calcolato e il Center dello step precedente
        Vector3 newTangent = (lane.LocalCurve.GetPoint(newPercentage + data.ForwardPrecision) - lane.LocalCurve.GetPoint(newPercentage - data.ForwardPrecision)).normalized;

        float prevPercentage = data.Percentage;

        //Setto i valori
        data.LocalPosition = newCenter;
        data.LocalForward = newTangent;
        data.Percentage = newPercentage;
        data.LocalUp = Vector3.Lerp(data.Plat.StartLocalUp, data.Plat.EndLocalUp, data.Percentage).normalized;

        //Richiamo evento StepTaken della special platform
        if (data.Plat.SpecialEffect != null)
            data.Plat.SpecialEffect.OnStepTaken(walker, data.Percentage, prevPercentage);

        return false;
    }
    private bool SwitchingLaneNextStep(Transform walker, float totalMovement, Data data)
    {
        Lane firstLane = data.Plat.Lanes[data.CurrentLane];
        Lane secondLane = data.Plat.Lanes[data.DestinationLane];

        //converto la speedScaled in percentuale rispetto la lunghezza della curva9
        float movementPercentage = totalMovement * Mathf.Lerp(firstLane.LocalCurve.InverseLength, secondLane.LocalCurve.InverseLength, data.LaneLerpPercentage);
        //calcolo la percentuale risultante dal movimento
        float newPercentage = data.Percentage + movementPercentage;

        //Se la percentuale supera 1f significa che ho superato la piattaforma corrente, per cui setto lo step alla fine della piattaforma e richiedo alla prossima piattaforma di calcolare lo step rimanente
        if (newPercentage > 1f)
        {
            //Richiamo evento Exit della special platform e update counters
            if (data.Plat.SpecialEffect != null)
            {
                if (SpecialPlatformsPassed)
                    SpecialPlatformsPassed.Value++;
                data.Plat.SpecialEffect.OnExit(walker, data.Percentage);
            }
            if (TotalPlatformsPassed)
                TotalPlatformsPassed.Value++;

            newPercentage -= 1f;
            data.Plat = data.Plat.Next;
            data.Percentage = 0f;

            float overMovement = newPercentage * Mathf.Lerp(firstLane.LocalCurve.Length, secondLane.LocalCurve.Length, data.LaneLerpPercentage); ;

            //Update distance counter
            if (TotalDistanceWalked)
                TotalDistanceWalked.Value += totalMovement - overMovement;

            //Richiamo evento Enter della special platform
            if (data.Plat.SpecialEffect != null)
                data.Plat.SpecialEffect.OnEnter(walker, newPercentage);

            CalculateNextStep(walker, overMovement, data);
            return true;
        }

        //Update distance counter
        if (TotalDistanceWalked)
            TotalDistanceWalked.Value += totalMovement;

        //calcolo la posizione finale
        Vector3 newCenter = Vector3.Lerp(firstLane.LocalCurve.GetPoint(newPercentage), secondLane.LocalCurve.GetPoint(newPercentage), data.LaneLerpPercentage); ;

        //calcolo la tangente dato il Center appena calcolato e il Center dello step precedente
        Vector3 first = Vector3.Lerp(firstLane.LocalCurve.GetPoint(newPercentage + data.ForwardPrecision), secondLane.LocalCurve.GetPoint(newPercentage + data.ForwardPrecision), data.LaneLerpPercentage);
        Vector3 second = Vector3.Lerp(firstLane.LocalCurve.GetPoint(newPercentage - data.ForwardPrecision), secondLane.LocalCurve.GetPoint(newPercentage - data.ForwardPrecision), data.LaneLerpPercentage);
        Vector3 newTangent = (first - second).normalized;

        float prevPercentage = data.Percentage;

        //Setto i valori
        data.LocalPosition = newCenter;
        data.LocalForward = newTangent;
        data.Percentage = newPercentage;
        data.LocalUp = Vector3.Lerp(data.Plat.StartLocalUp, data.Plat.EndLocalUp, data.Percentage).normalized;

        //Richiamo evento StepTaken della special platform
        if (data.Plat.SpecialEffect != null)
            data.Plat.SpecialEffect.OnStepTaken(walker, data.Percentage, prevPercentage);

        return false;
    }
}
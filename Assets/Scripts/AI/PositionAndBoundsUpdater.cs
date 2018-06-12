using UnityEngine;
using SOPRO;
public class PositionAndBoundsUpdater : MonoBehaviour
{
    [SerializeField]
    private SOVariableBounds boundsToUpdate;
    [SerializeField]
    private SOVariableVector3 positionToUpdate;
    [SerializeField]
    private Transform transformToUse;
    void OnEnable()
    {
        positionToUpdate.Value = transformToUse.position;
        boundsToUpdate.Value = new Bounds(transformToUse.position, transformToUse.localScale);
    }
    void Start()
    {
        positionToUpdate.Value = transformToUse.position;
        boundsToUpdate.Value = new Bounds(transformToUse.position, transformToUse.localScale);
    }
    void Update()
    {
        positionToUpdate.Value = transformToUse.position;
        boundsToUpdate.Value = new Bounds(transformToUse.position, transformToUse.localScale);
    }
}
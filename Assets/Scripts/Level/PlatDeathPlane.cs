using UnityEngine;
public class PlatDeathPlane : MonoBehaviour
{
    /// <summary>
    /// Plane created from this object's forward orientation and current position in World
    /// </summary>
    public Plane DeathPlane { get { return deathPlane; } }
    /// <summary>
    /// Point created from this object's back orientation. Represents a side of the DeathPlane
    /// </summary>
    public Vector3 DeathSidePoint { get { return deathSidePoint; } }

    protected Vector3 deathSidePoint;
    protected Plane deathPlane;
    /// <summary>
    /// Forces the DeathPlane and DeathSidePoint update
    /// </summary>
    public virtual void ForceUpdatePlane()
    {
        LateUpdate();
    }
    protected virtual void LateUpdate()
    {
        deathSidePoint = -this.transform.forward + this.transform.position;
        deathPlane = new Plane(this.transform.forward, this.transform.position);
    }
    protected virtual void Start()
    {
        LateUpdate();
    }
}
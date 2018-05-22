using UnityEngine;
public class PlatDeathPlane : MonoBehaviour
{
    /// <summary>
    /// Plane created from this object's forward orientation and current position in World
    /// </summary>
    public virtual Plane DeathPlane { get { return new Plane(this.transform.forward, this.transform.position); } }
    /// <summary>
    /// Point created from this object's back orientation. Represents a side of the DeathPlane
    /// </summary>
    public virtual Vector3 DeathSidePoint { get { return -this.transform.forward + this.transform.position; } }
}
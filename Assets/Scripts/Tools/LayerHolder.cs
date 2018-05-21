using UnityEngine;
[CreateAssetMenu(fileName = "Layer", menuName = "Utils/Layer")]
public class LayerHolder : ScriptableObject
{
    public ReferenceString LayerName;
    public int LayerIndex;
    void OnValidate()
    {
        LayerIndex = LayerMask.NameToLayer(LayerName.Value);
    }
    public static implicit operator int(LayerHolder layer)
    {
        return layer.LayerIndex;
    }
    public static implicit operator string(LayerHolder layer)
    {
        return layer.LayerName.Value;
    }
}
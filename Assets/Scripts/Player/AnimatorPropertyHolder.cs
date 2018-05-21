using UnityEngine;
[CreateAssetMenu(fileName = "AnimatorProperty", menuName = "Utils/AnimatorProperty")]
public class AnimatorPropertyHolder : ScriptableObject
{
    public ReferenceString PropertyName;
    public int PropertyHash;
    void OnValidate()
    {
        PropertyHash = Animator.StringToHash(PropertyName.Value);
    }
    public static implicit operator int(AnimatorPropertyHolder prop)
    {
        return prop.PropertyHash;
    }
    public static implicit operator string(AnimatorPropertyHolder prop)
    {
        return prop.PropertyName.Value;
    }
}
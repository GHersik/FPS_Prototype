using UnityEngine;

public abstract class BehaviourModule : ScriptableObject, IBehave
{
    public abstract void Behave(GameObject behavingObject);
}

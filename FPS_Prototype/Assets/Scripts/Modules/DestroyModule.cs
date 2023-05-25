using UnityEngine;

[CreateAssetMenu(fileName = "DestroyModule", menuName = "Game/Behaviour Modules/DestroyModule")]
public class DestroyModule : BehaviourModule
{
    [SerializeField] float delayToDestroyInSeconds = 1f;

    public override void Behave(GameObject behavingObject)
    {
        Destroy(behavingObject, delayToDestroyInSeconds);
    }
}

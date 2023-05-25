using UnityEngine;

[CreateAssetMenu(fileName = "ParticleModule", menuName = "Game/Behaviour Modules/ParticleModule")]
public class ParticleModule : BehaviourModule
{
    [SerializeField] GameObject particlePrefab;
    [SerializeField] Vector3 offsetPoint;
    [SerializeField] float timeToDestroy;

    public override void Behave(GameObject behavingObject)
    {
        if (particlePrefab != null)
        {
            Vector3 position = new Vector3(behavingObject.transform.position.x, behavingObject.transform.position.y + 1, behavingObject.transform.position.z);

            GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity);
            ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();

            particleSystem.Play();
            Destroy(particleEffect, 2f);
        }
    }
}

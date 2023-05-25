using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour, IDamageable
{
    [Header("Options")]
    [SerializeField] float durability = 100f;
    [SerializeField] MaterialType materialType;

    [Header("Behaviour Modules")]
    [SerializeField] List<BehaviourModule> moduleList = new List<BehaviourModule>();

    public void TakeDamage(float amount, MaterialType materialType)
    {
        if (materialType != this.materialType) return;

        durability -= amount;
        if (durability < 0) OnDestructionBehave();
    }

    private void OnDestructionBehave()
    {
        foreach (var module in moduleList)
            module.Behave(gameObject);
    }
}

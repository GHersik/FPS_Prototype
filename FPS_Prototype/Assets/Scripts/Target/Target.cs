using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //public delegate Particle[] ParticleDelegate();
    [Header("Options")]
    [SerializeField] float durability = 100f;
    [SerializeField] MaterialType materialType;

    [Header("Behaviour Modules")]
    [SerializeField] List<Module> moduleList = new List<Module>();

    public void TakeDamage(float amount, MaterialType materialType)
    {
        if (materialType != this.materialType) return;

        durability -= amount;
        if (durability < 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        foreach (var module in moduleList)
        {
            module.Behave();
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}

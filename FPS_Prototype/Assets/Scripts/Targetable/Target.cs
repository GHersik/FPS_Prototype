using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float durability = 100f;
    [SerializeField] MaterialType physicalMaterial;

    public void TakeDamage(float amount, MaterialType materialType)
    {
        if (materialType != physicalMaterial) return;

        durability -= amount;
        if (durability < 0) Destroy(gameObject);
    }
}

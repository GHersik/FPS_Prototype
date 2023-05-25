using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Module", menuName = "Game/Module")]
public class Module : ScriptableObject
{
    [SerializeField] ParticleSystem particleSystem;

    public void Behave()
    {

        if (particleSystem != null)
        {
            Debug.Log("Behave!");
            particleSystem.Play();
        }
    }
}

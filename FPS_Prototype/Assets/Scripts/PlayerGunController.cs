using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [Header("Control Options")]
    [SerializeField] float damage = 20f;
    [SerializeField] float range = 100f;
    [SerializeField] float impactForce = 10f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] MaterialType physicalMaterial;


    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] Camera playerCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource shootSoundSource;
    [SerializeField] AudioClip shootSoundClip;

    private float nextTimeToFire = 0f;

    private void Shoot()
    {
        if (Time.time < nextTimeToFire)
            return;

        nextTimeToFire = Time.time + 1 / fireRate;
        muzzleFlash.Play();
        shootSoundSource.PlayOneShot(shootSoundClip);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage, physicalMaterial);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject hitGameObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitGameObject, 2f);
        }
    }

    private void OnEnable()
    {
        inputManager.FireEvent += Shoot;
    }

    private void OnDisable()
    {
        inputManager.FireEvent -= Shoot;
    }
}

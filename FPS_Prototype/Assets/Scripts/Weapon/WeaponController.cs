using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Control Options")]
    [SerializeField] float damage = 20f;
    [SerializeField] float range = 100f;
    [SerializeField] float impactForce = 10f;
    [SerializeField] float fireRate = 15f;
    [SerializeField] MaterialType damageType;


    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioSource shootSoundSource;
    [SerializeField] AudioClip shootSoundClip;


    Transform originPoint;
    private float nextTimeToFire = 0f;


    public void SetOriginPoint(Transform origin) => originPoint = origin;

    private void Shoot()
    {
        if (Time.time < nextTimeToFire)
            return;

        nextTimeToFire = Time.time + 1 / fireRate;
        muzzleFlash.Play();
        shootSoundSource.PlayOneShot(shootSoundClip);

        RaycastHit hit;
        if (Physics.Raycast(originPoint.position, originPoint.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage, damageType);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            GameObject hitGameObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitGameObject, 2f);
        }
    }

    public void ShowWeapon(bool isVisible) => this.gameObject.SetActive(isVisible);

    private void OnEnable()
    {
        inputManager.FireEvent += Shoot;
    }

    private void OnDisable()
    {
        inputManager.FireEvent -= Shoot;
    }
}

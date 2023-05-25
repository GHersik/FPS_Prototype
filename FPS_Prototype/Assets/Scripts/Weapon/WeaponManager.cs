using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] List<WeaponController> startingWeapons = new List<WeaponController>();
    [SerializeField] float defaultFieldOfView = 60f;
    [SerializeField] float aimFieldOfView = 35f;
    [SerializeField] float aimSpeedTransition = 5f;
    [SerializeField] float switchSpeedTransition = 3f;
    [SerializeField] int holsterCapacity = 3;
    [SerializeField] float switchDelay = 0.3f;


    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] Camera weaponCamera;
    [SerializeField] Transform weaponUpPosition;
    [SerializeField] Transform weaponDownPosition;
    [SerializeField] Transform weaponFocusPosition;
    [SerializeField] Transform weaponContainer;


    WeaponController[] weaponSlots;
    int activeWeaponIndex = 0;

    bool isAiming = false;
    float currentFieldOfViev = 60f;
    Vector3 currentWeaponPosition;
    private float nextTimeToSwitch = 0f;

    private void Start()
    {
        weaponSlots = new WeaponController[holsterCapacity];

        for (int i = 0; i < Mathf.Clamp(startingWeapons.Count, 1, holsterCapacity); i++)
        {
            weaponSlots[i] = AddWeapon(startingWeapons[i]);
            weaponSlots[i].ShowWeapon(false);
        }

        weaponSlots[0].ShowWeapon(true);




        SetFieldOfView(defaultFieldOfView);
        currentWeaponPosition = weaponDownPosition.position;
    }

    private void Update()
    {
        UpdateAim();
    }

    private WeaponController AddWeapon(WeaponController weapon)
    {
        WeaponController weaponInstance = Instantiate(weapon, weaponContainer);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;
        weaponInstance.transform.localPosition = weaponUpPosition.localPosition;
        weaponInstance.SetOriginPoint(weaponCamera.transform);

        return weaponInstance;
    }

    public WeaponController GetActiveWeapon() => weaponSlots[activeWeaponIndex];

    private void SwitchWeapon(Vector2 arguments)
    {
        //Move to the weaponPositionDown, delay?
        if (Time.time < nextTimeToSwitch)
            return;

        nextTimeToSwitch = Time.time + switchDelay;



        weaponSlots[activeWeaponIndex].ShowWeapon(false);

        int sign = (int)Mathf.Sign(arguments.y);
        activeWeaponIndex += sign;

        if (activeWeaponIndex >= holsterCapacity)
            activeWeaponIndex = 0;
        if (activeWeaponIndex < 0)
            activeWeaponIndex = holsterCapacity - 1;

        weaponSlots[activeWeaponIndex].ShowWeapon(true);
        //Move to the weaponPositionUp?
    }

    private void AimingWeapon(bool aiming) => isAiming = aiming;

    private void UpdateAim()
    {
        if (isAiming)
        {
            currentFieldOfViev = Mathf.Lerp(currentFieldOfViev, aimFieldOfView, aimSpeedTransition * Time.deltaTime);
            SetFieldOfView(currentFieldOfViev);

            currentWeaponPosition = Vector3.Lerp(currentWeaponPosition, weaponFocusPosition.localPosition, switchSpeedTransition * Time.deltaTime);
            GetActiveWeapon().transform.localPosition = currentWeaponPosition;
        }
        else
        {
            currentFieldOfViev = Mathf.Lerp(currentFieldOfViev, defaultFieldOfView, aimSpeedTransition * Time.deltaTime);
            SetFieldOfView(currentFieldOfViev);

            currentWeaponPosition = Vector3.Lerp(currentWeaponPosition, weaponUpPosition.localPosition, switchSpeedTransition * Time.deltaTime);
            GetActiveWeapon().transform.localPosition = currentWeaponPosition;
        }
    }

    public void SetFieldOfView(float fov) => weaponCamera.fieldOfView = fov;

    private void OnEnable()
    {
        inputManager.SwitchWeaponEvent += SwitchWeapon;
        inputManager.AimWeaponEvent += AimingWeapon;
    }

    private void OnDisable()
    {
        inputManager.SwitchWeaponEvent -= SwitchWeapon;
        inputManager.AimWeaponEvent -= AimingWeapon;
    }
}

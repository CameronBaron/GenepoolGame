using UnityEngine;
using InControl;

public class WeaponManager : MonoBehaviour 
{
    public enum WeaponType
    {
        FlameThrower, // 0
        RocketLauncher, // 1
        AK47, // 2
        Shotgun // 3
    }
    public GameObject primary;
    public GameObject secondary;

    public GameObject aK47Prefab;
    public GameObject flameThrowerPrefab;
    public GameObject rocketLauncherPrefab;
    public GameObject shotgunPrefab;

	private GameObject aK47;
	private GameObject flameThrower;
	private GameObject rocketLauncher;
	private GameObject shotgun;

	public WeaponType currectWeapon;

    public GameObject WeaponSpawn;

	//Do touch my privates!
	private InputDevice device;

    // On start
    void Start()
	{
		aK47 = Instantiate(aK47Prefab, transform.position, transform.rotation) as GameObject;
		aK47.transform.parent = transform;
        aK47.transform.position = WeaponSpawn.transform.position;
		aK47.gameObject.SetActive(false);

        flameThrower = Instantiate(flameThrowerPrefab, transform.position, transform.rotation) as GameObject;
        flameThrower.transform.parent = transform;
        flameThrower.transform.position = WeaponSpawn.transform.position;
        flameThrower.gameObject.SetActive(false);

		rocketLauncher = Instantiate(rocketLauncherPrefab, transform.position, transform.rotation) as GameObject;
		rocketLauncher.transform.parent = transform;
        rocketLauncher.transform.position = WeaponSpawn.transform.position;
		rocketLauncher.gameObject.SetActive(false);

		shotgun = Instantiate(shotgunPrefab, transform.position, transform.rotation) as GameObject;
		shotgun.transform.parent = transform;
        shotgun.transform.position = WeaponSpawn.transform.position;
		shotgun.gameObject.SetActive(false);

        currectWeapon = WeaponType.FlameThrower;

		ChangeWeapon();
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (device.DPadLeft.IsPressed)
        {
            currectWeapon = WeaponType.FlameThrower;
            ChangeWeapon();
        }
        else if (device.DPadUp.IsPressed)
        {
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

            currectWeapon = WeaponType.AK47;
            ChangeWeapon();
        }
        else if (device.DPadRight.IsPressed)
		{
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

			currectWeapon = WeaponType.RocketLauncher;
            ChangeWeapon();
        }
        else if (device.DPadDown.IsPressed)
		{
			if (GetComponentInChildren<FlamethowerSounds>() != null)
				GetComponentInChildren<FlamethowerSounds>().EndSound();

			currectWeapon = WeaponType.Shotgun;
            ChangeWeapon();
        }
    }

    void ChangeWeapon()
    {
        flameThrower.gameObject.SetActive(false);
        aK47.gameObject.SetActive(false);
        rocketLauncher.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);

        switch (currectWeapon)
        {
            case WeaponType.FlameThrower:
                flameThrower.gameObject.SetActive(true);
                break;
            case WeaponType.AK47:
                aK47.gameObject.SetActive(true);
                break;
            case WeaponType.Shotgun:
                shotgun.gameObject.SetActive(true);
                break;
            case WeaponType.RocketLauncher:
                rocketLauncher.gameObject.SetActive(true);
                break;
        }
    }
}

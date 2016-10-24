using UnityEngine;
using InControl;

public class Gun : MonoBehaviour
{
    //Inspector Variables(Customizeable)
    public Transform spawn;
    public float fireRate;
    public float reloadSpeed;
    public float damage;
    public float multishot;
    public float projectileSpeed;
    public Rigidbody projectile;
    public float spread;
    public bool fullAuto;
    public bool delay;

    [HideInInspector] public bool shooting = false;


    //Set Variables(Dont touch my privates)
    private float timer;
    private bool semiLock;
    private float fireDelay;
	private PlayerController player;
    private InputDevice device;

    // Use this for initialization
    void Start ()
    {
        if (multishot == 0)
            multishot = 1;
        semiLock = false;
        fireDelay = (1.0f / fireRate);
        timer = fireDelay;
		player = GetComponentInParent<PlayerController>();
        device = player.Device;
    }
	
	// Update is called once per frame
	void Update ()
    {
        shooting = false;
        if (device.RightTrigger.IsPressed && !player.disableControls)
        {
            if (fullAuto)
            {
                AutoTrigger();
            }
            else
            {
                SemiTrigger();
            }
        }

        //RIP Variables
        if (!device.RightTrigger.IsPressed)
        {
            semiLock = false;
        }

        timer += Time.deltaTime;
	}

    void SemiTrigger()
    {
        if (delay == true)
        {
            if (timer > fireDelay)
            {
                if (semiLock == false)
                {                
                    Shoot();
                    semiLock = true;
                    timer = 0;
                }
            }
        }
        else
        {
            if (semiLock == false)
            {
                Shoot();
                semiLock = true;
            }
        }
    }

    void AutoTrigger()
    {
        if (delay == true)
        {
            if (timer > fireDelay)
            {
                Shoot();
                timer = 0;
            }
        }
        else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for (int i = 0; i < multishot; i++)
		{ 
            Rigidbody proj = Instantiate(projectile, spawn.position, Quaternion.Euler(90, transform.eulerAngles.y, 0)) as Rigidbody;
            proj.velocity = transform.TransformDirection(new Vector3(Random.Range(-spread, spread), Random.Range(-1, 1), projectileSpeed));

            Physics.IgnoreLayerCollision(8, 8);
			proj.GetComponent<BulletDamage>().damage = damage;
            proj.GetComponent<BulletDamage>().shooter = gameObject.GetComponentInParent<PlayerController>();

            shooting = true;
			//GetComponentInParent<Player>().stats.bulletsFired += 1;
        }
    }
}

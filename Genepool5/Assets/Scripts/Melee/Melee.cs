using UnityEngine;
using InControl;

public class Melee : MonoBehaviour 
{
    public float meleeRange = 15;
    public int damage = 34;
    private InputDevice device;
    bool semiLock = false;

    //Don't touch my privates!

    // On start
    void Start()
	{
        device = GetComponentInParent<PlayerController>().Device;
		if (device == null)
			Destroy(this);
    }

    void OnCollisionStay(Collision col)
    {
        if (device.RightBumper.WasPressed)
        {
            if (semiLock == false)
            {
                if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
                {
                    //Melee Stuff
                    col.gameObject.GetComponent<Health>().AdjustCurrentHP(-damage);
					Debug.Log("Melee");
                }
            }
            semiLock = true;
        }

        if (!device.RightBumper.IsPressed)
        {
            semiLock = false;
        }
    }
}

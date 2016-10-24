using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;	
    public int currentHP = 100;
	public float hurtForce = 10f;                   // The force with which the player is pushed when hurt.

	private bool killable = true;

	/// <summary>
	/// Pass in an int to adjust this current health value.
	/// </summary>
	/// <param name="adjustment">Amount to adjust health by; + for healing, - for damage.</param>
    public void AdjustCurrentHP(int adjustment)
    {
        currentHP += adjustment;                    // Modify current health by the adjustment amount.

		if (currentHP > 1)
		{
			killable = true;
		}

		if (currentHP > maxHP)                      // Check current Health value.         
			currentHP = maxHP;                      // If currentHP is more than maxHP, set it back to maxHP.								 
		else if (currentHP < 1)                     // If currentHp goes below zero.	
		{
			currentHP = 0;                          // Set it back to zero, as to not have a negative.
			if (!gameObject.CompareTag("Player"))
			{
				Destroy(gameObject);
			}
			else
			{
				gameObject.GetComponent<PlayerDeath>().DieAndRespawn();
				gameObject.GetComponent<Collider>().enabled = false;
				//gameObject.GetComponentInParent<PlayerInstantiate>().Respawn(gameObject.GetComponent<Player>());	
				//gameObject.GetComponent<Player>().stats.deaths++;				
			}
		}
		
		if (maxHP < 1)								// Make sure maxHp cannot be less than 1.
			maxHP = 1;
    }

	void OnCollisionEnter(Collision col)
	{
		// If the colliding gameObject is a bullet
		if (col.gameObject.CompareTag("Bullet"))
		{
			float dmg = col.gameObject.GetComponent<BulletDamage>().damage * -1;
			AdjustCurrentHP((int)dmg);

			if (gameObject.CompareTag("Player") && currentHP < 1 && killable)
			{
				col.gameObject.GetComponent<BulletDamage>().shooter.stats.score += gameObject.GetComponent<PlayerController>().scoreValue;
				gameObject.GetComponent<PlayerController>().stats.deaths += 1;
                killable = false;
				return;
			}		
		}
		else if (gameObject.CompareTag("Enemy") && currentHP < 1)
		{
			DestroyObject(gameObject);
		}

		if (col.gameObject.CompareTag("Enemy"))
		{
			AdjustCurrentHP(-5);
		}
	}
}

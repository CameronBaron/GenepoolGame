using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Attributes
{
    public float speedMultiplier;
    public float healthMultiplier;
    public float meleeMultiplier;
}

public class BasePlayer : Player
{
    //Public Variables
    public Attributes sharkAttributes;
    public Attributes rhinoAttributes;
    public Attributes tigerAttributes;
    public Attributes chickenAttributes;
    public Attributes alienAttributes;

    public PLAYERTYPE playerType;

    public enum PLAYERTYPE
    {
        SHARK = 0,
        CHICKEN = 1,
        RHINO = 2,
        TIGER = 3,
        ALIEN = 4,
    }

    void Start()
    {
        if (playerType == PLAYERTYPE.SHARK)
        {
            gameObject.GetComponent<Health>().maxHP         = (int)(gameObject.GetComponent<Health>().maxHP * sharkAttributes.healthMultiplier);
            gameObject.GetComponent<Health>().currentHP     = (int)(gameObject.GetComponent<Health>().maxHP * sharkAttributes.healthMultiplier);
            speed                                           = speed * sharkAttributes.speedMultiplier;
            meleeDamage                                     = meleeDamage * sharkAttributes.meleeMultiplier;
        }
        else if (playerType == PLAYERTYPE.RHINO)
        {
            gameObject.GetComponent<Health>().maxHP         = (int)(gameObject.GetComponent<Health>().maxHP * rhinoAttributes.healthMultiplier);
            gameObject.GetComponent<Health>().currentHP     = (int)(gameObject.GetComponent<Health>().maxHP * rhinoAttributes.healthMultiplier);
            speed                                           = speed * rhinoAttributes.speedMultiplier;
            meleeDamage                                     = meleeDamage * rhinoAttributes.meleeMultiplier;
        }
        else if (playerType == PLAYERTYPE.TIGER)
        {
            gameObject.GetComponent<Health>().maxHP         = (int)(gameObject.GetComponent<Health>().maxHP * tigerAttributes.healthMultiplier);
            gameObject.GetComponent<Health>().currentHP     = (int)(gameObject.GetComponent<Health>().maxHP * tigerAttributes.healthMultiplier);
            speed                                           = speed * tigerAttributes.speedMultiplier;
            meleeDamage                                     = meleeDamage * tigerAttributes.meleeMultiplier;
        }
        else if (playerType == PLAYERTYPE.CHICKEN)
        {
            gameObject.GetComponent<Health>().maxHP         = (int)(gameObject.GetComponent<Health>().maxHP * chickenAttributes.healthMultiplier);
            gameObject.GetComponent<Health>().currentHP     = (int)(gameObject.GetComponent<Health>().maxHP * chickenAttributes.healthMultiplier);
            speed                                           = speed * chickenAttributes.speedMultiplier;
            meleeDamage                                     = meleeDamage * chickenAttributes.meleeMultiplier;
        }
        else if (playerType == PLAYERTYPE.ALIEN)
        {
            gameObject.GetComponent<Health>().maxHP         = (int)(gameObject.GetComponent<Health>().maxHP * alienAttributes.healthMultiplier);
            gameObject.GetComponent<Health>().currentHP     = (int)(gameObject.GetComponent<Health>().maxHP * alienAttributes.healthMultiplier);
            speed                                           = speed * alienAttributes.speedMultiplier;
            meleeDamage                                     = meleeDamage * alienAttributes.meleeMultiplier;
        }
    }
}

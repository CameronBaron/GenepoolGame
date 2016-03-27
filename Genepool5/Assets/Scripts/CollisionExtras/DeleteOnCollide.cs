using UnityEngine;
using System.Collections;

public class DeleteOnCollide : MonoBehaviour 
{
	//Don't touch my privates!
    
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

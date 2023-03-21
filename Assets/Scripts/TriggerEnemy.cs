using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if( other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
       {
            //Debug.Log("trigger Enemy");
            if(other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().isTargeted = true;
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Debug.Log("bo trigger Enemy");
            if (other.GetComponent<EnemyController>() != null)
            {
                other.GetComponent<EnemyController>().isTargeted = false;
            }
            
        }
    }


}

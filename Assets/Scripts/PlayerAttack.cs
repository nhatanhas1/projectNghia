using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] List<GameObject> targetList;

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
        //Debug.Log("Have enemy in attack range");
        if (other.GetComponent<IDamageable>() != null)
        {
            Debug.Log("Have enemy in attack range");
            targetList.Add(other.gameObject);
            //other.GetComponent<IDamageable>().TakeDamage(5);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other.gameObject);
    }

    public void AttackInRadius()
    {
        StartCoroutine(AttackAllTager());
    }


    IEnumerator AttackAllTager()
    {
        foreach (GameObject target in targetList)
        {
            if (!target.IsDestroyed())
            {
                Debug.Log("atattack  " + target.name);
                target.GetComponent<IDamageable>().TakeDamage(5);
            }
            
        }
        yield return new WaitForSecondsRealtime(1);        
    }
}

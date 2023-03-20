using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] List<GameObject> targetList;
    [SerializeField] List<GameObject> consumeItemList;

    [SerializeField] Neko2 player;

    private void Awake()
    {
        player = GetComponentInParent<Neko2>();
    }
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

        if(other.GetComponent<ItemController>() != null)
        {
            Debug.Log("Have Item in attack range");
            consumeItemList.Add(other.gameObject);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            targetList.Remove(other.gameObject);
        }
        if (other.GetComponent<ItemController>() != null)
        {
            consumeItemList.Remove(other.gameObject);
        }
    }

    public void AttackInRadius()
    {
        StartCoroutine(AttackAllTager());
        StartCoroutine(ConsumeItem());
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

    IEnumerator ConsumeItem()
    {
        foreach (GameObject item in consumeItemList)
        {
            if (!item.IsDestroyed())
            {
                Debug.Log("eat Item  " + item.name);
                item.GetComponent<ItemController>().EatItem(player);
            }
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<ItemData> itemDatas = new List<ItemData>();
    public ItemData itemData;
    float healPoint;

    void Start()
    {
        healPoint = itemData.healPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Neko2>() != null)
        {
            other.GetComponent<Neko2>().hp += healPoint;
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] Sprite itemSprite;
    public enum ItemStype
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,          
    }

    public ItemStype itemStype;

    public float healPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

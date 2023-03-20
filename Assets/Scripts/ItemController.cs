using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<ItemData> itemDatas = new List<ItemData>();
    public ItemData itemData;
    float healPoint;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        healPoint = itemData.healPoint;
        spriteRenderer.sprite = itemData.itemSprite;
    }

    public void EatItem(Neko2 player)
    {
        player.hp += healPoint;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<Neko2>() != null)
        //{
        //    other.GetComponent<Neko2>().hp += healPoint;
        //}
        
    }

}

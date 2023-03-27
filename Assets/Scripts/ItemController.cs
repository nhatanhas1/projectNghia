using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<ItemData> itemDatas;
    public ItemData itemData;
    float healPoint;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            player.sfx.PlayEat();
        player.hp = Mathf.Clamp(player.hp, 0, player.maxHP);
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

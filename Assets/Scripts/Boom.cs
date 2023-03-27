using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject explodeParticle;
    SphereCollider sphereCollider;
    [SerializeField] float boomDamage = 20;
    [SerializeField] float boomRadius = 2;

    [SerializeField] float timeActive =2;
    [SerializeField] Neko2 player;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = boomRadius;
        timeActive = 3;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveBoom();

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Neko2>() != null)
        {
            
            if (timeActive <= 0 && isActive == false)
            {
                isActive = true;
                other.GetComponent<Neko2>().TakeDamage(boomDamage);
            }


            //player = other.GetComponent<Neko2>();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Neko2>() != null)
    //    {            
    //        player = other.GetComponent<Neko2>();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<Neko2>() != null)
    //    {
    //        player = null;
    //    }
    //}



    void ActiveBoom()
    {
        //Debug.Log("thoi gian no boom" + timeActive);
        timeActive -= Time.deltaTime;
        if(timeActive <= 0)
        {
            if(player != null)
            {
                player.TakeDamage(boomDamage);
            }
            Instantiate(explodeParticle,transform.position,Quaternion.identity);
            StartCoroutine(DestroyBoom());
        }
    }

    IEnumerator DestroyBoom()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}

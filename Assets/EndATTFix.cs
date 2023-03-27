using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndATTFix : MonoBehaviour
{
    public GameObject Neko;
    public void EndAT()
    {
        Neko.GetComponent<Neko2>().EndAT();
    }

}

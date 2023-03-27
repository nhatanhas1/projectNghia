using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Neko2 neko;
    // Start is called before the first frame update
    public Image healthBar;
    public float healthAmount;
    public Image staminaBar;
    public float staminaAmount;

    private void Awake()
    {
        neko = FindAnyObjectByType<Neko2>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (neko != null)
        {
            healthAmount = neko.hp; // dùng để lấy thông tin về máu của Neko
            staminaAmount = neko.stamina;//dùng để lấy thông tin về stamina của Neko
            healthBar.fillAmount = healthAmount / 100f; 
            //health bar và stamina bar là 2 cái hình (Image)
            //Để Image dạng Filled  (Image type = filled)
            //sau khi đổi dạng image thành filled
            //Trong cái Image có thành phần là Fill Amount có giá trị tối thiểu là 0 và tối đa là 1
            // thể hiện giá trị đã được fill trong Image đó.
            // Vì Neko có 100 hp và 100 stamina, cho nên để mỗi 1% của Fill Amount tương ứng với 
            // lượng HP và Stamina của Neko ta dùng cách chia lượng máu và stamina thành 100 phần. 
            staminaBar.fillAmount = staminaAmount / 100f;
        }
        
    }
}

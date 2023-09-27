using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Image healthIamage;
    public Image healthDelayIamage;
    public Image powerIaamage;


    private void Update()
    {
        if (healthDelayIamage.fillAmount > healthIamage.fillAmount)
        {
            healthDelayIamage.fillAmount -= Time.deltaTime;
        }
    }

    // 调整绿色血条
    public void OnHealthChange(float persentage)
    {
        healthIamage.fillAmount = persentage;
    }
}

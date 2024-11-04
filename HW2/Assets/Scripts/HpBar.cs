using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    private Image sprited;

    private Camera mainCamera;
    private PlayerHealth HealthStatus;
    void Start()
    {
        GameObject hpObject = GameObject.Find("/Player/HPbar/Background/HP");
        if (hpObject != null)
        {
            sprited = hpObject.GetComponent<Image>();
        }

        mainCamera = Camera.main;
        HealthStatus = FindObjectOfType<PlayerHealth>();
        UpdateHP(HealthStatus.GetHP(), HealthStatus.GetMaxHP());
    }

    public void UpdateHP(float curHP, float totalHP)
    {
        sprited.fillAmount = curHP / totalHP;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}

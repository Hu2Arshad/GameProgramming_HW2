using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    private Image sprited;
    private Transform hpTransform;
    private Camera mainCamera;
    private PlayerHealth HealthStatus;
    void Start()
    {
        hpTransform = transform.Find("Background/HP");
        GameObject hpObject = hpTransform.gameObject;
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

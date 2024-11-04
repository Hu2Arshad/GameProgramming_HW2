using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHPBar : MonoBehaviour
{
    private Image sprited;
    private Transform hpTransform;
    private Camera mainCamera;
    private Enemy enemyStatus;
    void Start()
    {
        hpTransform = transform.Find("Background/HP");
        GameObject hpObject = hpTransform.gameObject;
        if (hpObject != null)
        {
            sprited = hpObject.GetComponent<Image>();
        }

        mainCamera = Camera.main;
        enemyStatus = FindObjectOfType<Enemy>();
        UpdateHP(enemyStatus.GetHP(), enemyStatus.GetMaxHP());
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

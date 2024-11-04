using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    [SerializeField] private Image sprited;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
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

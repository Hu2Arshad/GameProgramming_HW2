using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerHealth obj;

    [SerializeField]
    private int totalHP = 100;
    [SerializeField]
    private int curHP = 100;

    [SerializeField]
    private HpBar HpSprite;
    void Awake()
    {
        if(!obj)
        {
            obj = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        HpSprite.UpdateHP(curHP, totalHP);
    }

    public int GetHP()
    {
        return curHP;
    }

    public int GetMaxHP()
    {
        return totalHP;
    }

    public void Damaged(int damage)
    {
        curHP -= damage;
        curHP = Mathf.Max(curHP, 0);
        HpSprite.UpdateHP(curHP, totalHP);
    }

    public void Healed(int heal)
    {
        curHP += heal;
        curHP = Mathf.Min(curHP, totalHP);
        HpSprite.UpdateHP(curHP, totalHP);
    }
}

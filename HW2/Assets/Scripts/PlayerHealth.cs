using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager manager;
    private int totalHP = 100;
    private int curHP = 100;

    private HpBar HpSprite;

    void Start()
    {   
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (manager != null )
        {
            curHP = manager.curHP; 
            totalHP = manager.totalHP;
        }
        else
        {
            curHP = 100;
            totalHP = 100;
            Debug.Log("PlayerHealth Unable to find manager");
        }
        HpSprite = transform.Find("HPbar").GetComponent<HpBar>();
        if (HpSprite == null ) { Debug.Log("Unable to find Player HP Bar"); }
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

    private void OnDestroy()
    {
        manager.totalHP = totalHP;
        manager.curHP = curHP;
    }
}

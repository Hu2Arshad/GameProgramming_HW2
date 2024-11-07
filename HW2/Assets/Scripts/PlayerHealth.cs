using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager manager;
    private ParticleManager particleManager;
    private int totalHP = 100;
    private int curHP = 100;

    private GameObject Player;
    private DeathScreen deathScreenUI;
    private HpBar HpSprite;

    void Start()
    {   
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleManager>();
        Player = GameObject.Find("Player");

        //Retrieve hp value from previous scene (if exist) throug GameManager
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
        //HpSprite.UpdateHP(curHP, totalHP); //Update Player HP Bar sprite

        deathScreenUI = GameObject.Find("Death_Container").GetComponent<DeathScreen>();
        if (deathScreenUI == null) { Debug.Log("Unable to find DeathScreenUI"); }

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
        if (particleManager != null)
        {   //Player transform pos is on the feet, increase y to suitable position.
            Vector3 HitEffectPos = new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z);
            particleManager.HitEffect(HitEffectPos, transform); // When player is damaged, create particle
        }
        else {
            Debug.Log("PlayerHealth Unable to find ParticleManager");
        }

        curHP -= damage;
        curHP = Mathf.Max(curHP, 0);
        HpSprite.UpdateHP(curHP, totalHP);

        if (curHP <= 0)
        {
            OnPlayerDeath();
        }
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

    private void OnPlayerDeath()
    {
        // Trigger the death screen
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
             Destroy(enemy);
        }

         Destroy(Player);
        
        if (deathScreenUI != null)
        {
            deathScreenUI.ShowDeathScreen();
        }
    }
}

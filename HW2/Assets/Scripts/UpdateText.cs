using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateText : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUI;
    // Start is called before the first frame update
    private CheckFinished checker;
    void Start()
    {
        textMeshProUI = GetComponent<TextMeshProUGUI>();
        if(textMeshProUI == null)
        {
            Debug.Log("cant find textMeshProUI");
        }

        checker = FindObjectOfType<CheckFinished>();
        if(checker == null)
        {
            Debug.Log("cant find checkfinished");
        }

        ChangeText(checker.DefeatedEnemies(), checker.KillObj());
    }

    public void ChangeText(int curDefeated, int totalDefeated)
    {
        if(curDefeated >= totalDefeated)
        {
            textMeshProUI.text = "Objective: \n Go to the portal!";
        }
        else
        {
            textMeshProUI.text = string.Format("Objective: \n Kill {0}/{1} Enemies", curDefeated, totalDefeated);
        }
    }

    public void DisableText()
    {
        textMeshProUI.text = "";
    }
}

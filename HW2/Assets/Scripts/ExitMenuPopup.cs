using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuPopup : MonoBehaviour
{
    Canvas popup;
    // Start is called before the first frame update
    void Start()
    {
        popup = GameObject.Find("Exit Menu").GetComponent<Canvas>();
        popup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenExitMenu()
    {
        popup.enabled = true;
    }

    public void CloseExitMenu()
    {
        popup.enabled = false;
    }

}

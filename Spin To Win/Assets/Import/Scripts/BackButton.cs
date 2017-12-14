using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

    public GameObject openMenu;
    public GameObject closeMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openMenu.SetActive(true);
            closeMenu.SetActive(false);
        }
    }
}

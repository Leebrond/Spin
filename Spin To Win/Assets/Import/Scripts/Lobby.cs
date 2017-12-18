using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

    public Text txtPlayername;

    public Text txtCoin;

    public GameObject menuAddCoin;

    public GameObject menuLogin;

    public GameObject menuSetting;

	void Start () {

        txtPlayername.text = PlayerManager.instance.playerName;
        txtCoin.text = PlayerManager.instance.amountCoin.ToString();
    }


    public void OpenSetting()
    {
        menuSetting.SetActive(false);
    }

  


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject type in GameObject.FindGameObjectsWithTag("Type"))
            {
                Destroy(type);
            }
            //Debug.Break();
            menuLogin.SetActive(true);
            FindObjectOfType<WWWGameType>().StartCoroutine("WaitData");
            //StartCoroutine(FindObjectOfType<WWWGameType>().WaitData());
            PlayerManager.instance.isLogin = false;
            this.gameObject.SetActive(false);


        }
    }
}

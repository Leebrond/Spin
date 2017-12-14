using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

    public Text txtPlayername;

    public Text txtCoin;

    public GameObject menuAddCoin;

    public GameObject menuLogin;

	void Start () {

        txtPlayername.text = PlayerManager.instance.playerName;
        txtCoin.text = PlayerManager.instance.amountCoin.ToString();
        StartCoroutine(DB.instance.UpdateBalance(-5, 20));
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
            // StartCoroutine(menuLogin.GetComponent<WWWGameType>().WaitData());
            menuLogin.GetComponent<WWWGameType>().StartCoroutine(menuLogin.GetComponent<WWWGameType>().WaitData());
            PlayerManager.instance.isLogin = false;
            this.gameObject.SetActive(false);


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WWWLogin : MonoBehaviour {

    public GameObject panelLogin;

    public GameObject panelLobby;

    public InputField inputUsername;

    public InputField inputPassword;

    public Text txtInfo;

    
    void Start()
    {
        txtInfo.text = "Welcome";
        if (PlayerManager.instance.isLogin)
        {
            panelLogin.SetActive(false);
            panelLobby.SetActive(true);
        } else
        {
            panelLogin.SetActive(true);
            panelLobby.SetActive(false);
        }
    }
    

    public void Login()
    {
        if (string.IsNullOrEmpty(inputUsername.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            txtInfo.text = "Input username and password ";
          // LoginWithoutPassword();
           return;
        }

        StartCoroutine(GetPlayerData());
    }


    private void LoginWithoutPassword()
    {
        PlayerManager.instance.amountCoin = 1000;
        PlayerManager.instance.playerName = "anonymous";
        panelLobby.SetActive(true);
        panelLogin.SetActive(false);
    }


    private IEnumerator GetPlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", inputUsername.text);
        form.AddField("passwordPost", inputPassword.text);

        WWW www = new WWW(DB.instance.URL + "user.php", form);

        yield return www;

        Debug.Log(www.text);

        string[] temp;

        temp = www.text.Split(';');

        if (temp[0] == "\nLogin success ")
        {
            
            GetComponent<WWWGameType>().panelLoading.SetActive(true);
            panelLobby.SetActive(true);
            PlayerManager.instance.amountCoin = int.Parse(temp[1]);
            PlayerManager.instance.playerName = temp[2];
            PlayerManager.instance.isLogin = true;
            FindObjectOfType<Lobby>().SetPlayerProfile();
            //panelLogin.SetActive(false);
            txtInfo.text = "Welcome";
            

        } else
        {
            txtInfo.text = www.text;
        }
    }
    
}

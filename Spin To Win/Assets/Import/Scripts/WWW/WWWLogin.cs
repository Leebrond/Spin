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
       /* if (PlayerManager.instance.isLogin)
        {
            panelLogin.SetActive(false);
            panelLobby.SetActive(true);
            GameObject panelLoading = GameObject.Find("Loading");
            panelLoading.transform.GetChild(0).GetComponent<Image>().enabled = false;
            panelLoading.transform.GetChild(1).GetComponent<Text>().enabled = false;
        } */
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
           
            yield return StartCoroutine(GetPlayerDaily(inputUsername.text));

            panelLobby.SetActive(true);
            PlayerManager.instance.amountCoin = int.Parse(temp[1]);
            PlayerManager.instance.playerName = temp[2];
            PlayerManager.instance.isLogin = true;
            //panelLogin.SetActive(false);

        }
    }


    IEnumerator GetPlayerDaily(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);

        WWW www = new WWW(DB.instance.URL + "daily.php", form);

        yield return www;

        Debug.Log(www.text);
    }
}

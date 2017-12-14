using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameType : MonoBehaviour {

    public int row;

    private Button btnPlay;

    private GameObject menuLoading;


    void Start () {
        btnPlay = transform.Find("Button").GetComponent<Button>();
        btnPlay.onClick.AddListener(PlayGameType);
        menuLoading = GameObject.Find("Loading");
	}


    public void PlayGameType()
    {
        menuLoading.GetComponent<Image>().enabled = true;
        menuLoading.transform.GetChild(0).GetComponent<Image>().enabled = true;
        menuLoading.transform.GetChild(1).GetComponent<Text>().enabled = true;
        StartCoroutine(WaitGetConfig());
    }


    IEnumerator WaitGetConfig()
    {
        StartCoroutine(GetConfig(false));
        yield return StartCoroutine(GetConfig(true));
        SceneManager.LoadScene(1);
    }


    IEnumerator GetConfig(bool isPic)
    {
        WWWForm form = new WWWForm();
        form.AddField("rowPost", row);
        form.AddField("picPost", isPic.ToString());

        WWW www = new WWW(DB.instance.URL + "config.php",form);

        yield return www;

        Debug.Log(www.text);

        if (isPic)
        {
            GameConfig.instance.SetPic(www);
        } else
        {
            GameConfig.instance.SetConfig(www);
        }
    }
}

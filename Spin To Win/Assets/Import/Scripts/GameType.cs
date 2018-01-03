using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameType : MonoBehaviour
{
    public int nowheel;

    public int idwheel;

    private Button btnPlay;


    

    void Start()
    {
        btnPlay = transform.Find("btnPlay").GetComponent<Button>();
        btnPlay.onClick.AddListener(PlayGameType);
    }


    public void PlayGameType()
    {
        GameConfig.instance.noWheel = nowheel;
        FindObjectOfType<WWWGameType>().panelLoading.SetActive(true);
        StartCoroutine(WaitGetConfig());
    }


    IEnumerator WaitGetConfig()
    {
        yield return StartCoroutine(GetConfig());
        SceneManager.LoadScene(1);
    }


    IEnumerator GetConfig()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", idwheel);

        WWW www = new WWW(DB.instance.URL + "config.php", form);

        yield return www;

        Debug.Log(www.text);

        GameConfig.instance.SetConfig(www);
    }
    
}

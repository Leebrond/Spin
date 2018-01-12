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

    public Button btnSound;

    [SerializeField]
    private AudioSource audioBG;

    [SerializeField]
    private Sprite soundON;

    [SerializeField]
    private Sprite soundOFF;


	void Start () {
        SetPlayerProfile();
        btnSound.onClick.AddListener(SetSound);
    }

    public void SetPlayerProfile()
    {
        txtPlayername.text = PlayerManager.instance.playerName;
        txtCoin.text = PlayerManager.instance.amountCoin.ToString();
        audioBG.enabled = PlayerManager.instance.toggleSound;
    }

    public void ShowMenuAvatar()
    {

    }


    public void SetSound()
    {
        if (PlayerManager.instance.toggleSound)
        {
            audioBG.enabled = false;
            btnSound.GetComponent<Image>().sprite = soundOFF;
            PlayerManager.instance.toggleSound = false;

        } else
        {
            audioBG.enabled = true;
            btnSound.GetComponent<Image>().sprite = soundON;
            PlayerManager.instance.toggleSound = true;
        }
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
            //46.166.160.159/~tigertoto/games/Spin/PHP//PHP/
            menuLogin.SetActive(true);
            FindObjectOfType<WWWGameType>().StartCoroutine("WaitData");
            //StartCoroutine(FindObjectOfType<WWWGameType>().WaitData());
            PlayerManager.instance.isLogin = false;
            this.gameObject.SetActive(false);


        }
    }
}

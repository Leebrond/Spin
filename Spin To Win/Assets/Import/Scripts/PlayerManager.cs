using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public string playerName;

    public bool isLogin;

    public int amountCoin;

    public bool toggleSound;

    void Awake()
    {
       if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if( instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Internet not reachable");
        }
    }
}

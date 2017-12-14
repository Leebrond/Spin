﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour {

    public static GameConfig instance;
    
    public Sprite spWheel;

    public int countPrizes;

    public int degreePrize;

    public int[] timesPrize;

    public int[] optionBet;
    

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void SetConfig(WWW data)
    {
        string[] temp = data.text.Split(';');

        countPrizes = int.Parse(temp[0]);
        degreePrize = 360 / countPrizes;

        //setPrize
        string[] tempPrize = temp[1].Split(',');

        timesPrize = new int[tempPrize.Length];

        for (int i = 0; i < tempPrize.Length; i++)
        {
            timesPrize[i] = int.Parse(tempPrize[i]);
        }

        //setBet
        string[] tempBet = temp[2].Split(',');

        optionBet = new int[tempBet.Length];

        for (int i = 0; i < tempBet.Length; i++)
        {
            optionBet[i] = int.Parse(tempBet[i]);
        }
    }


    public void SetPic(WWW data)
    {
        Texture2D texture = new Texture2D(5, 5);
        data.LoadImageIntoTexture(texture);
        spWheel = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
    }
}

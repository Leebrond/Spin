using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPlay : MonoBehaviour {

    [Header("Buttons")]
    public Button btnSpin;

    public Button btnIncBet;

    public Button btnDecBet;


    [Space]
    public Text txtamountBet;

    public Text txtamountCoin;

    public int chosenBet;

    public Slider sliderSpeed;

    [SerializeField]
    private Transform tfLevel;

    private int index;
    

	// Use this for initialization
	void Start () {
        index = 0;
        chosenBet = GameConfig.instance.optionBet[index];
        txtamountBet.text = chosenBet.ToString();
        txtamountCoin.text = PlayerManager.instance.amountCoin.ToString();

        btnSpin.onClick.AddListener(Spin);
        btnDecBet.onClick.AddListener(DecreaseBet);
        btnIncBet.onClick.AddListener(IncreaseBet);
	}
    

    public void Spin()
    {
        Wheel.instance.SpinWheel();
    }


    public void IncreaseBet()
    {
        if(index>=0 && index < GameConfig.instance.optionBet.Length - 1)
        {
            index++;
            chosenBet = GameConfig.instance.optionBet[index];
            txtamountBet.text = chosenBet.ToString();
        }
    }


    public void DecreaseBet()
    {
        if (index > 0 && index < GameConfig.instance.optionBet.Length)
        {
            index--;
            chosenBet = GameConfig.instance.optionBet[index];
            txtamountBet.text = chosenBet.ToString();
        }
    }


    public void DisableButton()
    {
        btnSpin.interactable = false;
        btnIncBet.interactable = false;
        btnDecBet.interactable = false;
        sliderSpeed.interactable = false;
    }

    public void EnableButton()
    {
        btnSpin.interactable = true;
        btnIncBet.interactable = true;
        btnDecBet.interactable = true;
        sliderSpeed.interactable = true;
    }

    public void BackLobby()
    {
        SceneManager.LoadScene(0);
    }
    

    public void SetSliderValue(float sliderValue)
    {
        Wheel.instance.maxLerpRotationTime = sliderValue;

        float limit = sliderValue / (float)(Wheel.instance.checkSpeed.Length + 1);

        Wheel.instance.checkSpeed[0] = limit + (limit / 2f);
        Wheel.instance.checkSpeed[1] = (limit*2f) + (limit / 3f);
        Wheel.instance.checkSpeed[2] = (limit*3f) + (limit / 4f);
    }


    public void OnEndDragSlider()
    {
        float[] level = new float[4];
        level[0] = 3f;
        level[3] = sliderSpeed.maxValue;
        level[1] = (level[3] - level[0]) / level[0] + level[0];
        level[2] = level[1] + (level[3] - level[0]) / level[0];

        level[0] = 10f;
        level[3] = sliderSpeed.minValue;
        level[1] = level[0] - (level[0] - level[3]) / 3;
        level[2] = level[1] - (level[0] - level[3]) / 3;

        if (sliderSpeed.value >= level[0])
        {
            sliderSpeed.value = level[0];
            tfLevel.GetChild(0).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(1).GetComponent<Image>().enabled = false;
            tfLevel.GetChild(2).GetComponent<Image>().enabled = false;
            tfLevel.GetChild(3).GetComponent<Image>().enabled = false;
        }
        else if (sliderSpeed.value < level[0] && sliderSpeed.value >= level[1])
        {
            sliderSpeed.value = level[1];
            tfLevel.GetChild(0).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(1).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(2).GetComponent<Image>().enabled = false;
            tfLevel.GetChild(3).GetComponent<Image>().enabled = false;
        }
        else if (sliderSpeed.value < level[1] && sliderSpeed.value >= level[2])
        {
            sliderSpeed.value = level[2];
            tfLevel.GetChild(0).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(1).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(2).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(3).GetComponent<Image>().enabled = false;
        }
        else
        {
            sliderSpeed.value = level[3];
            tfLevel.GetChild(0).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(1).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(2).GetComponent<Image>().enabled = true;
            tfLevel.GetChild(3).GetComponent<Image>().enabled = true;
        }

        Wheel.instance.maxLerpRotationTime = sliderSpeed.value;

        float limit = sliderSpeed.value / (float)(Wheel.instance.checkSpeed.Length + 1);

        Wheel.instance.checkSpeed[0] = limit + (limit / 2f);
        Wheel.instance.checkSpeed[1] = (limit * 2f) + (limit / 3f);
        Wheel.instance.checkSpeed[2] = (limit * 3f) + (limit / 4f);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            //PlayerManager.instance.isLogin = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class Wheel : MonoBehaviour
{
    public static Wheel instance;

    public float[] spinAngle;

    public List<float> listLose;

    public float[] checkSpeed;

    private UIPlay uiPlay;

    private Prize prize;
    
    private bool isSpinning;

    private float startAngle;

    private float finishAngle;
    
    public float maxLerpRotationTime;

    private float currentLerpRotationTime;

    private float arrowSpeed;

    private float randomFinishAngle;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        SetAngle();

        uiPlay = FindObjectOfType<UIPlay>();

        uiPlay.OnEndDragSlider();

        //GetComponent<Image>().sprite = GameConfig.instance.spWheel;

        GetComponent<Image>().sprite = GameConfig.instance.spWheel[GameConfig.instance.noWheel];

        prize = GetComponent<Prize>();

        prize.SetPrize();
        FindObjectOfType<Point>().SetPoints();
    }

    
    private void SetAngle()
    {
        spinAngle = new float[GameConfig.instance.countPrizes];
        float a = 0;
        
        for (int i = 0; i < GameConfig.instance.countPrizes; i++)
        {
           spinAngle[i] = a;
            a += GameConfig.instance.degreePrize;

            if (GameConfig.instance.timesPrize[i] < 0)
                listLose.Add(360- (i * GameConfig.instance.degreePrize));
        }
    }


    void FixedUpdate()
    {
        if (isSpinning)
        {
            if (currentLerpRotationTime <= checkSpeed[0])
            {
                currentLerpRotationTime += Time.deltaTime;
            }
            else if (currentLerpRotationTime > checkSpeed[0] && currentLerpRotationTime < checkSpeed[1])
            {
                currentLerpRotationTime += Time.deltaTime / 2.5f;
            }
            else if (currentLerpRotationTime > checkSpeed[1] && currentLerpRotationTime < checkSpeed[2])
            {
                currentLerpRotationTime += Time.deltaTime / 4.5f;
            }
            else
            {
                currentLerpRotationTime += Time.deltaTime / 6f;
            }


            if (currentLerpRotationTime > maxLerpRotationTime || this.transform.eulerAngles.z == finishAngle)
            {
                currentLerpRotationTime = maxLerpRotationTime;
                
                isSpinning = false;

                FindObjectOfType<Arrow>().transform.eulerAngles = Vector3.zero;
                FindObjectOfType<Arrow>().transform.localPosition = new Vector3(0, 507.2f, 0);
                FindObjectOfType<Arrow>().soundArrow.PlayOneShot(FindObjectOfType<Arrow>().soundArrow.clip);

                startAngle = finishAngle % 360;
                Debug.Log("Start Angle : " + startAngle);

                Debug.Log("Finish Angle : " + finishAngle);

                //GiveAward();
                uiPlay.txtamountCoin.text = PlayerManager.instance.amountCoin.ToString();
                uiPlay.EnableButton();
            }

            float t = currentLerpRotationTime / maxLerpRotationTime;

            t = t * t * t * (t * (6f * t - 15f) + 10f);

            float angle = Mathf.Lerp(startAngle, finishAngle, t);

            this.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
    

    private void GiveAward()
    {
        int angleZ = (int)transform.eulerAngles.z;

        Debug.Log("Angle Z : " + angleZ);

        for(int i = 0; i<GameConfig.instance.countPrizes; i++)
        {
            int a = i * GameConfig.instance.degreePrize;
            if( a == angleZ || a-1 == angleZ)
            {
                PlayerManager.instance.amountCoin += (GameConfig.instance.timesPrize[i] * uiPlay.chosenBet);
                uiPlay.txtamountCoin.text = PlayerManager.instance.amountCoin.ToString();
                Debug.Log(i);
                uiPlay.EnableButton();
                return;
            }
        }
    }


    public void SpinWheel()
    {
        uiPlay.DisableButton();
        
        StartCoroutine(CheckSpin());
    }


    IEnumerator CheckSpin()
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerManager.instance.playerName);
        form.AddField("idPost", GameConfig.instance.idWheel);

        WWW www = new WWW(DB.instance.URL + "daily.php", form);

        yield return www;

        Debug.Log(www.text);

        if (www.error != null)
            yield break;
        else
        {
            if (www.text == "\nL")
            {
                Debug.Log("Lose");
                randomFinishAngle = listLose[UnityEngine.Random.Range(0, listLose.Count)];
            } else
            {
                Debug.Log("Random");
                randomFinishAngle = spinAngle[UnityEngine.Random.Range(0, spinAngle.Length)];
            }
        }

        Debug.Log(randomFinishAngle);

        currentLerpRotationTime = 0f;

        maxLerpRotationTime = uiPlay.sliderSpeed.value;

        int fullCircle = 12;

        finishAngle = -(fullCircle * 360 + randomFinishAngle);

        int stopAngleZ = 0;
        
        if(randomFinishAngle != 0)
            stopAngleZ = (int)(360 - randomFinishAngle);
        
        Debug.Log("Stop Angle Z : " + stopAngleZ);

        int win = 0;

        for (int i = 0; i < GameConfig.instance.countPrizes; i++)
        {
            int a = i * GameConfig.instance.degreePrize;
            if (a == stopAngleZ)
            {
                win = GameConfig.instance.timesPrize[i];
                break;
            }
        }

        form.AddField("betPost", uiPlay.chosenBet);
        form.AddField("timesPost", win);

        WWW www2 = new WWW(DB.instance.URL + "bet.php", form);

        yield return www2;
        Debug.Log(www2.text);

        PlayerManager.instance.amountCoin = float.Parse(www2.text);
        
        // Debug.Log("Finish Angle : " + finishAngle);
        isSpinning = true;
    }
}

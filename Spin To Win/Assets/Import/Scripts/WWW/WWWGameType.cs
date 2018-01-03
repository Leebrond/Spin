using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WWWGameType : MonoBehaviour {

    public GameObject[] wheelType;

    public GameObject prefType;

    public Transform tfType;

    public GameObject panelLoading;
    
    

	void Start () {
        
            StartCoroutine(WaitData());
    }

   
    public IEnumerator WaitData()
    {
        panelLoading.SetActive(true);
        yield return StartCoroutine(GetWheelTypes());
        
        while (!PlayerManager.instance.isLogin)
        {
            yield return 0;
        }
        panelLoading.SetActive(false);
        GameObject.Find("Login").SetActive(false);

        
    }
    
    private void SetTypeWidth(int count)
    {
        if(count != 0)
        {
            float contentWidth = tfType.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            float typesWidth = (count * contentWidth) + (count * 65f);
            tfType.GetComponent<RectTransform>().sizeDelta = new Vector2(typesWidth, 100f);
        }
    }

    
    private IEnumerator GetWheelTypes()
    {
        WWW www = new WWW(DB.instance.URL + "length.php");

        yield return www;

        Debug.Log(www.text);

        string[] temp = www.text.Split(';');

        int length = temp.Length - 1;

        wheelType = new GameObject[length];

        GameConfig.instance.spWheel = new Sprite[length];

       // tfType.GetComponent<RectTransform>().sizeDelta = new Vector2((length * 600f) + 150f, 100f);

        for (int i = 0; i < wheelType.Length; i++)
        {
            wheelType[i] = Instantiate(prefType, transform.position, Quaternion.identity, tfType);
            wheelType[i].GetComponent<GameType>().nowheel = i;
            wheelType[i].GetComponent<GameType>().idwheel = int.Parse(temp[i]);

            if (i == wheelType.Length - 1)
            {
                SetTypeWidth(length);
                yield return StartCoroutine(GetPic(wheelType[i].GetComponent<GameType>().idwheel, i));
                yield return StartCoroutine(GetName());
            }
            else
            {
                StartCoroutine(GetPic(wheelType[i].GetComponent<GameType>().idwheel, i));
            }
        }
    }
  

    private IEnumerator GetPic(int id, int index)
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", id.ToString());

        WWW www = new WWW(DB.instance.URL + "stw.php", form);

        WWW www2 = new WWW(DB.instance.URL + "pic.php", form);

        yield return www;

        Debug.Log(www.text);

        Texture2D texture = new Texture2D(5, 5);
        www.LoadImageIntoTexture(texture);
        wheelType[index].GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

        yield return www2;

        GameConfig.instance.SetPic(www2, index);
       /* Texture2D textureWheel = new Texture2D(5, 5);
        www2.LoadImageIntoTexture(textureWheel);
        GameConfig.instance.spWheel[id] = Sprite.Create(textureWheel, new Rect(0, 0, textureWheel.width, textureWheel.height), Vector2.one / 2);*/
    }


    private IEnumerator GetName()
    {
       // WWWForm form = new WWWForm();
        //form.AddField("namePost", id.ToString());

        WWW www = new WWW(DB.instance.URL + "name.php");

        yield return www;

        Debug.Log(www.text);

        string[] name = www.text.Split(';');

        for(int i = 0; i<wheelType.Length; i++)
        {
            wheelType[i].transform.GetChild(1).GetComponent<Text>().text = name[i];
        }
    }
}

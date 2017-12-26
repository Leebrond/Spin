using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour {

    [SerializeField]
    private Button myAvatar;

    [SerializeField]
    private GameObject prefAvatar;

    [SerializeField]
    private Sprite[] spAvatars;

    private Transform tfCanvas;

    private Transform tfContent;

  

	
	void Start () {
        
        tfCanvas = transform.root;

        tfContent = GetComponentInChildren<HorizontalLayoutGroup>().transform;

        ShowAvatars();
	}
	
    public void ShowAvatars()
    {
        for(int i = 0; i<spAvatars.Length; i++)
        {
            GameObject avatar =Instantiate(prefAvatar, transform.position, Quaternion.identity, tfContent);
            avatar.GetComponent<Image>().sprite = spAvatars[i];
        }

        tfContent.GetComponent<RectTransform>().sizeDelta = new Vector2((spAvatars.Length * 400), 100);
    }


    public void SetAvatar(int id)
    {
        PlayerManager.instance.idAvatar = id;
        myAvatar.GetComponent<Image>().sprite = spAvatars[id];
    }
    
}

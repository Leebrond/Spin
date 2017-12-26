using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour {

   
    public void ChooseAvatar()
    {
        FindObjectOfType<Profile>().SetAvatar(transform.GetSiblingIndex());
    }
}

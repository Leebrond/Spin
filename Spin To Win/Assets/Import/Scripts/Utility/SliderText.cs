
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {

    private Text thisText;

    private Wheel wheel;


	void Start () {
        
        thisText = GetComponent<Text>();
        thisText.text = Mathf.Round(transform.parent.GetComponent<Slider>().value * 100).ToString();
	}
	
	
    public void SetSliderValue(float sliderValue)
    {
        thisText.text = Mathf.Round(sliderValue * 100).ToString();

    }
}

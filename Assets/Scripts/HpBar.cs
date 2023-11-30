using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    //public Image FillImg;
    int i = 100;

    public Slider slider;
    public Slider follower;

    Coroutine sliderCoroutine;

    float newValue;

    public void Start()
    {
        //FillImg = transform.GetChild(0).GetComponent<Image>();
        //slider = GetComponent<Slider>();
    }

    public void UpdateValue(float value, float maxValue)
    {
        newValue = value / maxValue;
        bool isHeal = slider.value < newValue;
        if (isHeal)
        {
            if (sliderCoroutine != null) StopCoroutine(sliderCoroutine);
            sliderCoroutine = StartCoroutine(SliderLerp(slider,newValue));

        }
        else
        {
            if(sliderCoroutine != null) StopCoroutine(sliderCoroutine);
            slider.value = newValue;
            sliderCoroutine = StartCoroutine(SliderLerp(follower,newValue));
        }
    }

    public void UpdateAutoHP()
    {
        Player player = GameManager.Instance.Player.GetComponent<Player>();
        UpdateValue(player.hp, player.mhp);
    }

    public void Update()
    {
        
    }

    IEnumerator SliderLerp(Slider _slider, float goal)
    {
        i = 0;
        while (true) 
        {
            //Debug.Log(_slider.value);
            
            _slider.value = Mathf.Lerp(_slider.value, goal, 2 * Time.deltaTime);
            if (Mathf.Abs(_slider.value - goal) < 0.005f) 
            { 
                _slider.value = goal;
                break;
            }


            yield return new WaitForEndOfFrame();
        }
        follower.value = goal;
        slider.value = goal;
    }
}

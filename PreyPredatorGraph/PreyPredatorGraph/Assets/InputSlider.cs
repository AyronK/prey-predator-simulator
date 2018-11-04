using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InputSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private InputField input;

    public float Value
    {
        get { return slider.value; }

        set { slider.value = value; }
    }

    public Slider.SliderEvent onValueChanged => slider.onValueChanged;

    // Use this for initialization
    void Start()
    {
        RefreshInput(slider.value);

        slider.onValueChanged.AddListener(RefreshInput);
        input.onEndEdit.AddListener(RefreshSlider);
    }

    private void RefreshSlider(string inputText)
    {
        float value;
        if (float.TryParse(inputText, out value))
        {
            slider.value = value;
        }
    }

    private void RefreshInput(float value)
    {
        input.text = $"{value:F6}";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
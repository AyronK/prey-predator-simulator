using System;
using System.Collections;
using System.Collections.Generic;
using PreyPredatorSim.Brain.Models;
using UnityEngine;
using UnityEngine.UI;

public class LotkaVolterraSim : MonoBehaviour
{
    private LotkaVolterraModel model;

    [Range(100, 1000000), SerializeField] private int initialPreyDensity = 10000;
    [Range(100, 1000000), SerializeField] private int initialPredatorDensity = 1000;

    [SerializeField] private Image preyGraph;
    [SerializeField] private Image predGraph;
    [SerializeField] private Text totalPopText;
    [SerializeField] private Text preyPopText;
    [SerializeField] private Text predPopText;
    [SerializeField] private Slider speedSlider;

    public void Reset()
    {
        model = new LotkaVolterraModel(initialPreyDensity, initialPredatorDensity)
        {
            PreyGrowthRatio = 10.0,
            PredationRate = 0.00055,
            PredatorMortalityRate = 5.0,
            PredatorReproductionRate = 0.1
        };

        RefreshView();
    }

    private void RefreshView()
    {
        var pop = model.PredatorDensity + model.PreyDensity;
        totalPopText.text = pop.ToString();
        preyPopText.text = model.PreyDensity.ToString();
        predPopText.text = model.PredatorDensity.ToString();
        var pred = (model.PredatorDensity / (float) pop);
        predGraph.fillAmount = pred;
        preyGraph.fillAmount = 1 - pred;
    }

    private void Awake()
    {
        Reset();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;

        model.Calculate(Time.deltaTime / (100 / speedSlider.value));
        RefreshView();
    }
}
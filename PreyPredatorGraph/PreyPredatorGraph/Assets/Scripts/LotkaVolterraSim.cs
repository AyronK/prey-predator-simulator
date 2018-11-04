using System;
using System.Collections;
using System.Collections.Generic;
using PreyPredatorSim.Brain.Models;
using UnityEngine;
using UnityEngine.UI;

public class LotkaVolterraSim : MonoBehaviour
{
    private LotkaVolterraModel model;
    [SerializeField] private Image preyGraph;
    [SerializeField] private Image predGraph;

    [SerializeField] private InputField totalPopText;
    [SerializeField] private InputField preyPopText;
    [SerializeField] private InputField predPopText;

    [SerializeField] private InputSlider predationRateSlider;
    [SerializeField] private InputSlider preyGrowthRatioSlider;
    [SerializeField] private InputSlider predatorMortalityRateSlider;
    [SerializeField] private InputSlider predatorReproductionRateSlider;
    [SerializeField] private InputSlider speedSlider;

    public void Reset()
    {
        int initialPreyDensity, initialPredatorDensity;

        if (int.TryParse(preyPopText.text, out initialPreyDensity) && int.TryParse(predPopText.text, out initialPredatorDensity))
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
    }

    private void RefreshView()
    {
        var pop = model.PredatorDensity + model.PreyDensity;
        totalPopText.text = pop.ToString();
        preyPopText.text = model.PreyDensity.ToString();
        predPopText.text = model.PredatorDensity.ToString();
        var pred = Mathf.Clamp((model.PredatorDensity / (float) pop), 0.0f, 1.0f);

        predGraph.fillAmount = pred;
        predGraph.enabled = predGraph.fillAmount > 0;

        preyGraph.fillAmount = 1 - pred;
        preyGraph.enabled = preyGraph.fillAmount > 0;

        predationRateSlider.Value = (float) model.PredationRate;
        preyGrowthRatioSlider.Value = (float) model.PreyGrowthRatio;
        predatorMortalityRateSlider.Value = (float) model.PredatorMortalityRate;
        predatorReproductionRateSlider.Value = (float) model.PredatorReproductionRate;
    }

    private void Awake()
    {
        preyPopText.text = "100000";
        predPopText.text = "10000";
        Reset();
    }

    // Use this for initialization
    void Start()
    {
        predationRateSlider.onValueChanged.AddListener(v => model.PredationRate = v);
        preyGrowthRatioSlider.onValueChanged.AddListener(v => model.PreyGrowthRatio = v);
        predatorMortalityRateSlider.onValueChanged.AddListener(v => model.PredatorMortalityRate = v);
        predatorReproductionRateSlider.onValueChanged.AddListener(v => model.PredatorReproductionRate = v);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Space))
            return;

        model.Calculate(Time.deltaTime / (100 / speedSlider.Value));
        RefreshView();
    }
}
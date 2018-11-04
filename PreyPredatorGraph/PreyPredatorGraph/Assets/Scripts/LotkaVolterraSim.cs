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

    private void Awake()
    {
        enabled = false;
        model = new LotkaVolterraModel((ulong) initialPreyDensity, (ulong) initialPredatorDensity)
        {
            PreyGrowthRatio = 1.0,
            PredationRate = 0.001,
            PredatorMortalityRate = 0.5,
            PredatorReproductionRate = 0.0001
        };
        Debug.Log($"Pred: {model.PredatorDensity}");
        Debug.Log($"Prey: {model.PreyDensity}");
    }

    // Use this for initialization
    void Start()
    {
    }

    private float timeElapsed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        //if (timeElapsed >= 1)
        //{
            model.Calculate(Time.deltaTime / 1);
            Debug.Log($"Pred: {model.PredatorDensity}");
            Debug.Log($"Prey: {model.PreyDensity}");
            timeElapsed = 0;
            var pop = model.PredatorDensity + model.PreyDensity;
            var pred = (model.PredatorDensity / (float) pop);
            predGraph.fillAmount = pred;
            preyGraph.fillAmount = 1 - pred;
        //}
    }
}
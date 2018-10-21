using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreyPredatorSim.Brain.Extensions;

namespace PreyPredatorSim.Brain.Models
{
    public class LotkaVolterraModel
    {
        /// <summary>
        /// Current density of prey in model.
        /// </summary>
        public ulong PreyDensity { get; private set; }

        /// <summary>
        /// Current density of predators in model.
        /// </summary>
        public ulong PredatorDensity { get; private set; }

        /// <summary>
        /// Intrinsic rate of prey population increase.
        /// </summary>
        public double PreyGrowthRatio { get; set; }

        /// <summary>
        /// Intrinsic rate of predators population decrease.
        /// </summary>
        public double PredatorMortalityRate { get; set; }

        /// <summary>
        /// Predation rate coefficient.
        /// </summary>
        public double PredationRate { get; set; }

        /// <summary>
        /// Reproduction rate of predators per 1 prey eaten
        /// </summary>
        public double PredatorReproductionRate { get; set; }

        public LotkaVolterraModel(ulong initialPreyDensity, ulong initialPredatorDensity)
        {
            PreyDensity = initialPreyDensity;
            PredatorDensity = initialPredatorDensity;
        }

        /// <summary>
        /// Recalculates density of both prey and predator population after given time.
        /// </summary>
        /// <param name="deltaTime">The amount of time since last calculation</param>
        public void Calculate(double deltaTime)
        {
            if (deltaTime <= 0)
                return;

            ulong preyDensityInNextStep = ComputePreyDensity(deltaTime);
            ulong predatorDensityInNextStep = ComputPredatorDensity(deltaTime);

            PreyDensity = preyDensityInNextStep;
            PredatorDensity = predatorDensityInNextStep;
        }

        private ulong ComputPredatorDensity(double deltaTime)
        {
            var populationChange = deltaTime * (PredatorReproductionRate * PredationRate * PreyDensity - PredatorMortalityRate);
            var computedPopulation = PredatorDensity * (1 + populationChange);
            computedPopulation = computedPopulation.Clamp(min: 0);

            return (ulong) Math.Floor(computedPopulation);
        }

        private ulong ComputePreyDensity(double deltaTime)
        {
            double populationChange = deltaTime * (PreyGrowthRatio - PredationRate * PredatorDensity);
            double computedPopulation = PreyDensity * (1 + populationChange);
            computedPopulation = computedPopulation.Clamp(min: 0);

            return (ulong) Math.Floor(computedPopulation);
        }
    }
}
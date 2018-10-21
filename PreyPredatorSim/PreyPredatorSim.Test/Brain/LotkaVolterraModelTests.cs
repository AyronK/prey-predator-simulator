using Microsoft.VisualStudio.TestTools.UnitTesting;
using PreyPredatorSim.Brain.Models;
using System.Collections.Generic;

namespace PreyPredatorSim.Test.Brain
{
    [TestClass]
    public class LotkaVolterraModelTests
    {
        [TestMethod]
        public void DeltaTimeZero_Test()
        {
            ulong initialPreyDensity = 100;
            ulong initialPredatorDensity = 10;

            var model = new LotkaVolterraModel(initialPreyDensity, initialPredatorDensity);

            model.Calculate(0);

            Assert.AreEqual(initialPreyDensity, model.PreyDensity);
            Assert.AreEqual(initialPredatorDensity, model.PredatorDensity);
        }

        [TestMethod]
        public void OnlyPrey_Test()
        {
            var stateAfterTimeStep = new Dictionary<double, (ulong PreyDensity, ulong PredatorDensity)>();
            double timeStep = 0.0;
            double deltaTime = 1.0;

            ulong initialPreyDensity = 1000;
            ulong initialPredatorDensity = 0;

            var model = new LotkaVolterraModel(initialPreyDensity, initialPredatorDensity)
            {
                PreyGrowthRatio = 0.5,
                PredationRate = 0.5,
                PredatorMortalityRate = 0.5,
                PredatorReproductionRate = 0.5
            };

            model.Calculate(deltaTime);
            timeStep += deltaTime;
            stateAfterTimeStep.Add(timeStep, (model.PreyDensity, model.PredatorDensity));

            model.Calculate(deltaTime);
            timeStep += deltaTime;
            stateAfterTimeStep.Add(timeStep, (model.PreyDensity, model.PredatorDensity));

            Assert.AreEqual((ulong)1500, stateAfterTimeStep[1 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)0, stateAfterTimeStep[1 * deltaTime].PredatorDensity);

            Assert.AreEqual((ulong)2250, stateAfterTimeStep[2 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)0, stateAfterTimeStep[2 * deltaTime].PredatorDensity);
        }

        [TestMethod]
        public void OnlyPredator_Test()
        {
            var stateAfterTimeStep = new Dictionary<double, (ulong PreyDensity, ulong PredatorDensity)>();
            double timeStep = 0.0;
            double deltaTime = 1.0;

            ulong initialPreyDensity = 0;
            ulong initialPredatorDensity = 1000;

            var model = new LotkaVolterraModel(initialPreyDensity, initialPredatorDensity)
            {
                PreyGrowthRatio = 0.5,
                PredationRate = 0.5,
                PredatorMortalityRate = 0.5,
                PredatorReproductionRate = 0.5
            };

            model.Calculate(deltaTime);
            timeStep += deltaTime;
            stateAfterTimeStep.Add(timeStep, (model.PreyDensity, model.PredatorDensity));

            model.Calculate(deltaTime);
            timeStep += deltaTime;
            stateAfterTimeStep.Add(timeStep, (model.PreyDensity, model.PredatorDensity));

            Assert.AreEqual((ulong)0, stateAfterTimeStep[1 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)500, stateAfterTimeStep[1 * deltaTime].PredatorDensity);

            Assert.AreEqual((ulong)0, stateAfterTimeStep[2 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)250, stateAfterTimeStep[2 * deltaTime].PredatorDensity);
        }

        [TestMethod]
        public void BothExist_Test()
        {
            var stateAfterTimeStep = new Dictionary<double, (ulong PreyDensity, ulong PredatorDensity)>();
            double timeElapsed = 0.0;
            double deltaTime = 1.0 / 12.0;

            ulong initialPreyDensity = 100;
            ulong initialPredatorDensity = 10;

            var model = new LotkaVolterraModel(initialPreyDensity, initialPredatorDensity)
            {
                PreyGrowthRatio = 0.1,
                PredationRate = 0.25,
                PredatorMortalityRate = 0.05,
                PredatorReproductionRate = 0.25
            };            

            model.Calculate(deltaTime);
            timeElapsed += deltaTime;
            stateAfterTimeStep.Add(timeElapsed, (model.PreyDensity, model.PredatorDensity));

            model.Calculate(deltaTime);
            timeElapsed += deltaTime;
            stateAfterTimeStep.Add(timeElapsed, (model.PreyDensity, model.PredatorDensity));
            
            model.Calculate(deltaTime);
            timeElapsed += deltaTime;
            stateAfterTimeStep.Add(timeElapsed, (model.PreyDensity, model.PredatorDensity));

            Assert.AreEqual((ulong)80, stateAfterTimeStep[1 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)15, stateAfterTimeStep[1 * deltaTime].PredatorDensity);

            Assert.AreEqual((ulong)55, stateAfterTimeStep[2 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)21, stateAfterTimeStep[2 * deltaTime].PredatorDensity);

            Assert.AreEqual((ulong)31, stateAfterTimeStep[3 * deltaTime].PreyDensity);
            Assert.AreEqual((ulong)26, stateAfterTimeStep[3 * deltaTime].PredatorDensity);
        }
    }
}

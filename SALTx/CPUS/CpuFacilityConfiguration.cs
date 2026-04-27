namespace SALTx.CPUS
{
    public sealed class CpuFacilityConfiguration
    {
        private readonly double[] classProbabilities;
        private readonly double[] serviceMeansByClass;

        public CpuFacilityConfiguration(
            string name,
            double admissionWindow,
            double meanInterarrivalTime,
            double class1Probability,
            double class2Probability,
            double class3Probability,
            double class4Probability,
            double class1ServiceMean,
            double class2ServiceMean,
            double class3ServiceMean,
            double class4ServiceMean)
        {
            Name = name;
            AdmissionWindow = admissionWindow;
            MeanInterarrivalTime = meanInterarrivalTime;
            classProbabilities = new double[]
            {
                0.0,
                class1Probability,
                class2Probability,
                class3Probability,
                class4Probability
            };
            serviceMeansByClass = new double[]
            {
                0.0,
                class1ServiceMean,
                class2ServiceMean,
                class3ServiceMean,
                class4ServiceMean
            };
        }

        public double AdmissionWindow { get; }

        public double Class1Probability
        {
            get { return classProbabilities[1]; }
        }

        public double Class2Probability
        {
            get { return classProbabilities[2]; }
        }

        public double Class3Probability
        {
            get { return classProbabilities[3]; }
        }

        public double Class4Probability
        {
            get { return classProbabilities[4]; }
        }

        public double MeanInterarrivalTime { get; }

        public string Name { get; }

        public double GetClassProbability(int classLevel)
        {
            return classProbabilities[classLevel];
        }

        public double GetServiceMean(int classLevel)
        {
            return serviceMeansByClass[classLevel];
        }

        public static CpuFacilityConfiguration Baseline()
        {
            return new CpuFacilityConfiguration(
                "Baseline",
                1020.0,
                1.91,
                0.15,
                0.30,
                0.50,
                0.05,
                3.00,
                1.50,
                1.00,
                0.25);
        }
    }
}

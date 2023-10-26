using System.ComponentModel;

namespace Lab3.ProcessedObjects
{
    internal class PatientObject: IProcessedObject
    {
        public PatientType type;
        public PatientType initType;

        public static double type1Sum;
        public static double type2Sum;
        public static double type3Sum;

        public static int type1Count;
        public static int type2Count;
        public static int type3Count;

        public double startTime;

        public PatientObject(PatientType _type)
        {
            type = _type;
            initType = _type;
        }

        public void start(double startTime)
        {
            this.startTime = startTime;
        }

        public void finish(double finishTime)
        {
            double lifeTime = finishTime - startTime;
            switch ((int)initType)
            {
                case (1):
                    type1Sum += lifeTime;
                    type1Count++;
                    break;
                case (2):
                    type2Sum += lifeTime;
                    type2Count++;
                    break;
                case (3):
                    type3Sum += lifeTime;
                    type3Count++;
                    break;
            }
        }
    }

    public enum PatientType
    {
        [Description("Хворі, що пройшли попереднє обстеження і направлені на лікування")]
        Type1=1,
        [Description("Хворі, що бажають потрапити в лікарню, але не пройшли повністю попереднє обстеження")]
        Type2=2,
        [Description("Хворі, які поступили тільки на попереднє обстеження")]
        Type3=3
    }
}

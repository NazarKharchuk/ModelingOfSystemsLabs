using System.ComponentModel;

namespace Lab3.ProcessedObjects
{
    internal class PatientObject: IProcessedObject
    {
        public PatientType type;

        public static double types12Sum;
        public static double type3Sum;

        public static int types12Count;
        public static int type3Count;

        public double startTime;

        public PatientObject(PatientType _type)
        {
            type = _type;
        }

        public void start(double startTime)
        {
            this.startTime = startTime;
        }

        public void finish(double finishTime)
        {
            double lifeTime = finishTime - startTime;
            switch ((int)type)
            {
                case (3):
                    type3Sum += lifeTime;
                    type3Count++;
                    break;
                default: types12Sum += lifeTime;
                    types12Count++;
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

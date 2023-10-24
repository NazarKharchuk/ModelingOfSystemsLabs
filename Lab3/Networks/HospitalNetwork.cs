using Lab3.DelayGenerators;
using Lab3.NextElementPickers;
using Lab3.ObjectsGenerators;
using Lab3.ProcessedObjects;
using Lab3.Queues;
using Lab3.SystemElements;
using System.Collections.Generic;

namespace Lab3.Networks
{
    internal class HospitalNetwork : INetwork
    {
        public void RunNetwork(double time)
        {
            IObjectGenerator objectGenerator = new PatientObjectGenerator();
            Create creator = new Create("CREATOR", new ExpDelayGenerator(15), objectGenerator);

            IDelayGenerator receptionDelayGenerator = new ReceptionDelayGenerator();
            QueueUnlimitedWithPriority receptionQueue = new QueueUnlimitedWithPriority();
            Process reception = new Process("RECEPTION", receptionDelayGenerator, receptionQueue, new List<Device> { 
                new Device("RECEPTION DOCTOR 1", receptionDelayGenerator),
                new Device("RECEPTION DOCTOR 2", receptionDelayGenerator)
            });

            creator.nextElementPicker = new NextElementSinglePicker(reception);

            IDelayGenerator pathToСhambersDelayGenerator = new UniformDelayGenerator(3, 8);
            QueueUnlimited pathToСhambersQueue = new QueueUnlimited();
            Process pathToСhambers = new Process("PATH TO CHAMBER", pathToСhambersDelayGenerator, pathToСhambersQueue, new List<Device> {
                new Device("COMPANION 1", pathToСhambersDelayGenerator),
                new Device("COMPANION 2", pathToСhambersDelayGenerator),
                new Device("COMPANION 3", pathToСhambersDelayGenerator)
            });

            IDelayGenerator pathToLabDelayGenerator = new UniformDelayGenerator(2, 5);
            QueueLimited pathToLabQueue = new QueueLimited(0);
            List<Device> pathsToLab = new List<Device>();
            for (int i = 0; i < 10; i++) pathsToLab.Add(new Device("PATH TO LABORATORY " + (i + 1), pathToСhambersDelayGenerator));
            Process pathToLab = new Process("PATH TO LABORATORY", pathToLabDelayGenerator, pathToLabQueue, pathsToLab);

            reception.nextElementPicker = new NextElementReceptionPicker(pathToСhambers, pathToLab);

            IDelayGenerator labRegistryDelayGenerator = new ErlangDelayGenerator(4.5, 3);
            QueueUnlimited labRegistryQueue = new QueueUnlimited();
            Process labRegistry = new Process("LABORATORY REGISTRY", labRegistryDelayGenerator, labRegistryQueue, new List<Device> {
                new Device("LABORATORY REGISTRY 1", labRegistryDelayGenerator)
            });

            pathToLab.nextElementPicker = new NextElementSinglePicker(labRegistry);

            IDelayGenerator analysisDelayGenerator = new ErlangDelayGenerator(4, 2);
            QueueUnlimited analysisQueue = new QueueUnlimited();
            Process analysis = new Process("PERFORMING ANALYZES", analysisDelayGenerator, analysisQueue, new List<Device> {
                new Device("LABORATORY ASSISTANT 1", analysisDelayGenerator),
                new Device("LABORATORY ASSISTANT 2", analysisDelayGenerator)
            });

            labRegistry.nextElementPicker = new NextElementSinglePicker(analysis);

            IDelayGenerator pathAgainToHospitalDelayGenerator = new UniformDelayGenerator(2, 5);
            QueueLimited pathAgainToHospitalQueue = new QueueLimited(0);
            List<Device> pathsAgainToHospital = new List<Device>();
            for (int i = 0; i < 10; i++) pathsAgainToHospital.Add(new Device("PATH TO HOSPITAL AGAIN " + (i + 1), pathAgainToHospitalDelayGenerator));
            Process pathAgainToHospital = new Process("PATH TO HOSPITAL AGAIN", pathAgainToHospitalDelayGenerator, pathAgainToHospitalQueue, pathsAgainToHospital);

            analysis.nextElementPicker = new NextElementAnalysisPicker(pathAgainToHospital);

            pathAgainToHospital.nextElementPicker = new NextElementPathAgainToHospitalPicker(reception);

            List<Element> elements = new()
            {
                creator,
                reception,
                pathToСhambers,
                pathToLab,
                labRegistry,
                analysis,
                pathAgainToHospital
            };

            Model model = new Model(elements);
            model.Simulate(time);

            Console.WriteLine($"\n\t-------------ВИЗНАЧЕНІ ВЕЛИЧИНИ-------------");
            Console.WriteLine($"\tЧас, проведений хворим у системі (типи 1 та 2) = {PatientObject.types12Sum / PatientObject.types12Count} ");
            Console.WriteLine($"\tЧас, проведений хворим у системі (тип 3) = {PatientObject.type3Sum / PatientObject.type3Count} ");
            Console.WriteLine($"\tІнтервал між прибуттями хворих у лабораторію = {labRegistry.sumTimeLeave / labRegistry.processedCountThis} ");
            /*Console.WriteLine($"\tSum (типи 1 та 2) = {PatientObject.types12Sum} ");
            Console.WriteLine($"\tCount (типи 1 та 2) = {PatientObject.types12Count} ");
            Console.WriteLine($"\tSum (тип 3) = {PatientObject.type3Sum} ");
            Console.WriteLine($"\tCount (тип 3) = {PatientObject.type3Count} ");*/
        }
    }
}
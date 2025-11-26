using System;
using HL7FHIRClient.Boundary;
using HL7FHIRClient.Model;

namespace HL7FHIRClient
{
    public class Program
    {
        private static readonly string serverUrl = "http://hapi.fhir.org/baseR4";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Test of HL7FHIR!");
            // TODO: 1) Use breakpoint to follow the execution of this little HL7 FHIR Client, lots of breakpoints!!!

            var bpmCompleteSequence = new BpmCompleteSequence()
            {
                NameOfObject = "231145-2341",
                StartTime = DateTime.Now,
                BpmCounts = 3600,
                DurationInSeconds = 3600
            };

            // Start receiving sample sequence from BPM device, here the data is generated 
            for (var step = 0; step < 100 /*3600*/; step++) //Simulation 1 hour in steps of second, though here truncated to 100 steps for spedding up test
            {
                var samples = new BpmLocalSampleSequence() { NoBpmValues = 50, SequenceNo = step };
                float[] data = new float[50];
                for (var sampleNo = 0; sampleNo < 50; sampleNo++)//Generating values
                {
                    data[sampleNo] = sampleNo / 100F;
                }
                //https://stackoverflow.com/questions/4635769/how-do-i-convert-an-array-of-floats-to-a-byte-and-back
                var rawData = new byte[data.Length * 4]; //Four bytes per float
                Buffer.BlockCopy(data, 0, rawData, 0, data.Length * 4); //Make floats a BLOB 
                //Third param in BlockCopy is number of bytes to copy, that's the reason to data.length *4 !
                samples.BpmSamples = rawData; //Add data to child object
                bpmCompleteSequence.SequenceOfBpmSamples.Add(samples); //Add child object to root object
            }


            var retId = bpmCompleteSequence.BpmCompleteSequenceId;

            var client = new Hl7Fhir4BpmClient(serverUrl);
            client.CreateObservation(bpmCompleteSequence);

            // TODO: 2) Create HL7 Fhir Observation and send to server
        }
    }
}

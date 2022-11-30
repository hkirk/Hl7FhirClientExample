using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HL7FHIRClient.Model
{
    public class BpmCompleteSequence
    {
        public BpmCompleteSequence()
        {
            SequenceOfBpmSamples = new List<BpmLocalSampleSequence>();
        }

        public long BpmCompleteSequenceId { get; set; } //Local Unique Id

        public string NameOfObject { get; set; } //Object is the person in focus for the Blood Pressure Measurement

        public long DurationInSeconds { get; set; } //Durations of BPM in seconds
        public DateTime StartTime { get; set; } //Start Time for BPM sequence
        public long BpmCounts { get; set; } //Actual count of BPM values in sequence
        public List<BpmLocalSampleSequence> SequenceOfBpmSamples { get; set; } //The collection of all sample sequences
    }

    public class BpmLocalSampleSequence
    {
        public long BpmLocalSampleSequenceId { get; set; } //LocalUnique Id
        public long SequenceNo { get; set; } //The number in the set  of Sample Sequences
        public long NoBpmValues { get; set; } //Number of samples in the sequence
        public byte[] BpmSamples { get; set; } //The actual sample values
    }



}

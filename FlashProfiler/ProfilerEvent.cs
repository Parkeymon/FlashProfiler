namespace FlashProfiler
{
    public class ProfilerEvent
    {
        /// <summary>
        /// The time it took for the event to run in ms
        /// </summary>
        public double Time { get; set; }
        
        /// <summary>
        /// The name of the method that was called
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// The full qualified name of the class that the method was called from
        /// </summary>
        public string Class { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Net
{
    /// <summary>
    /// Information about how the console runner should function.
    /// </summary>
    public class ConsoleRunnerInfo
    {

        /// <summary>
        /// Which strings result in closing the run-loop.
        /// </summary>
        public string[] QuitPhrases { get; set; } = new string[] { "q", "quit", "close" };
        /// <summary>
        /// Should descriptions be shown in the sample list view.
        /// </summary>
        public bool ShowDescriptionInList { get; set; } = false;

        /// <summary>
        /// Should the description be shown before the sample is run.
        /// </summary>
        public bool ShowDescriptionAtSampleStart { get; set; } = true;

    }
}

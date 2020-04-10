using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public interface ISpeechToText
    {
        void StartSpeechToText();
        void StopSpeechToText();
    }
}

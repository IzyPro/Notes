using System;
using System.Collections.Generic;
using System.Text;
using Notes.Renderers;

namespace Notes.Models
{
    public interface ISpeechToText
    {
        void StartSpeechToText();
        void StopSpeechToText();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Models;

namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        private ISpeechToText _speechRecognitionInstance;
        public NoteEntryPage()
        {
            InitializeComponent();
            Init();

            try
            {
                _speechRecognitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch(Exception ex)
            {
                Note.Text = ex.Message;
            }

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) => { SpeechToTextFinalResultRecieved(args); });
            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) => { start.IsEnabled = true; });
            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) => { SpeechToTextFinalResultRecieved(args); });
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            Note.Text += args;
        }

        private void Start_Clicked(object sender, EventArgs e)
        {
            try
            {
                _speechRecognitionInstance.StartSpeechToText();
            }
            catch(Exception ex)
            {
                Note.Text = ex.Message;
            }

            if(Device.RuntimePlatform == Device.iOS)
            {
                start.IsEnabled = false;
            }
        }
        void Init()
        {
            //Button.TextColor = Constants.ButtonText;
            //Button2.TextColor = Constants.ButtonText;
            BackgroundColor = Constants.BackgroundColor;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                File.WriteAllText(note.Filename, note.Text);
            }
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }
            await Navigation.PopAsync();
        }
    }
}
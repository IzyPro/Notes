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
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Listview.BackgroundColor = Constants.BackgroundColor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach(var filename in files)
            {
                notes.Add(new Note { Filename = filename, Text = File.ReadAllText(filename), Date = File.GetCreationTime(filename) });
            }
            Listview.ItemsSource = notes.OrderBy(d => d.Date).ToList();
        }
        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteEntryPage { BindingContext = new Note()});
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NoteEntryPage { BindingContext = e.SelectedItem as Note });
            }
        }
    }
}
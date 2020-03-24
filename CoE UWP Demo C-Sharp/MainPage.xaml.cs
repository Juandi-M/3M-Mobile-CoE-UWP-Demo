using System;

using System.Linq;

using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CoE_UWP_Demo_C_Sharp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            CoE_Icon.Visibility = Visibility.Collapsed;
            TextboxTExt.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Make image appear when clicking button
            CoE_Icon.Visibility = Visibility.Visible;


            //Make Text appear when clicking button
            TextboxTExt.Visibility = Visibility.Visible;


            //Read text when clicking button - using MS SpeechSynthesizer
            MediaElement mediaElement = new MediaElement();

            //Force female voice
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                VoiceInformation voiceInfo =
                    (
                        from voice in SpeechSynthesizer.AllVoices
                        where voice.Gender == VoiceGender.Female
                        select voice
                    ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice;

                synthesizer.Voice = voiceInfo;


                SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync
                ("Welcome to 3M Mobile C-O-E, we are the team in charge of screening and compliance");

                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
        }
    }
}

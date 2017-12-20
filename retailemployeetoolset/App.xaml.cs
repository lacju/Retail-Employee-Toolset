using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Media.SpeechRecognition;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Views;
using retailemployeetoolset.ViewModels;
using retailemployeetoolset.Views;

namespace retailemployeetoolset
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

            public static MobileServiceClient MobileService =
            new MobileServiceClient(
            Constants.WebApp);

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            //Register VCD  Cortana
            try
            {
                // Install the main VCD. 
                StorageFile vcdStorageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/VoiceCommandDefinition.xml"));

                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.
                    InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);

                //// Update phrase list.
                //ViewModel.ViewModelLocator locator = App.Current.Resources["ViewModelLocator"] as ViewModel.ViewModelLocator;
                //if (locator != null)
                //{
                //    await locator.TripViewModel.UpdateDestinationPhraseList();
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }



            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                rootFrame.NavigationFailed += OnNavigationFailed;


                //  Display an extended splash screen if app was not previously running.
                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                    Window.Current.Content = extendedSplash;
                }

                // Place the frame in the current Window
                Window.Current.Activate();
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(Views.NavView), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Entry point for an application activated by some means other than normal launching. 
        /// This includes voice commands, URI, share target from another app, and so on. 
        /// 
        /// NOTE:
        /// A previous version of the VCD file might remain in place 
        /// if you modify it and update the app through the store. 
        /// Activations might include commands from older versions of your VCD. 
        /// Try to handle these commands gracefully.
        /// </summary>
        /// <param name="args">Details about the activation method.</param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            Type navigationToPageType;
            ViewModels.AnswerDeskVoiceCommand? navigationCommand = null;

            // Voice command activation.
            if (args.Kind == ActivationKind.VoiceCommand)
            {
                // Event args can represent many different activation types. 
                // Cast it so we can get the parameters we care about out.
                var commandArgs = args as VoiceCommandActivatedEventArgs;

                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;

                // Get the name of the voice command and the text spoken. 
                // See VoiceCommands.xml for supported voice commands.
                string voiceCommandName = speechRecognitionResult.RulePath[0];
                string textSpoken = speechRecognitionResult.Text;

                // commandMode indicates whether the command was entered using speech or text.
                // Apps should respect text mode by providing silent (text) feedback.
                string commandMode = this.SemanticInterpretation("commandMode", speechRecognitionResult);

                switch (voiceCommandName)
                {
                    case "scheduleAppointment":
                        // Access the value of {destination} in the voice command.
                        string appointmentType = this.SemanticInterpretation("appointmentType", speechRecognitionResult);

                        // Create a navigation command object to pass to the page. 
                        navigationCommand = new ViewModels.AnswerDeskVoiceCommand(
                            voiceCommandName,
                            commandMode,
                            textSpoken,
                            appointmentType);

                            // Set the page to navigate to for this voice command.
                            navigationToPageType = typeof(Views.AnswerDeskView);
                        break;
                    case "nextAvailableAnswerDesk":
                        // Access the value of {destination} in the voice command.
                        string appointmentType1 = this.SemanticInterpretation("appointmentType", speechRecognitionResult);

                        // Create a navigation command object to pass to the page. 
                        navigationCommand = new ViewModels.AnswerDeskVoiceCommand(
                            voiceCommandName,
                            commandMode,
                            textSpoken,
                            appointmentType1);

                        // Set the page to navigate to for this voice command.
                        navigationToPageType = typeof(Views.AnswerDeskView);
                        break;
                    default:
                        // If we can't determine what page to launch, go to the default entry point.
                        navigationToPageType = typeof(Views.NavView);
                        break;
                }
            }
            // Protocol activation occurs when a card is clicked within Cortana (using a background task).
            else if (args.Kind == ActivationKind.Protocol)
            {
                // Extract the launch context. In this case, we're just using the destination from the phrase set (passed
                // along in the background task inside Cortana), which makes no attempt to be unique. A unique id or 
                // identifier is ideal for more complex scenarios. We let the destination page check if the 
                // destination trip still exists, and navigate back to the trip list if it doesn't.
                var commandArgs = args as ProtocolActivatedEventArgs;
                Windows.Foundation.WwwFormUrlDecoder decoder = new Windows.Foundation.WwwFormUrlDecoder(commandArgs.Uri.Query);
                var destination = decoder.GetFirstValueByName("LaunchContext");

                navigationCommand = new ViewModels.AnswerDeskVoiceCommand(
                    "protocolLaunch",
                    "text",
                    "destination",
                    destination);

                navigationToPageType = typeof(Views.AnswerDeskView);
            }
            else
            {
                // If we were launched via any other mechanism, fall back to the main page view.
                // Otherwise, we'll hang at a splash screen.
                navigationToPageType = typeof(Views.NavView);
            }

            // Repeat the same basic initialization as OnLaunched() above, taking into account whether
            // or not the app is already active.
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (rootFrame == null)
            {
                // Create a frame to act as the navigation context and navigate to the first page.
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            // Since we're expecting to always show a details page, navigate even if 
            // a content frame is in place (unlike OnLaunched).
            // Navigate to either the main trip list page, or if a valid voice command
            // was provided, to the details page for that trip.
            rootFrame.Navigate(navigationToPageType, navigationCommand);

            // Ensure the current window is active
            Window.Current.Activate();
        }

        private string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult)
        {
            return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}

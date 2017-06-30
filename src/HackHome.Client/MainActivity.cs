using Android.App;
using Android.Widget;
using Android.OS;
using HackHome.SAL;
using HackHome.Entities;
using Android.Views;

namespace HackHome.Client
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button validateButton;
        EditText emailEditText, passwordEditText;
        ImageView orientationImage;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);

            validateButton = FindViewById<Button>(Resource.Id.validateButton);
            emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            orientationImage = FindViewById<ImageView>(Resource.Id.orientationImage);

            validateButton.Click += ValidateButton_Click;

            if (IsLandScape(this))
            {
                orientationImage.SetImageResource(Resource.Drawable.land);
                orientationImage.Visibility = ViewStates.Invisible;
            }
            else
            {
                orientationImage.SetImageResource(Resource.Drawable.port);
                orientationImage.Visibility = ViewStates.Visible;
            }
            
        }

        private async void ValidateButton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text;
            var password = passwordEditText.Text;
            emailEditText.Enabled = false;
            passwordEditText.Enabled = false;
            var serviceClient = new ServiceClient();
            var result = await serviceClient.AutenticateAsync(email, password);

            if(result.Status == Status.Success || result.Status == Status.AllSuccess)
            {
                string deviceId = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                var microsotClient = new MicrosoftServiceClient();
                await microsotClient.SendEvidence(new LabItem
                {
                    DeviceId = deviceId,
                    Email = email,
                    Lab = "Hack@Home"
                });
                var newIntent = new Android.Content.Intent(this, typeof(EvidenceActivity));
                newIntent.PutExtra("FullName", result.FullName);
                newIntent.PutExtra("Token", result.Token);
                StartActivity(newIntent);

                emailEditText.Text = string.Empty;
                passwordEditText.Text = string.Empty;
                emailEditText.Enabled = true;
                passwordEditText.Enabled = true;
            }
            else
            {
                int messageId;
                switch (result.Status)
                {
                    case Status.InvalidUserOrNotInEvent:
                        messageId = Resource.String.InvalidUserOrNotInEvent_Authentication;
                        break;
                    case Status.OutOfDate:
                        messageId = Resource.String.OutOfDate_Authentication;
                        break;
                    case Status.Error:
                    default:
                        messageId = Resource.String.Error_Authentication;
                        break;
                }

                new Android.App.AlertDialog.Builder(this)
                .SetMessage(this.Resources.GetString(messageId))
                .SetPositiveButton("OK", delegate { })
                .Show();

                emailEditText.Enabled = true;
                passwordEditText.Enabled = true;
            }
        }

        bool IsLandScape(Activity activity)
        {
            var orientation = activity.WindowManager.DefaultDisplay.Rotation;
            return orientation == SurfaceOrientation.Rotation90 || 
                orientation == SurfaceOrientation.Rotation270;
        }
    }
}


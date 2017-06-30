using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackHome.SAL;
using Android.Webkit;

namespace HackHome.Client
{
    [Activity(Label = "@string/ApplicationName")]
    public class EvidenceDetailActivity : Activity
    {
        TextView fullNameTextView, evidenceTitleTextView, evidenceStatusTextView;
        ImageView evidenceImageView;
        WebView evidenceDescription;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EvidenceDetail);

            fullNameTextView = FindViewById<TextView>(Resource.Id.fullNameTextView);
            fullNameTextView.Text = Intent.GetStringExtra("FullName");

            var token = Intent.GetStringExtra("Token");
            var evidenceId = Intent.GetIntExtra("EvidenceID", 0);
            var title = Intent.GetStringExtra("Title");
            var status = Intent.GetStringExtra("Status");

            var serviceClient = new ServiceClient();
            var evidence = await serviceClient.GetEvidenceByIDAsync(token, evidenceId);

            evidenceTitleTextView = FindViewById<TextView>(Resource.Id.evidenceTitleTextView);
            evidenceTitleTextView.Text = title;

            evidenceStatusTextView = FindViewById<TextView>(Resource.Id.evidenceStatusTextView);
            evidenceStatusTextView.Text = status;

            evidenceDescription = FindViewById<WebView>(Resource.Id.evidenceDescription);
            evidenceDescription.LoadDataWithBaseURL(null, evidence.Description, "text/html", "utf-8", null);

            evidenceImageView = FindViewById<ImageView>(Resource.Id.evidenceImageView);
            Koush.UrlImageViewHelper.SetUrlDrawable(evidenceImageView, evidence.Url);
        }
    }
}
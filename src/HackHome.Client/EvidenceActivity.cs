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
using HackHome.Client.Fragments;
using HackHome.SAL;
using HackHome.CustomAdapters;
using HackHome.Entities;

namespace HackHome.Client
{
    [Activity(Label = "@string/ApplicationName")]
    public class EvidenceActivity : Activity
    {
        TextView fullNameTextView;
        ListView evidenceListView;
        string token, fullName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Evidence);

            fullName = Intent.GetStringExtra("FullName");
            token = Intent.GetStringExtra("Token");

            evidenceListView = FindViewById<ListView>(Resource.Id.evidenceListView);
            fullNameTextView = FindViewById<TextView>(Resource.Id.fullNameTextView);
            fullNameTextView.Text = fullName;

            evidenceListView.ItemClick += EvidenceListView_ItemClick;

            var data = this.FragmentManager.FindFragmentByTag("Data") as EvidenceData;
            if(data == null)
            {
                LoadData();
            }
            else
            {
                evidenceListView.Adapter = data.Adapter;
            }
        }

        private async void LoadData()
        {
            var serviceClient = new ServiceClient();
            var evidenceList = await serviceClient.GetEvidencesAsync(token);
            var adapter = new EvidencesAdapter(this,
                evidenceList,
                Resource.Layout.EvidenceItem,
                Resource.Id.evidenceTitleTextView,
                Resource.Id.evidenceStatusTextView
            );
            evidenceListView.Adapter = adapter;

            var data = new EvidenceData
            {
                Adapter = adapter
            };
            var FragmentTransaction = this.FragmentManager.BeginTransaction();
            FragmentTransaction.Add(data, "Data");
            FragmentTransaction.Commit();
        }

        private void EvidenceListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var serviceClient = new ServiceClient();
            var evidence = (evidenceListView.Adapter as EvidencesAdapter)[e.Position];

            var newIntent = new Android.Content.Intent(this, typeof(EvidenceDetailActivity));
            newIntent.PutExtra("Title", evidence.Title);
            newIntent.PutExtra("Status", evidence.Status);
            newIntent.PutExtra("EvidenceID", evidence.EvidenceID);
            newIntent.PutExtra("FullName", fullName);
            newIntent.PutExtra("Token", token);
            StartActivity(newIntent);
        }
    }
}
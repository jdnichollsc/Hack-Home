using Android.App;
using Android.OS;
using HackHome.CustomAdapters;
using HackHome.Entities;
using System.Collections.Generic;

namespace HackHome.Client.Fragments
{
    public class EvidenceData : Fragment
    {
        public EvidencesAdapter Adapter { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}
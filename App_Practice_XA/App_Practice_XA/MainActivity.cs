using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Support.Design;
using Android.Webkit;
using Android.Views;
using Calligraphy;
using Java.Lang;

namespace App_Practice_XA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ExpandableListView expListView;
        List<string> listDataHeader;
        Dictionary<string, List<string>> listDataChild;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            expListView = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);

            // Prepare list data
            listDataHeader = new List<string>();
            listDataChild = new Dictionary<string, List<string>>();

            // Adding child data
            listDataHeader.Add("Language Programming");
            listDataHeader.Add("OS");
            listDataHeader.Add("Color");

            // Adding child data
            var lstCS = new List<string>();
            lstCS.Add("C");
            lstCS.Add("C++");
            lstCS.Add("C#");
            lstCS.Add("Java");
            lstCS.Add("Python");
            lstCS.Add("Java Script");
            lstCS.Add("Php");

            var lstEC = new List<string>();
            lstEC.Add("Winwdos");
            lstEC.Add("Android");
            lstEC.Add("IOS");
            lstEC.Add("Mac");
            lstEC.Add("Linux");

            var lstMech = new List<string>();
            lstMech.Add("Blue");
            lstMech.Add("White");
            lstMech.Add("Black");
            lstMech.Add("Red");
            lstMech.Add("Orange");
            lstMech.Add("Green");

            // Header, Child data
            listDataChild.Add(listDataHeader[0], lstCS);
            listDataChild.Add(listDataHeader[1], lstEC);
            listDataChild.Add(listDataHeader[2], lstMech);

            //Bind list
            var ExpandableListAdapter = new ExpandableListAdapter(this, listDataHeader, listDataChild,new List<int>() { Resource.Mipmap.dev,Resource.Mipmap.win,Resource.Mipmap.color });
            expListView.SetAdapter(ExpandableListAdapter);

            expListView.ChildClick += ExpListView_ChildClick;

           
        }

        private void ExpListView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            var vchild = e.ClickedView;
            TextView textView = vchild.FindViewById<TextView>(Resource.Id.textViewAcMain_Item);
            Toast.MakeText(this, textView.Text, ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        private Activity context;
        private List<string> List_Data_Header;
        private Dictionary<string, List<string>> List_Data_Child;
        private List<int> Icons;

        public ExpandableListAdapter(Activity context, List<string> listDataHeader, Dictionary<string, List<string>> listChildDatas , List <int> icon)
        {
            this.context = context;
            this.List_Data_Header = listDataHeader;
            this. List_Data_Child = listChildDatas;
            Icons = icon;
        }
        //for cchild item view
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return List_Data_Child[List_Data_Header[groupPosition]][childPosition];
        }
        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            string childText = (string)GetChild(groupPosition, childPosition);
            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.layout_Item, null);
            }
            TextView txtListChild = (TextView)convertView.FindViewById(Resource.Id.textViewAcMain_Item);
            txtListChild.Text = childText;
            return convertView;
        }
        public override int GetChildrenCount(int groupPosition)
        {
            return List_Data_Child[List_Data_Header[groupPosition]].Count;
        }
        //For header view
        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return List_Data_Header[groupPosition];
        }
        public override int GroupCount
        {
            get
            {
                return List_Data_Header.Count;
            }
        }
        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }
        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string headerTitle = (string)GetGroup(groupPosition);
            var cd = Icons[groupPosition];

            convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.layout_header, null);
            var lblListHeader = (TextView)convertView.FindViewById(Resource.Id.textViewAcmain_H);
            var aImage = (ImageView)convertView.FindViewById(Resource.Id.imageViewHH);
            aImage.SetImageResource(cd);
            lblListHeader.Text = headerTitle;

            return convertView;
        }
        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }
        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

    
    }
}
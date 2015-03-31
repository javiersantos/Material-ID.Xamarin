using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using com.refractored;
using Android.Support.V4.View;
using Android.Util;
using Android.Graphics;

namespace MaterialID {
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class MainActivity : BaseActivity, IOnTabReselectedListener {

		protected override int LayoutResource {
			get {
				return Resource.Layout.main;
			}
		}

		private MyPagerAdapter adapter;
		private Drawable oldBackground = null;
		private int currentColor;
		private ViewPager pager;
		private PagerSlidingTabStrip tabs;

		protected override void OnCreate (Bundle bundle) {
			base.OnCreate (bundle);

			adapter = new MyPagerAdapter(SupportFragmentManager);
			pager = FindViewById<ViewPager> (Resource.Id.pager);
			tabs = FindViewById<PagerSlidingTabStrip> (Resource.Id.tabs);
			pager.Adapter = adapter;
			tabs.SetViewPager (pager);

			var pageMargin = (int)TypedValue.ApplyDimension (ComplexUnitType.Dip, 4, Resources.DisplayMetrics);
			pager.PageMargin = pageMargin;
			tabs.OnTabReselectedListener = this;

			ChangeColor (Resources.GetColor (Resource.Color.blueLight));
		}



		#region IOnTabReselectedListener implementation
		public void OnTabReselected (int position){
//			Toast.MakeText(this, "Tab reselected: " + position, ToastLength.Short).Show();
		} 
		#endregion

		private void ChangeColor(Color newColor) {
			tabs.SetBackgroundColor(newColor);

			// change ActionBar color just if an ActionBar is available
			Drawable colorDrawable = new ColorDrawable(newColor);
			Drawable bottomDrawable = new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent));
			LayerDrawable ld = new LayerDrawable(new Drawable[]{colorDrawable, bottomDrawable});
			if (oldBackground == null) {
				SupportActionBar.SetBackgroundDrawable(ld);
			} else {
				TransitionDrawable td = new TransitionDrawable(new Drawable[]{oldBackground, ld});
				SupportActionBar.SetBackgroundDrawable(td);
				td.StartTransition(200);
			}

			oldBackground = ld;
			currentColor = newColor;
		}
		[Java.Interop.Export("onColorClicked")]
		public void OnColorClicked(View v) {
			var color = Color.ParseColor(v.Tag.ToString());
			ChangeColor(color);
		}

		protected override void OnSaveInstanceState (Bundle outState) {
			base.OnSaveInstanceState (outState);
			outState.PutInt ("currentColor", currentColor);
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState) {
			base.OnRestoreInstanceState (savedInstanceState);
			currentColor = savedInstanceState.GetInt ("currentColor");
			ChangeColor (new Color (currentColor));
		}
	}

	public class MyPagerAdapter : FragmentPagerAdapter{
		private string[] Titles = {"Device", "Hardware", "Battery", "Connectivity", "Geolocation", "Other", "About"};

		public MyPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm) {
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position) {
			return new Java.Lang.String (Titles [position]);
		}
		#region implemented abstract members of PagerAdapter
		public override int Count {
			get {
				return Titles.Length;
			}
		}
		#endregion
		#region implemented abstract members of FragmentPagerAdapter
		public override Android.Support.V4.App.Fragment GetItem (int position) {
			return CardFragment.NewInstance (position);
		}
		#endregion
	}
}
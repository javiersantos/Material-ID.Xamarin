using Android.Support.V4.App;
using Android.OS;
using Android.Support.V4.View;
using System;

using Android.Widget;
using DeviceInfo.Plugin;
using Battery.Plugin;
using Connectivity.Plugin;
using Android.Content.Res;
using com.refractored;
using Geolocator.Plugin;
using Refractored.Xam.Vibrate;
using Android.Telephony;
using Android.Content;

namespace MaterialID {
	public class CardFragment : Fragment {
		private int position;

		public static CardFragment NewInstance(int position) {
			var f = new CardFragment ();
			var b = new Bundle ();
			b.PutInt("position", position);
			f.Arguments = b;
			return f;
		}

		public override void OnCreate (Bundle savedInstanceState) {
			base.OnCreate (savedInstanceState);

			position = Arguments.GetInt ("position");
		}

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState) {
			if (position == 0) {
				var root = inflater.Inflate (Resource.Layout.card_device, container, false);

				var device_name = root.FindViewById<TextView> (Resource.Id.device_name);
				var device_version = root.FindViewById<TextView> (Resource.Id.device_version);
				var device_id = root.FindViewById<TextView> (Resource.Id.device_id);
				var device_bootloader = root.FindViewById<TextView> (Resource.Id.device_bootloader);
				var device_fingerprint = root.FindViewById<TextView> (Resource.Id.device_fingerprint);
				var device_tags = root.FindViewById<TextView> (Resource.Id.device_tags);

				device_name.Append (CrossDeviceInfo.Current.Model);
				device_version.Append (CrossDeviceInfo.Current.Version);
				device_id.Append (CrossDeviceInfo.Current.Id.ToUpper ());
				device_bootloader.Append (Android.OS.Build.Bootloader);
				device_fingerprint.Append (Android.OS.Build.Fingerprint);
				device_tags.Append (Android.OS.Build.Tags);

				ViewCompat.SetElevation (root, 50);
				return root;

			} else if (position == 1) {
				var root = inflater.Inflate (Resource.Layout.card_hardware, container, false);

				var hardware_manufacturer = root.FindViewById<TextView> (Resource.Id.hardware_manufacturer);
				var hardware_brand = root.FindViewById<TextView> (Resource.Id.hardware_brand);
				var hardware_hardware = root.FindViewById<TextView> (Resource.Id.hardware_hardware);
				var hardware_cpu_abi = root.FindViewById<TextView> (Resource.Id.hardware_cpu_abi);
				var hardware_cpu_abi2 = root.FindViewById<TextView> (Resource.Id.hardware_cpu_abi2);

				hardware_manufacturer.Append (Android.OS.Build.Manufacturer);
				hardware_brand.Append (Android.OS.Build.Brand);
				hardware_hardware.Append (Android.OS.Build.Hardware);
				hardware_cpu_abi.Append (Android.OS.Build.CpuAbi);
				hardware_cpu_abi2.Append (Android.OS.Build.CpuAbi2);

				ViewCompat.SetElevation (root, 50);
				return root;

			} else if (position == 2) {
				var root = inflater.Inflate (Resource.Layout.card_battery, container, false);

				var battery_percent = root.FindViewById<TextView> (Resource.Id.battery_percent);
				var battery_status = root.FindViewById<TextView> (Resource.Id.battery_status);

				battery_percent.Append (CrossBattery.Current.RemainingChargePercent.ToString () + " %");
				battery_status.Append (CrossBattery.Current.Status.ToString ());

				ViewCompat.SetElevation (root, 50);
				return root;

			} else if (position == 3) {
				var root = inflater.Inflate (Resource.Layout.card_connectivity, container, false);

				var connectivity_status = root.FindViewById<TextView> (Resource.Id.connectivity_status);

				if (CrossConnectivity.Current.IsConnected) {
					connectivity_status.Append (Resources.GetString (Resource.String.yes));
				} else {
					connectivity_status.Append (Resources.GetString (Resource.String.no));
				}

				ViewCompat.SetElevation (root, 50);
				return root;

			} else if (position == 4) {
				var root = inflater.Inflate (Resource.Layout.card_geolocation, container, false);

				var geolocation_available = root.FindViewById<TextView> (Resource.Id.geolocation_available);
				var geolocation_enable = root.FindViewById<TextView> (Resource.Id.geolocation_enable);

				if (CrossGeolocator.Current.IsGeolocationAvailable) {
					geolocation_available.Append (Resources.GetString (Resource.String.yes));
				} else {
					geolocation_available.Append (Resources.GetString (Resource.String.no));
				}
				if (CrossGeolocator.Current.IsGeolocationEnabled) {
					geolocation_enable.Append (Resources.GetString (Resource.String.yes));
				} else {
					geolocation_enable.Append (Resources.GetString (Resource.String.no));
				}

				ViewCompat.SetElevation (root, 50);
				return root;


			} else if (position == 5) {
				var root = inflater.Inflate (Resource.Layout.card_other, container, false);

				var other_vibration = root.FindViewById<Button> (Resource.Id.other_vibration);

				other_vibration.Click += delegate {
					CrossVibrate.Current.Vibration(2000);
				};

				ViewCompat.SetElevation (root, 50);
				return root;

			} else {
				var root = inflater.Inflate (Resource.Layout.card_about, container, false);

				var about_title = root.FindViewById<TextView> (Resource.Id.about_title);
				var about_version = root.FindViewById<TextView> (Resource.Id.about_version);
				var about_description = root.FindViewById<TextView> (Resource.Id.about_description);
				var about_author = root.FindViewById<TextView> (Resource.Id.about_author);
				var about_github = root.FindViewById<Button> (Resource.Id.about_github);

				about_version.Append (Resources.GetString(Resource.String.app_version) + " \"" + Resources.GetString(Resource.String.app_version_name) + "\"");

				about_github.Click += delegate {
					var uri = Android.Net.Uri.Parse ("https://github.com/javiersantos/Material-ID");
					var intent = new Intent (Intent.ActionView, uri); 
					StartActivity (intent);
				};

				ViewCompat.SetElevation (root, 50);
				return root;

			}
		}

	}

}
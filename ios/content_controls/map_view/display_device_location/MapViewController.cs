using System;
using System.Timers;
using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;

namespace MapView {

	public class MapViewController : UIViewController {
		
		MKMapView mapView;
		UISegmentedControl mapTypes;
		CLLocationManager locationManager = new CLLocationManager();
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "User Location";
			
			mapView = new MKMapView(View.Bounds);	
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;		
			//mapView.MapType = MKMapType.Standard;	// this is the default
			//mapView.MapType = MKMapType.Satellite;
			//mapView.MapType = MKMapType.Hybrid;
			View.AddSubview(mapView);

            Timer delayTimer = new Timer();
            delayTimer.Interval = 4000;
            delayTimer.Elapsed += (object sender, ElapsedEventArgs e) => {
                delayTimer.Stop();
                locationManager.RequestWhenInUseAuthorization();
                // this is required to show the blue dot indicating user-location
                mapView.ShowsUserLocation = true;
            };
            delayTimer.Start();
			
			Console.WriteLine ("initial loc:"+mapView.UserLocation.Coordinate.Latitude + "," + mapView.UserLocation.Coordinate.Longitude);

			mapView.DidUpdateUserLocation += (sender, e) => {
				if (mapView.UserLocation != null) {
					Console.WriteLine ("userloc:"+mapView.UserLocation.Coordinate.Latitude + "," + mapView.UserLocation.Coordinate.Longitude);
					CLLocationCoordinate2D coords = mapView.UserLocation.Coordinate;
					MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(2), MilesToLongitudeDegrees(2, coords.Latitude));
					mapView.Region = new MKCoordinateRegion(coords, span);
				}
			};

			if (!mapView.UserLocationVisible) {
				// user denied permission, or device doesn't have GPS/location ability
				Console.WriteLine ("userloc not visible, show cupertino");
				CLLocationCoordinate2D coords = new CLLocationCoordinate2D(37.33233141,-122.0312186); // cupertino
				MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));
				mapView.Region = new MKCoordinateRegion(coords, span);
			}

			int typesWidth=260, typesHeight=30, distanceFromBottom=60;
			mapTypes = new UISegmentedControl(new CGRect((View.Bounds.Width-typesWidth)/2, View.Bounds.Height-distanceFromBottom, typesWidth, typesHeight));
            mapTypes.BackgroundColor = UIColor.White;
            mapTypes.Layer.CornerRadius = 5;
            mapTypes.ClipsToBounds = true;
            mapTypes.InsertSegment("Road", 0, false);
			mapTypes.InsertSegment("Satellite", 1, false);
			mapTypes.InsertSegment("Hybrid", 2, false);
			mapTypes.SelectedSegment = 0; // Road is the default
			mapTypes.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			mapTypes.ValueChanged += (s, e) => {
				switch(mapTypes.SelectedSegment) {
				case 0:
					mapView.MapType = MKMapType.Standard;
					break;
				case 1:
					mapView.MapType = MKMapType.Satellite;
					break;
				case 2:
					mapView.MapType = MKMapType.Hybrid;
					break;
				}
			};

			View.AddSubview(mapTypes);
           
			

		}

		/// <summary>
		/// Converts miles to latitude degrees
		/// </summary>
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0;
			double radiansToDegrees = 180.0/Math.PI;
			return (miles/earthRadius) * radiansToDegrees;
		}

		/// <summary>
		/// Converts miles to longitudinal degrees at a specified latitude
		/// </summary>
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0;
			double degreesToRadians = Math.PI/180.0;
			double radiansToDegrees = 180.0/Math.PI;

			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
    		return (miles / radiusAtLatitude) * radiansToDegrees;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MapView {
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate {
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;
		UIViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            //AppCenter.Start("4b37d08a-66fd-4136-bc4c-1d99e3c7ee3a",
                   //typeof(Analytics), typeof(Crashes));
            AppCenter.Start("4b37d08a-66fd-4136-bc4c-1d99e3c7ee3a", typeof(Analytics), typeof(Crashes));
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new MapViewController();
			
			navigationController = new UINavigationController();
			navigationController.PushViewController (viewController, false);

			// If you have defined a view, add it here:
            window.RootViewController = navigationController;
			
			// make the window visible
			window.MakeKeyAndVisible ();
           #if ENABLE_TEST_CLOUD
           Xamarin.Calabash.Start();
           #endif
			return true;
		}
	}
}


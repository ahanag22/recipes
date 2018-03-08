using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;

namespace MapViewUITest
{
    [TestFixture]
    public class Tests
    {
        iOSApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the iOS app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            //
            // The iOS project should have the Xamarin.TestCloud.Agent NuGet package
            // installed. To start the Test Cloud Agent the following code should be
            // added to the FinishedLaunching method of the AppDelegate:
            //
            //    #if ENABLE_TEST_CLOUD
            //    Xamarin.Calabash.Start();
            //    #endif
            app = ConfigureApp
                .iOS
                // TODO: Update this path to point to your iOS app and uncomment the
                // code if the app is not included in the solution.
                //.AppBundle ("../../../iOS/bin/iPhoneSimulator/Debug/MapViewUITest.iOS.app")
                //.AppBundle ("/Users/allyis1/Desktop/recipes/ios/content_controls/map_view/display_device_location/bin/iPhone/Release/MapView.exe")
                .StartApp();
        }

        [Test]
        public void System_dialog_display()
        {
            Thread.Sleep(10*1000); // Wait for 10 Seconds, so by this time the location system dialog should pop-up
            app.DismissSpringboardAlerts();
            app.Screenshot("First screen.");
        }
    }
}

using VLCKit;

namespace BasicPlayerView;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new PlaybackViewController ();
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}

	public class PlaybackViewController : UIViewController
	{
		private VLCMediaPlayer player;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			player = new VLCMediaPlayer();
			player.Delegate = new TestDelegate();
			player.Drawable = View;
			player.Media = new VLCMedia(new NSUrl("https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"));
			player.Play();
		}

		public class TestDelegate : VLCMediaPlayerDelegate
		{
			public override void MediaPlayerTimeChanged(NSNotification aNotification)
			{
				Console.WriteLine("MediaPlayerTimeChanged");
			}
		}
	}
}

using VLCKit;

namespace BasicPlayerView;

[Register ("AppDelegate")]
public class AppDelegate : NSApplicationDelegate {
	public override void DidFinishLaunching (NSNotification notification)
	{
		// Insert code here to initialize your application
		var mainWindow = new NSWindow(new CGRect(0, 0, 800, 600), NSWindowStyle.Titled | NSWindowStyle.Closable | NSWindowStyle.Resizable, NSBackingStore.Buffered, false);
	
		var videoView = new VLCVideoView();
		mainWindow.ContentView = videoView;
		//
		// videoView.AutoresizingMask = NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable;
		// mainWindow.ContentView = videoView;
		//
		var mediaPlayer = new VLCMediaPlayer(videoView);
		mediaPlayer.Media = new VLCMedia(new NSUrl("https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4"));
		
		mainWindow.MakeKeyAndOrderFront(this);
		mainWindow.Center();
		
		mediaPlayer.Play();
	}

	public override void WillTerminate (NSNotification notification)
	{
		// Insert code here to tear down your application
	}
}

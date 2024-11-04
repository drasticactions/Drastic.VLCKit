using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using VLCKit;

namespace VLCKit
{
    public enum VLCLogLevel
	{
		Error = 0,
		Warning,
		Info,
		Debug
	}

	[Flags]
	public enum VLCLogContextFlag
	{
		None = 0x0,
		Module = 1 << 0,
		FileLocation = 1 << 1,
		CallingFunction = 1 << 2,
		Custom = 1 << 3,
		All = 0xf
	}

	[Native]
	public enum VLCDialogQuestionType : ulong
	{
		Normal,
		Warning,
		Critical
	}

	[Native]
	public enum VLCMediaTrackType : long
	{
		Unknown = -1,
		Audio = 0,
		Video = 1,
		Text = 2
	}

	[Native]
	public enum VLCMediaOrientation : ulong
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight,
		LeftTop,
		LeftBottom,
		RightTop,
		RightBottom
	}

	[Native]
	public enum VLCMediaProjection : ulong
	{
		Rectangular,
		EquiRectangular,
		CubemapLayoutStandard = 256
	}

	[Native]
	public enum VLCMediaType : ulong
	{
		Unknown,
		File,
		Directory,
		Disc,
		Stream,
		Playlist
	}

	public enum VLCMediaParsedStatus : uint
	{
		Init = 0,
		Pending,
		Skipped,
		Failed,
		Timeout,
		Done
	}

	public enum VLCMediaFileStatType : uint
	{
		Mtime = 0,
		Size = 1
	}

	public enum VLCMediaFileStatReturnType
	{
		Error = -1,
		NotFound = 0,
		Success = 1
	}

	[Flags]
	public enum VLCMediaParsingOptions
	{
		ParseLocal = 0x1,
		ParseNetwork = 0x2,
		ParseForced = 0x4,
		FetchLocal = 0x8,
		FetchNetwork = 0x10,
		DoInteract = 0x20
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct VLCMediaStats
	{
		public int readBytes;

		public float inputBitrate;

		public int demuxReadBytes;

		public float demuxBitrate;

		public int demuxCorrupted;

		public int demuxDiscontinuity;

		public int decodedVideo;

		public int decodedAudio;

		public int displayedPictures;

		public int latePictures;

		public int lostPictures;

		public int playedAudioBuffers;

		public int lostAudioBuffers;
	}

	public enum VLCMediaDiscovererCategoryType : uint
	{
		Devices = 0,
		Lan,
		Podcasts,
		LocalDirectories
	}

	[Native]
	public enum VLCRepeatMode : long
	{
		DoNotRepeat,
		RepeatCurrentItem,
		RepeatAllItems
	}

	[Native]
	public enum VLCMediaPlayerState : long
	{
		Stopped,
		Stopping,
		Opening,
		Buffering,
		Error,
		Playing,
		Paused
	}

	public enum VLCMediaPlaybackNavigationAction : uint
	{
		Activate = 0,
		Up,
		Down,
		Left,
		Right
	}

	[Native]
	public enum VLCDeinterlace : long
	{
		Auto = -1,
		On = 1,
		Off = 0
	}

	public enum VLCMediaPlaybackSlaveType : uint
	{
		Subtitle = 0,
		Audio
	}

	[Native]
	public enum VLCAudioStereoMode : ulong
	{
		Unset = 0,
		Stereo = 1,
		RStereo = 2,
		Left = 3,
		Right = 4,
		Dolbys = 5,
		Mono = 7
	}

	public enum VLCAudioMixMode : uint
	{
		Unset = 0,
		Stereo = 1,
		Binaural = 2,
		VLCAudioMixMode4_0 = 3,
		VLCAudioMixMode5_1 = 4,
		VLCAudioMixMode7_1 = 5
	}

	[Flags]
	public enum VLCMediaPlayerTitleType : uint
	{
		Menu = 0x1,
		Interactive = 0x2
	}

	[Flags]
	[Native]
	public enum VLCRendererPlay : long
	{
		Audio = 1L << 0,
		Video = 1L << 1
	}

}

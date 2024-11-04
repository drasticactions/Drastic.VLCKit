using System;
using AppKit;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using VLCKit;

namespace VLCKit
{
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface VLCFilterParameter
	{
		// @required @property (nonatomic) id _Nonnull value;
		[Abstract]
		[Export ("value", ArgumentSemantic.Assign)]
		NSObject Value { get; set; }

		// @required @property (readonly, nonatomic) id _Nonnull defaultValue;
		[Abstract]
		[Export ("defaultValue")]
		NSObject DefaultValue { get; }

		// @required @property (readonly, nonatomic) id _Nonnull minValue;
		[Abstract]
		[Export ("minValue")]
		NSObject MinValue { get; }

		// @required @property (readonly, nonatomic) id _Nonnull maxValue;
		[Abstract]
		[Export ("maxValue")]
		NSObject MaxValue { get; }

		// @required -(BOOL)isValueSetToDefault;
		[Abstract]
		[Export ("isValueSetToDefault")]
		// [Verify (MethodToProperty)]
		bool IsValueSetToDefault { get; }
	}

	// @protocol VLCFilter <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface IVLCFilter
	{
		// @required @property (readonly, nonatomic, weak) VLCMediaPlayer * _Nullable mediaPlayer;
		[Abstract]
		[NullAllowed, Export ("mediaPlayer", ArgumentSemantic.Weak)]
		VLCMediaPlayer MediaPlayer { get; }

		// @required @property (getter = isEnabled, nonatomic) BOOL enabled;
		[Abstract]
		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		// @required @property (readonly, nonatomic) NSDictionary<NSString *,id<VLCFilterParameter>> * _Nonnull parameters;
		// [Abstract]
		// [Export ("parameters")]
		// NSDictionary<NSString, VLCFilterParameter> Parameters { get; }

		// @required -(BOOL)resetParametersIfNeeded;
		[Abstract]
		[Export ("resetParametersIfNeeded")]
		// [Verify (MethodToProperty)]
		bool ResetParametersIfNeeded ();

		// @required -(void)applyParametersFrom:(id<VLCFilter> _Nonnull)otherFilter;
		[Abstract]
		[Export ("applyParametersFrom:")]
		void ApplyParametersFrom (IVLCFilter otherFilter);
	}

	// @interface VLCAdjustFilter : NSObject <VLCFilter>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCAdjustFilter : IVLCFilter
	{
		// @property (readonly, nonatomic) id<VLCFilterParameter> _Nonnull contrast;
		[Export ("contrast")]
		VLCFilterParameter Contrast { get; }

		// @property (readonly, nonatomic) id<VLCFilterParameter> _Nonnull brightness;
		[Export ("brightness")]
		VLCFilterParameter Brightness { get; }

		// @property (readonly, nonatomic) id<VLCFilterParameter> _Nonnull hue;
		[Export ("hue")]
		VLCFilterParameter Hue { get; }

		// @property (readonly, nonatomic) id<VLCFilterParameter> _Nonnull saturation;
		[Export ("saturation")]
		VLCFilterParameter Saturation { get; }

		// @property (readonly, nonatomic) id<VLCFilterParameter> _Nonnull gamma;
		[Export ("gamma")]
		VLCFilterParameter Gamma { get; }

		// +(instancetype _Nonnull)createWithVLCMediaPlayer:(VLCMediaPlayer * _Nonnull)mediaPlayer;
		[Static]
		[Export ("createWithVLCMediaPlayer:")]
		VLCAdjustFilter CreateWithVLCMediaPlayer (VLCMediaPlayer mediaPlayer);

		// -(instancetype _Nonnull)initWithVLCMediaPlayer:(VLCMediaPlayer * _Nonnull)mediaPlayer __attribute__((objc_designated_initializer));
		[Export ("initWithVLCMediaPlayer:")]
		[DesignatedInitializer]
		NativeHandle Constructor (VLCMediaPlayer mediaPlayer);
	}

	// @interface VLCAudio : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCAudio
	{
		// @property (getter = isMuted) BOOL muted;
		[Export ("muted")]
		bool Muted { [Bind ("isMuted")] get; set; }

		// @property (assign) int volume;
		[Export ("volume")]
		int Volume { get; set; }

		// @property (readwrite) BOOL passthrough;
		[Export ("passthrough")]
		bool Passthrough { get; set; }

		// -(void)volumeDown;
		[Export ("volumeDown")]
		void VolumeDown ();

		// -(void)volumeUp;
		[Export ("volumeUp")]
		void VolumeUp ();
	}

	// @interface VLCAudioEqualizerPreset : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCAudioEqualizerPreset
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) unsigned int index;
		[Export ("index")]
		uint Index { get; }
	}

	// @interface VLCAudioEqualizerBand : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCAudioEqualizerBand
	{
		// @property (readonly, nonatomic) float frequency;
		[Export ("frequency")]
		float Frequency { get; }

		// @property (readonly, nonatomic) unsigned int index;
		[Export ("index")]
		uint Index { get; }

		// @property (nonatomic) float amplification;
		[Export ("amplification")]
		float Amplification { get; set; }
	}

	// @interface VLCAudioEqualizer : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCAudioEqualizer
	{
		// @property (readonly, copy, nonatomic, class) NSArray<VLCAudioEqualizerPreset *> * _Nonnull presets;
		[Static]
		[Export ("presets", ArgumentSemantic.Copy)]
		VLCAudioEqualizerPreset[] Presets { get; }

		// @property (nonatomic) float preAmplification;
		[Export ("preAmplification")]
		float PreAmplification { get; set; }

		// @property (readonly, copy, nonatomic) NSArray<VLCAudioEqualizerBand *> * _Nonnull bands;
		[Export ("bands", ArgumentSemantic.Copy)]
		VLCAudioEqualizerBand[] Bands { get; }

		// -(instancetype _Nonnull)initWithPreset:(VLCAudioEqualizerPreset * _Nonnull)preset;
		[Export ("initWithPreset:")]
		NativeHandle Constructor (VLCAudioEqualizerPreset preset);
	}

	// @interface VLCLogContext : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCLogContext
	{
		// @property (readonly, nonatomic) uintptr_t objectId;
		[Export ("objectId")]
		UIntPtr ObjectId { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull objectType;
		[Export ("objectType")]
		string ObjectType { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull module;
		[Export ("module")]
		string Module { get; }

		// @property (readonly, nonatomic) NSString * _Nullable header;
		[NullAllowed, Export ("header")]
		string Header { get; }

		// @property (readonly, nonatomic) NSString * _Nullable file;
		[NullAllowed, Export ("file")]
		string File { get; }

		// @property (readonly, nonatomic) int line;
		[Export ("line")]
		int Line { get; }

		// @property (readonly, nonatomic) NSString * _Nullable function;
		[NullAllowed, Export ("function")]
		string Function { get; }

		// @property (readonly, nonatomic) unsigned long threadId;
		[Export ("threadId")]
		nuint ThreadId { get; }
	}

	// @protocol VLCLogMessageFormatting <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface IVLCLogMessageFormatting
	{
		// @required @property (readwrite, nonatomic) VLCLogContextFlag contextFlags;
		[Abstract]
		[Export ("contextFlags", ArgumentSemantic.Assign)]
		VLCLogContextFlag ContextFlags { get; set; }

		// @required @property (readwrite, nonatomic) id _Nullable customContext;
		[Abstract]
		[NullAllowed, Export ("customContext", ArgumentSemantic.Assign)]
		NSObject CustomContext { get; set; }

		// @required -(NSString * _Nonnull)formatWithMessage:(NSString * _Nonnull)message logLevel:(VLCLogLevel)level context:(VLCLogContext * _Nullable)context;
		[Abstract]
		[Export ("formatWithMessage:logLevel:context:")]
		string LogLevel (string message, VLCLogLevel level, [NullAllowed] VLCLogContext context);
	}

	// @protocol VLCLogging <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface IVLCLogging
	{
		// @required @property (readwrite, nonatomic) VLCLogLevel level;
		[Abstract]
		[Export ("level", ArgumentSemantic.Assign)]
		VLCLogLevel Level { get; set; }

		// @required -(void)handleMessage:(NSString * _Nonnull)message logLevel:(VLCLogLevel)level context:(VLCLogContext * _Nullable)context;
		[Abstract]
		[Export ("handleMessage:logLevel:context:")]
		void LogLevel (string message, VLCLogLevel level, [NullAllowed] VLCLogContext context);
	}

	// @protocol VLCFormattedMessageLogging <VLCLogging>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	interface IVLCFormattedMessageLogging : IVLCLogging
	{
		// @required @property (readwrite, nonatomic) id<VLCLogMessageFormatting> _Nonnull formatter;
		[Abstract]
		[Export ("formatter", ArgumentSemantic.Assign)]
		IVLCLogMessageFormatting Formatter { get; set; }
	}

	// @interface VLCConsoleLogger : NSObject <VLCFormattedMessageLogging>
	[BaseType (typeof(NSObject))]
	interface VLCConsoleLogger : IVLCFormattedMessageLogging
	{
		// @property (readwrite, nonatomic) id<VLCLogMessageFormatting> _Nonnull formatter;
		[Export ("formatter", ArgumentSemantic.Assign)]
		IVLCLogMessageFormatting Formatter { get; set; }
	}

	// @protocol VLCCustomDialogRendererProtocol <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface VLCCustomDialogRendererProtocol
	{
		// @required -(void)showErrorWithTitle:(NSString * _Nonnull)error message:(NSString * _Nonnull)message;
		[Abstract]
		[Export ("showErrorWithTitle:message:")]
		void ShowErrorWithTitle (string error, string message);

		// @required -(void)showLoginWithTitle:(NSString * _Nonnull)title message:(NSString * _Nonnull)message defaultUsername:(NSString * _Nullable)username askingForStorage:(BOOL)askingForStorage withReference:(NSValue * _Nonnull)reference;
		[Abstract]
		[Export ("showLoginWithTitle:message:defaultUsername:askingForStorage:withReference:")]
		void ShowLoginWithTitle (string title, string message, [NullAllowed] string username, bool askingForStorage, NSValue reference);

		// @required -(void)showQuestionWithTitle:(NSString * _Nonnull)title message:(NSString * _Nonnull)message type:(VLCDialogQuestionType)questionType cancelString:(NSString * _Nullable)cancelString action1String:(NSString * _Nullable)action1String action2String:(NSString * _Nullable)action2String withReference:(NSValue * _Nonnull)reference;
		[Abstract]
		[Export ("showQuestionWithTitle:message:type:cancelString:action1String:action2String:withReference:")]
		void ShowQuestionWithTitle (string title, string message, VLCDialogQuestionType questionType, [NullAllowed] string cancelString, [NullAllowed] string action1String, [NullAllowed] string action2String, NSValue reference);

		// @required -(void)showProgressWithTitle:(NSString * _Nonnull)title message:(NSString * _Nonnull)message isIndeterminate:(BOOL)isIndeterminate position:(float)position cancelString:(NSString * _Nullable)cancelString withReference:(NSValue * _Nonnull)reference;
		[Abstract]
		[Export ("showProgressWithTitle:message:isIndeterminate:position:cancelString:withReference:")]
		void ShowProgressWithTitle (string title, string message, bool isIndeterminate, float position, [NullAllowed] string cancelString, NSValue reference);

		// @required -(void)updateProgressWithReference:(NSValue * _Nonnull)reference message:(NSString * _Nullable)message position:(float)position;
		[Abstract]
		[Export ("updateProgressWithReference:message:position:")]
		void UpdateProgressWithReference (NSValue reference, [NullAllowed] string message, float position);

		// @required -(void)cancelDialogWithReference:(NSValue * _Nonnull)reference;
		[Abstract]
		[Export ("cancelDialogWithReference:")]
		void CancelDialogWithReference (NSValue reference);
	}

	// @interface VLCDialogProvider : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCDialogProvider
	{
		// -(instancetype _Nullable)initWithLibrary:(VLCLibrary * _Nullable)library customUI:(BOOL)customUI;
		[Export ("initWithLibrary:customUI:")]
		NativeHandle Constructor ([NullAllowed] VLCLibrary library, bool customUI);

		// @property (readwrite, nonatomic, weak) id<VLCCustomDialogRendererProtocol> _Nullable customRenderer;
		[NullAllowed, Export ("customRenderer", ArgumentSemantic.Weak)]
		VLCCustomDialogRendererProtocol CustomRenderer { get; set; }

		// -(void)postUsername:(NSString * _Nonnull)username andPassword:(NSString * _Nonnull)password forDialogReference:(NSValue * _Nonnull)dialogReference store:(BOOL)store;
		[Export ("postUsername:andPassword:forDialogReference:store:")]
		void PostUsername (string username, string password, NSValue dialogReference, bool store);

		// -(void)postAction:(int)buttonNumber forDialogReference:(NSValue * _Nonnull)dialogReference;
		[Export ("postAction:forDialogReference:")]
		void PostAction (int buttonNumber, NSValue dialogReference);

		// -(void)dismissDialogWithReference:(NSValue * _Nonnull)dialogReference;
		[Export ("dismissDialogWithReference:")]
		void DismissDialogWithReference (NSValue dialogReference);
	}

	// @protocol VLCEventsConfiguring <NSObject>
	/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/[Protocol]
	[BaseType (typeof(NSObject))]
	interface IVLCEventsConfiguring
	{
		// @required -(dispatch_queue_t _Nullable)dispatchQueue;
		[Abstract]
		[NullAllowed, Export ("dispatchQueue")]
		//[Verify (MethodToProperty)]
		DispatchQueue DispatchQueue { get; }

		// @required -(BOOL)isAsync;
		[Abstract]
		[Export ("isAsync")]
		// [Verify (MethodToProperty)]
		bool IsAsync { get; }
	}

	// @interface VLCEventsDefaultConfiguration : NSObject <VLCEventsConfiguring>
	[BaseType (typeof(NSObject))]
	interface VLCEventsDefaultConfiguration : IVLCEventsConfiguring
	{
	}

	// @interface VLCEventsLegacyConfiguration : NSObject <VLCEventsConfiguring>
	[BaseType (typeof(NSObject))]
	interface VLCEventsLegacyConfiguration : IVLCEventsConfiguring
	{
	}

	// @interface VLCFileLogger : NSObject <VLCFormattedMessageLogging>
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCFileLogger : IVLCFormattedMessageLogging
	{
		// @property (readonly, nonatomic) NSFileHandle * _Nonnull fileHandle;
		[Export ("fileHandle")]
		NSFileHandle FileHandle { get; }

		// @property (readwrite, nonatomic) id<VLCLogMessageFormatting> _Nonnull formatter;
		[Export ("formatter", ArgumentSemantic.Assign)]
		IVLCLogMessageFormatting Formatter { get; set; }

		// +(instancetype _Nonnull)createWithFileHandle:(NSFileHandle * _Nonnull)fileHandle;
		[Static]
		[Export ("createWithFileHandle:")]
		VLCFileLogger CreateWithFileHandle (NSFileHandle fileHandle);

		// -(instancetype _Nonnull)initWithFileHandle:(NSFileHandle * _Nonnull)fileHandle __attribute__((objc_designated_initializer));
		[Export ("initWithFileHandle:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSFileHandle fileHandle);
	}

	// @interface VLCLibrary : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCLibrary
	{
		// @property (nonatomic, class) id<VLCEventsConfiguring> _Nullable sharedEventsConfiguration;
		[Static]
		[NullAllowed, Export ("sharedEventsConfiguration", ArgumentSemantic.Assign)]
		IVLCEventsConfiguring SharedEventsConfiguration { get; set; }

		// @property (copy, nonatomic, class) NSString * _Nullable currentErrorMessage;
		[Static]
		[NullAllowed, Export ("currentErrorMessage")]
		string CurrentErrorMessage { get; set; }

		// +(VLCLibrary * _Nonnull)sharedLibrary;
		[Static]
		[Export ("sharedLibrary")]
		VLCLibrary SharedLibrary { get; }

		// -(instancetype _Nonnull)initWithOptions:(NSArray * _Nonnull)options;
		[Export ("initWithOptions:")]
		// [Verify (StronglyTypedNSArray)]
		NativeHandle Constructor (NSString[] options);

		// @property (readwrite, nonatomic) NSArray<id<VLCLogging>> * _Nullable loggers;
		[NullAllowed, Export ("loggers", ArgumentSemantic.Assign)]
		IVLCLogging[] Loggers { get; set; }

		// @property (readonly, copy) NSString * _Nonnull version;
		[Export ("version")]
		string Version { get; }

		// @property (readonly, copy) NSString * _Nonnull compiler;
		[Export ("compiler")]
		string Compiler { get; }

		// @property (readonly, copy) NSString * _Nonnull changeset;
		[Export ("changeset")]
		string Changeset { get; }

		// -(void)setHumanReadableName:(NSString * _Nonnull)readableName withHTTPUserAgent:(NSString * _Nonnull)userAgent;
		[Export ("setHumanReadableName:withHTTPUserAgent:")]
		void SetHumanReadableName (string readableName, string userAgent);

		// -(void)setApplicationIdentifier:(NSString * _Nonnull)identifier withVersion:(NSString * _Nonnull)version andApplicationIconName:(NSString * _Nonnull)icon;
		[Export ("setApplicationIdentifier:withVersion:andApplicationIconName:")]
		void SetApplicationIdentifier (string identifier, string version, string icon);

		// @property (assign, nonatomic) void * _Nonnull instance;
		[Export ("instance", ArgumentSemantic.Assign)]
		unsafe IntPtr Instance { get; set; }
	}

	// @interface VLCLogMessageFormatter : NSObject <VLCLogMessageFormatting>
	[BaseType (typeof(NSObject))]
	interface VLCLogMessageFormatter : IVLCLogMessageFormatting
	{
	}

	// @protocol VLCMediaDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCMediaDelegate
	{
		// @optional -(void)mediaMetaDataDidChange:(VLCMedia * _Nonnull)aMedia;
		[Export ("mediaMetaDataDidChange:")]
		void MediaMetaDataDidChange (VLCMedia aMedia);

		// @optional -(void)mediaDidFinishParsing:(VLCMedia * _Nonnull)aMedia;
		[Export ("mediaDidFinishParsing:")]
		void MediaDidFinishParsing (VLCMedia aMedia);
	}

	// @interface VLCMedia : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMedia
	{
		// +(instancetype _Nullable)mediaWithURL:(NSUrl * _Nonnull)anURL;
		[Static]
		[Export ("mediaWithURL:")]
		[return: NullAllowed]
		VLCMedia MediaWithURL (NSUrl anURL);

		// +(instancetype _Nullable)mediaWithPath:(NSString * _Nonnull)aPath;
		[Static]
		[Export ("mediaWithPath:")]
		[return: NullAllowed]
		VLCMedia MediaWithPath (string aPath);

		// +(NSString * _Nonnull)codecNameForFourCC:(uint32_t)fourcc trackType:(VLCMediaTrackType)trackType;
		[Static]
		[Export ("codecNameForFourCC:trackType:")]
		string CodecNameForFourCC (uint fourcc, VLCMediaTrackType trackType);

		// +(instancetype _Nullable)mediaAsNodeWithName:(NSString * _Nonnull)aName;
		[Static]
		[Export ("mediaAsNodeWithName:")]
		[return: NullAllowed]
		VLCMedia MediaAsNodeWithName (string aName);

		// -(instancetype _Nullable)initWithURL:(NSUrl * _Nonnull)anURL;
		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl anURL);

		// -(instancetype _Nullable)initWithPath:(NSString * _Nonnull)aPath;
		[Export ("initWithPath:")]
		NativeHandle Constructor (string aPath);

		// -(instancetype _Nullable)initWithStream:(NSInputStream * _Nonnull)stream;
		[Export ("initWithStream:")]
		NativeHandle Constructor (NSInputStream stream);

		// // -(instancetype _Nullable)initAsNodeWithName:(NSString * _Nonnull)aName;
		// [Export ("initAsNodeWithName:")]
		// NativeHandle Constructor (string aName);

		// @property (readonly) VLCMediaType mediaType;
		[Export ("mediaType")]
		VLCMediaType MediaType { get; }

		// -(NSComparisonResult)compare:(VLCMedia * _Nullable)media;
		[Export ("compare:")]
		NSComparisonResult Compare ([NullAllowed] VLCMedia media);

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCMediaDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCMediaDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readwrite, nonatomic, strong) VLCTime * _Nonnull length;
		[Export ("length", ArgumentSemantic.Strong)]
		VLCTime Length { get; set; }

		// -(VLCTime * _Nonnull)lengthWaitUntilDate:(NSDate * _Nonnull)aDate;
		[Export ("lengthWaitUntilDate:")]
		VLCTime LengthWaitUntilDate (NSDate aDate);

		// @property (readonly, nonatomic) VLCMediaParsedStatus parsedStatus;
		[Export ("parsedStatus")]
		VLCMediaParsedStatus ParsedStatus { get; }

		// @property (readonly, nonatomic, strong) NSUrl * _Nullable url;
		[NullAllowed, Export ("url", ArgumentSemantic.Strong)]
		NSUrl Url { get; }

		// @property (readonly, nonatomic, strong) VLCMediaList * _Nullable subitems;
		[NullAllowed, Export ("subitems", ArgumentSemantic.Strong)]
		VLCMediaList Subitems { get; }

		// @property (readonly, nonatomic) VLCMediaMetaData * _Nonnull metaData;
		[Export ("metaData")]
		VLCMediaMetaData MetaData { get; }

		// @property (readonly, copy, atomic) NSArray<VLCMediaTrack *> * _Nonnull tracksInformation;
		[Export ("tracksInformation", ArgumentSemantic.Copy)]
		VLCMediaTrack[] TracksInformation { get; }

		// @property (nonatomic) id _Nullable userData;
		[NullAllowed, Export ("userData", ArgumentSemantic.Assign)]
		NSObject UserData { get; set; }

		// -(VLCMediaFileStatReturnType)fileStatValueForType:(const VLCMediaFileStatType)type value:(uint64_t * _Nonnull)value;
		[Export ("fileStatValueForType:value:")]
		unsafe VLCMediaFileStatReturnType FileStatValueForType (VLCMediaFileStatType type, ulong* value);

		// -(int)parseWithOptions:(VLCMediaParsingOptions)options;
		[Export ("parseWithOptions:")]
		int ParseWithOptions (VLCMediaParsingOptions options);

		// -(int)parseWithOptions:(VLCMediaParsingOptions)options timeout:(int)timeoutValue;
		[Export ("parseWithOptions:timeout:")]
		int ParseWithOptions (VLCMediaParsingOptions options, int timeoutValue);

		// -(int)parseWithOptions:(VLCMediaParsingOptions)options timeout:(int)timeoutValue library:(VLCLibrary * _Nonnull)library;
		[Export ("parseWithOptions:timeout:library:")]
		int ParseWithOptions (VLCMediaParsingOptions options, int timeoutValue, VLCLibrary library);

		// -(void)parseStop;
		[Export ("parseStop")]
		void ParseStop ();

		// -(void)addOption:(NSString * _Nonnull)option;
		[Export ("addOption:")]
		void AddOption (string option);

		// -(void)addOptions:(NSDictionary * _Nonnull)options;
		[Export ("addOptions:")]
		void AddOptions (NSDictionary options);

		// -(int)storeCookie:(NSString * _Nonnull)cookie forHost:(NSString * _Nonnull)host path:(NSString * _Nonnull)path;
		[Export ("storeCookie:forHost:path:")]
		int StoreCookie (string cookie, string host, string path);

		// -(void)clearStoredCookies;
		[Export ("clearStoredCookies")]
		void ClearStoredCookies ();

		// @property (readonly, nonatomic) VLCMediaStats statistics;
		[Export ("statistics")]
		VLCMediaStats Statistics { get; }
	}

	// @interface Tracks (VLCMedia)
	// [Category]
	// [BaseType (typeof(VLCMedia))]
	// interface VLCMedia_Tracks
	// {
	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaTrack *> * _Nonnull audioTracks;
	// 	[Export ("audioTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaTrack[] AudioTracks { get; }

	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaTrack *> * _Nonnull videoTracks;
	// 	[Export ("videoTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaTrack[] VideoTracks { get; }

	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaTrack *> * _Nonnull textTracks;
	// 	[Export ("textTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaTrack[] TextTracks { get; }
	// }

	// @interface VLCMediaAudioTrack : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaAudioTrack
	{
		// @property (readonly, nonatomic) unsigned int channelsNumber;
		[Export ("channelsNumber")]
		uint ChannelsNumber { get; }

		// @property (readonly, nonatomic) unsigned int rate;
		[Export ("rate")]
		uint Rate { get; }
	}

	// @interface VLCMediaVideoTrack : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaVideoTrack
	{
		// @property (readonly, nonatomic) unsigned int height;
		[Export ("height")]
		uint Height { get; }

		// @property (readonly, nonatomic) unsigned int width;
		[Export ("width")]
		uint Width { get; }

		// @property (readonly, nonatomic) VLCMediaOrientation orientation;
		[Export ("orientation")]
		VLCMediaOrientation Orientation { get; }

		// @property (readonly, nonatomic) VLCMediaProjection projection;
		[Export ("projection")]
		VLCMediaProjection Projection { get; }

		// @property (readonly, nonatomic) unsigned int sourceAspectRatio;
		[Export ("sourceAspectRatio")]
		uint SourceAspectRatio { get; }

		// @property (readonly, nonatomic) unsigned int sourceAspectRatioDenominator;
		[Export ("sourceAspectRatioDenominator")]
		uint SourceAspectRatioDenominator { get; }

		// @property (readonly, nonatomic) unsigned int frameRate;
		[Export ("frameRate")]
		uint FrameRate { get; }

		// @property (readonly, nonatomic) unsigned int frameRateDenominator;
		[Export ("frameRateDenominator")]
		uint FrameRateDenominator { get; }
	}

	// @interface VLCMediaTextTrack : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaTextTrack
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable encoding;
		[NullAllowed, Export ("encoding")]
		string Encoding { get; }
	}

	// @interface VLCMediaTrack : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaTrack
	{
		// @property (readonly, nonatomic) VLCMediaTrackType type;
		[Export ("type")]
		VLCMediaTrackType Type { get; }

		// @property (readonly, nonatomic) u_int32_t codec;
		[Export ("codec")]
		uint Codec { get; }

		// @property (readonly, nonatomic) u_int32_t fourcc;
		[Export ("fourcc")]
		uint Fourcc { get; }

		// @property (readonly, nonatomic) int identifier;
		[Export ("identifier")]
		int Identifier { get; }

		// @property (readonly, nonatomic) int profile;
		[Export ("profile")]
		int Profile { get; }

		// @property (readonly, nonatomic) int level;
		[Export ("level")]
		int Level { get; }

		// @property (readonly, nonatomic) unsigned int bitrate;
		[Export ("bitrate")]
		uint Bitrate { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable language;
		[NullAllowed, Export ("language")]
		string Language { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable trackDescription;
		[NullAllowed, Export ("trackDescription")]
		string TrackDescription { get; }

		// @property (readonly, nonatomic) VLCMediaAudioTrack * _Nullable audio;
		[NullAllowed, Export ("audio")]
		VLCMediaAudioTrack Audio { get; }

		// @property (readonly, nonatomic) VLCMediaVideoTrack * _Nullable video;
		[NullAllowed, Export ("video")]
		VLCMediaVideoTrack Video { get; }

		// @property (readonly, nonatomic) VLCMediaTextTrack * _Nullable text;
		[NullAllowed, Export ("text")]
		VLCMediaTextTrack Text { get; }

		// -(NSString * _Nonnull)codecName;
		[Export ("codecName")]
		// [Verify (MethodToProperty)]
		string CodecName { get; }
	}

	// @interface VLCMediaPlayerTrack : VLCMediaTrack
	[BaseType (typeof(VLCMediaTrack))]
	[DisableDefaultCtor]
	interface VLCMediaPlayerTrack
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull trackId;
		[Export ("trackId")]
		string TrackId { get; }

		// @property (readonly, getter = isIdStable, nonatomic) BOOL idStable;
		[Export ("idStable")]
		bool IdStable { [Bind ("isIdStable")] get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull trackName;
		[Export ("trackName")]
		string TrackName { get; }

		// @property (getter = isSelected, nonatomic) BOOL selected;
		[Export ("selected")]
		bool Selected { [Bind ("isSelected")] get; set; }

		// @property (getter = isSelectedExclusively, nonatomic) BOOL selectedExclusively;
		[Export ("selectedExclusively")]
		bool SelectedExclusively { [Bind ("isSelectedExclusively")] get; set; }
	}

	// @interface VLCMediaDiscoverer : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMediaDiscoverer
	{
		// @property (readonly, nonatomic) VLCLibrary * _Nonnull libraryInstance;
		[Export ("libraryInstance")]
		VLCLibrary LibraryInstance { get; }

		// +(NSArray * _Nonnull)availableMediaDiscovererForCategoryType:(VLCMediaDiscovererCategoryType)categoryType;
		[Static]
		[Export ("availableMediaDiscovererForCategoryType:")]
		//[Verify (StronglyTypedNSArray)]
		VLCMediaDiscoverer[] AvailableMediaDiscovererForCategoryType (VLCMediaDiscovererCategoryType categoryType);

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)aServiceName;
		[Export ("initWithName:")]
		NativeHandle Constructor (string aServiceName);

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)aServiceName libraryInstance:(VLCLibrary * _Nullable)libraryInstance;
		[Export ("initWithName:libraryInstance:")]
		NativeHandle Constructor (string aServiceName, [NullAllowed] VLCLibrary libraryInstance);

		// -(int)startDiscoverer;
		[Export ("startDiscoverer")]
		// [Verify (MethodToProperty)]
		int StartDiscoverer ();

		// -(void)stopDiscoverer;
		[Export ("stopDiscoverer")]
		void StopDiscoverer ();

		// @property (readonly, weak) VLCMediaList * _Nullable discoveredMedia;
		[NullAllowed, Export ("discoveredMedia", ArgumentSemantic.Weak)]
		VLCMediaList DiscoveredMedia { get; }

		// @property (readonly) BOOL isRunning;
		[Export ("isRunning")]
		bool IsRunning { get; }
	}

	// @protocol VLCMediaListDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCMediaListDelegate
	{
		// @optional -(void)mediaList:(VLCMediaList * _Nonnull)aMediaList mediaAdded:(VLCMedia * _Nonnull)media atIndex:(NSUInteger)index;
		[Export ("mediaList:mediaAdded:atIndex:")]
		void MediaAdded (VLCMediaList aMediaList, VLCMedia media, nuint index);

		// @optional -(void)mediaList:(VLCMediaList * _Nonnull)aMediaList mediaRemovedAtIndex:(NSUInteger)index;
		[Export ("mediaList:mediaRemovedAtIndex:")]
		void MediaRemovedAtIndex (VLCMediaList aMediaList, nuint index);
	}

	// @interface VLCMediaList : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMediaList
	{
		// -(instancetype _Nonnull)initWithArray:(NSArray<VLCMedia *> * _Nullable)array;
		[Export ("initWithArray:")]
		NativeHandle Constructor ([NullAllowed] VLCMedia[] array);

		// -(void)lock;
		[Export ("lock")]
		void Lock ();

		// -(void)unlock;
		[Export ("unlock")]
		void Unlock ();

		// -(NSUInteger)addMedia:(VLCMedia * _Nonnull)media;
		[Export ("addMedia:")]
		nuint AddMedia (VLCMedia media);

		// -(void)insertMedia:(VLCMedia * _Nonnull)media atIndex:(NSUInteger)index;
		[Export ("insertMedia:atIndex:")]
		void InsertMedia (VLCMedia media, nuint index);

		// -(BOOL)removeMediaAtIndex:(NSUInteger)index;
		[Export ("removeMediaAtIndex:")]
		bool RemoveMediaAtIndex (nuint index);

		// -(VLCMedia * _Nullable)mediaAtIndex:(NSUInteger)index;
		[Export ("mediaAtIndex:")]
		[return: NullAllowed]
		VLCMedia MediaAtIndex (nuint index);

		// -(NSUInteger)indexOfMedia:(VLCMedia * _Nonnull)media;
		[Export ("indexOfMedia:")]
		nuint IndexOfMedia (VLCMedia media);

		// @property (readonly) NSInteger count;
		[Export ("count")]
		nint Count { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCMediaListDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCMediaListDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly) BOOL isReadOnly;
		[Export ("isReadOnly")]
		bool IsReadOnly { get; }

		// @property (readonly) BOOL isEmpty;
		[Export ("isEmpty")]
		bool IsEmpty { get; }
	}

	// @protocol VLCMediaListPlayerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCMediaListPlayerDelegate
	{
		// @optional -(void)mediaListPlayerFinishedPlayback:(VLCMediaListPlayer * _Nonnull)player;
		[Export ("mediaListPlayerFinishedPlayback:")]
		void MediaListPlayerFinishedPlayback (VLCMediaListPlayer player);

		// @optional -(void)mediaListPlayer:(VLCMediaListPlayer * _Nonnull)player nextMedia:(VLCMedia * _Nonnull)media;
		[Export ("mediaListPlayer:nextMedia:")]
		void MediaListPlayer (VLCMediaListPlayer player, VLCMedia media);

		// @optional -(void)mediaListPlayerStopped:(VLCMediaListPlayer * _Nonnull)player;
		[Export ("mediaListPlayerStopped:")]
		void MediaListPlayerStopped (VLCMediaListPlayer player);
	}

	// @interface VLCMediaListPlayer : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMediaListPlayer
	{
		// @property (readwrite) VLCMediaList * _Nullable mediaList;
		[NullAllowed, Export ("mediaList", ArgumentSemantic.Assign)]
		VLCMediaList MediaList { get; set; }

		// @property (readwrite) VLCMedia * _Nullable rootMedia;
		[NullAllowed, Export ("rootMedia", ArgumentSemantic.Assign)]
		VLCMedia RootMedia { get; set; }

		// @property (readonly) VLCMediaPlayer * _Nonnull mediaPlayer;
		[Export ("mediaPlayer")]
		VLCMediaPlayer MediaPlayer { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCMediaListPlayerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCMediaListPlayerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)initWithDrawable:(id _Nonnull)drawable;
		[Export ("initWithDrawable:")]
		NativeHandle Constructor (NSObject drawable);

		// -(instancetype _Nonnull)initWithOptions:(NSArray * _Nonnull)options;
		[Export ("initWithOptions:")]
		// [Verify (StronglyTypedNSArray)]
		NativeHandle Constructor (NSString[] options);

		// -(instancetype _Nonnull)initWithOptions:(NSArray * _Nullable)options andDrawable:(id _Nullable)drawable;
		[Export ("initWithOptions:andDrawable:")]
		//[Verify (StronglyTypedNSArray)]
		NativeHandle Constructor ([NullAllowed] NSString[] options, [NullAllowed] NSObject drawable);

		// -(void)play;
		[Export ("play")]
		void Play ();

		// -(void)pause;
		[Export ("pause")]
		void Pause ();

		// -(void)stop;
		[Export ("stop")]
		void Stop ();

		// @property (readonly, atomic) BOOL next;
		[Export ("next")]
		bool Next { get; }

		// @property (readonly, atomic) BOOL previous;
		[Export ("previous")]
		bool Previous { get; }

		// -(void)playItemAtNumber:(NSNumber * _Nonnull)index;
		[Export ("playItemAtNumber:")]
		void PlayItemAtNumber (NSNumber index);

		// @property (readwrite) VLCRepeatMode repeatMode;
		[Export ("repeatMode", ArgumentSemantic.Assign)]
		VLCRepeatMode RepeatMode { get; set; }

		// -(void)playMedia:(VLCMedia * _Nonnull)media;
		[Export ("playMedia:")]
		void PlayMedia (VLCMedia media);
	}

	// @interface VLCMediaMetaData : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaMetaData
	{
		// @property (copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable artist;
		[NullAllowed, Export ("artist")]
		string Artist { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable genre;
		[NullAllowed, Export ("genre")]
		string Genre { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable copyright;
		[NullAllowed, Export ("copyright")]
		string Copyright { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable album;
		[NullAllowed, Export ("album")]
		string Album { get; set; }

		// @property (nonatomic) unsigned int trackNumber;
		[Export ("trackNumber")]
		uint TrackNumber { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable metaDescription;
		[NullAllowed, Export ("metaDescription")]
		string MetaDescription { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable rating;
		[NullAllowed, Export ("rating")]
		string Rating { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable date;
		[NullAllowed, Export ("date")]
		string Date { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable setting;
		[NullAllowed, Export ("setting")]
		string Setting { get; set; }

		// @property (nonatomic) NSUrl * _Nullable url;
		[NullAllowed, Export ("url", ArgumentSemantic.Assign)]
		NSUrl Url { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable language;
		[NullAllowed, Export ("language")]
		string Language { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable nowPlaying;
		[NullAllowed, Export ("nowPlaying")]
		string NowPlaying { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable publisher;
		[NullAllowed, Export ("publisher")]
		string Publisher { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable encodedBy;
		[NullAllowed, Export ("encodedBy")]
		string EncodedBy { get; set; }

		// @property (nonatomic) NSUrl * _Nullable artworkURL;
		[NullAllowed, Export ("artworkURL", ArgumentSemantic.Assign)]
		NSUrl ArtworkURL { get; set; }

		// @property (nonatomic) unsigned int trackID;
		[Export ("trackID")]
		uint TrackID { get; set; }

		// @property (nonatomic) unsigned int trackTotal;
		[Export ("trackTotal")]
		uint TrackTotal { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable director;
		[NullAllowed, Export ("director")]
		string Director { get; set; }

		// @property (nonatomic) unsigned int season;
		[Export ("season")]
		uint Season { get; set; }

		// @property (nonatomic) unsigned int episode;
		[Export ("episode")]
		uint Episode { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable showName;
		[NullAllowed, Export ("showName")]
		string ShowName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable actors;
		[NullAllowed, Export ("actors")]
		string Actors { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable albumArtist;
		[NullAllowed, Export ("albumArtist")]
		string AlbumArtist { get; set; }

		// @property (nonatomic) unsigned int discNumber;
		[Export ("discNumber")]
		uint DiscNumber { get; set; }

		// @property (nonatomic) unsigned int discTotal;
		[Export ("discTotal")]
		uint DiscTotal { get; set; }

		// @property (readonly, nonatomic) VLCPlatformImage * _Nullable artwork;
		[NullAllowed, Export ("artwork")]
		NSImage Artwork { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,NSString *> * _Nullable extra;
		[NullAllowed, Export ("extra", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> Extra { get; }

		// -(BOOL)save;
		[Export ("save")]
		//[Verify (MethodToProperty)]
		bool Save ();

		// -(void)prefetch;
		[Export ("prefetch")]
		void Prefetch ();

		// -(void)clearCache;
		[Export ("clearCache")]
		void ClearCache ();

		// -(NSString * _Nullable)extraValueForKey:(NSString * _Nonnull)key;
		[Export ("extraValueForKey:")]
		[return: NullAllowed]
		string ExtraValueForKey (string key);

		// -(void)setExtraValue:(NSString * _Nullable)value forKey:(NSString * _Nonnull)key;
		[Export ("setExtraValue:forKey:")]
		void SetExtraValue ([NullAllowed] string value, string key);
	}

	// @protocol VLCMediaPlayerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCMediaPlayerDelegate
	{
		// @optional -(void)mediaPlayerStateChanged:(VLCMediaPlayerState)newState;
		[Export ("mediaPlayerStateChanged:")]
		void MediaPlayerStateChanged (VLCMediaPlayerState newState);

		// @optional -(void)mediaPlayerTrackAdded:(NSString * _Nonnull)trackId withType:(VLCMediaTrackType)trackType;
		[Export ("mediaPlayerTrackAdded:withType:")]
		void MediaPlayerTrackAdded (string trackId, VLCMediaTrackType trackType);

		// @optional -(void)mediaPlayerTrackRemoved:(NSString * _Nonnull)trackId withType:(VLCMediaTrackType)trackType;
		[Export ("mediaPlayerTrackRemoved:withType:")]
		void MediaPlayerTrackRemoved (string trackId, VLCMediaTrackType trackType);

		// @optional -(void)mediaPlayerTrackUpdated:(NSString * _Nonnull)trackId withType:(VLCMediaTrackType)trackType;
		[Export ("mediaPlayerTrackUpdated:withType:")]
		void MediaPlayerTrackUpdated (string trackId, VLCMediaTrackType trackType);

		// @optional -(void)mediaPlayerTrackSelected:(VLCMediaTrackType)trackType selectedId:(NSString * _Nonnull)unselectedId unselectedId:(NSString * _Nonnull)unselectedId;
		[Export ("mediaPlayerTrackSelected:selectedId:unselectedId:")]
		void MediaPlayerTrackSelected (VLCMediaTrackType trackType, string selectedId, string unselectedId);

		// @optional -(void)mediaPlayerLengthChanged:(int64_t)length;
		[Export ("mediaPlayerLengthChanged:")]
		void MediaPlayerLengthChanged (long length);

		// @optional -(void)mediaPlayerTimeChanged:(NSNotification * _Nonnull)aNotification;
		[Export ("mediaPlayerTimeChanged:")]
		void MediaPlayerTimeChanged (NSNotification aNotification);

		// @optional -(void)mediaPlayerTitleSelectionChanged:(NSNotification * _Nonnull)aNotification;
		[Export ("mediaPlayerTitleSelectionChanged:")]
		void MediaPlayerTitleSelectionChanged (NSNotification aNotification);

		// @optional -(void)mediaPlayerTitleListChanged:(NSNotification * _Nonnull)aNotification;
		[Export ("mediaPlayerTitleListChanged:")]
		void MediaPlayerTitleListChanged (NSNotification aNotification);

		// @optional -(void)mediaPlayerChapterChanged:(NSNotification * _Nonnull)aNotification;
		[Export ("mediaPlayerChapterChanged:")]
		void MediaPlayerChapterChanged (NSNotification aNotification);

		// @optional -(void)mediaPlayerSnapshot:(NSNotification * _Nonnull)aNotification;
		[Export ("mediaPlayerSnapshot:")]
		void MediaPlayerSnapshot (NSNotification aNotification);

		// @optional -(void)mediaPlayerStartedRecording:(VLCMediaPlayer * _Nonnull)player;
		[Export ("mediaPlayerStartedRecording:")]
		void MediaPlayerStartedRecording (VLCMediaPlayer player);

		// @optional -(void)mediaPlayer:(VLCMediaPlayer * _Nonnull)player recordingStoppedAtURL:(NSUrl * _Nullable)url;
		[Export ("mediaPlayer:recordingStoppedAtURL:")]
		void MediaPlayer (VLCMediaPlayer player, [NullAllowed] NSUrl url);
	}

	// @interface VLCMediaPlayer : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMediaPlayer
	{
		// @property (readonly, nonatomic) VLCLibrary * _Nonnull libraryInstance;
		[Export ("libraryInstance")]
		VLCLibrary LibraryInstance { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCMediaPlayerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCMediaPlayerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)initWithVideoView:(VLCVideoView * _Nonnull)aVideoView;
		[Export ("initWithVideoView:")]
		NativeHandle Constructor (VLCVideoView aVideoView);

		// -(instancetype _Nonnull)initWithVideoLayer:(VLCVideoLayer * _Nonnull)aVideoLayer;
		[Export ("initWithVideoLayer:")]
		NativeHandle Constructor (VLCVideoLayer aVideoLayer);

		// -(instancetype _Nonnull)initWithLibrary:(VLCLibrary * _Nonnull)library;
		[Export ("initWithLibrary:")]
		NativeHandle Constructor (VLCLibrary library);

		// -(instancetype _Nonnull)initWithOptions:(NSArray * _Nonnull)options;
		[Export ("initWithOptions:")]
		NativeHandle Constructor (NSString[] options);

		// -(instancetype _Nonnull)initWithLibVLCInstance:(void * _Nonnull)playerInstance andLibrary:(VLCLibrary * _Nonnull)library;
		[Export ("initWithLibVLCInstance:andLibrary:")]
		unsafe NativeHandle Constructor (IntPtr playerInstance, VLCLibrary library);

		// -(void)setVideoView:(VLCVideoView * _Nonnull)aVideoView;
		[Export ("setVideoView:")]
		void SetVideoView (VLCVideoView aVideoView);

		// -(void)setVideoLayer:(VLCVideoLayer * _Nonnull)aVideoLayer;
		[Export ("setVideoLayer:")]
		void SetVideoLayer (VLCVideoLayer aVideoLayer);

		// @property (strong) id _Nullable drawable;
		[NullAllowed, Export ("drawable", ArgumentSemantic.Strong)]
		NSObject Drawable { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable videoAspectRatio;
		[NullAllowed, Export ("videoAspectRatio")]
		string VideoAspectRatio { get; set; }

		// -(void)setCropRatioWithNumerator:(unsigned int)numerator denominator:(unsigned int)denominator;
		[Export ("setCropRatioWithNumerator:denominator:")]
		void SetCropRatioWithNumerator (uint numerator, uint denominator);

		// @property (nonatomic) float scaleFactor;
		[Export ("scaleFactor")]
		float ScaleFactor { get; set; }

		// -(void)saveVideoSnapshotAt:(NSString * _Nonnull)path withWidth:(int)width andHeight:(int)height;
		[Export ("saveVideoSnapshotAt:withWidth:andHeight:")]
		void SaveVideoSnapshotAt (string path, int width, int height);

		// -(void)setDeinterlaceFilter:(NSString * _Nullable)name;
		[Export ("setDeinterlaceFilter:")]
		void SetDeinterlaceFilter ([NullAllowed] string name);

		// -(void)setDeinterlace:(VLCDeinterlace)deinterlace withFilter:(NSString * _Nonnull)name;
		[Export ("setDeinterlace:withFilter:")]
		void SetDeinterlace (VLCDeinterlace deinterlace, string name);

		// @property (readonly, nonatomic) VLCAdjustFilter * _Nonnull adjustFilter;
		[Export ("adjustFilter")]
		VLCAdjustFilter AdjustFilter { get; }

		// @property (nonatomic) BOOL adjustFilterEnabled __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].enabled instead")));
		[Export ("adjustFilterEnabled")]
		bool AdjustFilterEnabled { get; set; }

		// @property (nonatomic) float contrast __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].contrast instead")));
		[Export ("contrast")]
		float Contrast { get; set; }

		// @property (nonatomic) float brightness __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].brightness instead")));
		[Export ("brightness")]
		float Brightness { get; set; }

		// @property (nonatomic) float hue __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].hue instead")));
		[Export ("hue")]
		float Hue { get; set; }

		// @property (nonatomic) float saturation __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].saturation instead")));
		[Export ("saturation")]
		float Saturation { get; set; }

		// @property (nonatomic) float gamma __attribute__((deprecated("Use -[VLCMediaPlayer adjustFilter].gamma instead")));
		[Export ("gamma")]
		float Gamma { get; set; }

		// @property (nonatomic) float rate;
		[Export ("rate")]
		float Rate { get; set; }

		// @property (readonly, nonatomic, weak) VLCAudio * _Nullable audio;
		[NullAllowed, Export ("audio", ArgumentSemantic.Weak)]
		VLCAudio Audio { get; }

		// @property (readonly, atomic) CGSize videoSize;
		[Export ("videoSize")]
		CGSize VideoSize { get; }

		// @property (readonly, atomic) BOOL hasVideoOut;
		[Export ("hasVideoOut")]
		bool HasVideoOut { get; }

		// @property (atomic, strong) VLCTime * _Nonnull time;
		[Export ("time", ArgumentSemantic.Strong)]
		VLCTime Time { get; set; }

		// @property (readonly, nonatomic, weak) VLCTime * _Nullable remainingTime;
		[NullAllowed, Export ("remainingTime", ArgumentSemantic.Weak)]
		VLCTime RemainingTime { get; }

		// @property (nonatomic) int64_t minimalTimePeriod;
		[Export ("minimalTimePeriod")]
		long MinimalTimePeriod { get; set; }

		// @property (nonatomic) NSTimeInterval timeChangeUpdateInterval;
		[Export ("timeChangeUpdateInterval")]
		double TimeChangeUpdateInterval { get; set; }

		// -(int)addPlaybackSlave:(NSUrl * _Nonnull)slaveURL type:(VLCMediaPlaybackSlaveType)slaveType enforce:(BOOL)enforceSelection;
		[Export ("addPlaybackSlave:type:enforce:")]
		int AddPlaybackSlave (NSUrl slaveURL, VLCMediaPlaybackSlaveType slaveType, bool enforceSelection);

		// @property (readwrite) NSInteger currentVideoSubTitleDelay;
		[Export ("currentVideoSubTitleDelay")]
		nint CurrentVideoSubTitleDelay { get; set; }

		// @property (readwrite) float currentSubTitleFontScale;
		[Export ("currentSubTitleFontScale")]
		float CurrentSubTitleFontScale { get; set; }

		// @property (readwrite) int currentChapterIndex;
		[Export ("currentChapterIndex")]
		int CurrentChapterIndex { get; set; }

		// -(void)previousChapter;
		[Export ("previousChapter")]
		void PreviousChapter ();

		// -(void)nextChapter;
		[Export ("nextChapter")]
		void NextChapter ();

		// -(int)numberOfChaptersForTitle:(int)titleIndex;
		[Export ("numberOfChaptersForTitle:")]
		int NumberOfChaptersForTitle (int titleIndex);

		// @property (nonatomic) VLCMediaPlayerChapterDescription * _Nullable currentChapterDescription;
		[NullAllowed, Export ("currentChapterDescription", ArgumentSemantic.Assign)]
		VLCMediaPlayerChapterDescription CurrentChapterDescription { get; set; }

		// -(NSArray<VLCMediaPlayerChapterDescription *> * _Nonnull)chapterDescriptionsOfTitle:(int)titleIndex;
		[Export ("chapterDescriptionsOfTitle:")]
		VLCMediaPlayerChapterDescription[] ChapterDescriptionsOfTitle (int titleIndex);

		// @property (readwrite) int currentTitleIndex;
		[Export ("currentTitleIndex")]
		int CurrentTitleIndex { get; set; }

		// @property (readonly) int numberOfTitles;
		[Export ("numberOfTitles")]
		int NumberOfTitles { get; }

		// @property (nonatomic) VLCMediaPlayerTitleDescription * _Nullable currentTitleDescription;
		[NullAllowed, Export ("currentTitleDescription", ArgumentSemantic.Assign)]
		VLCMediaPlayerTitleDescription CurrentTitleDescription { get; set; }

		// @property (readonly, copy, atomic) NSArray<VLCMediaPlayerTitleDescription *> * _Nonnull titleDescriptions;
		[Export ("titleDescriptions", ArgumentSemantic.Copy)]
		VLCMediaPlayerTitleDescription[] TitleDescriptions { get; }

		// @property (readonly) int indexOfLongestTitle;
		[Export ("indexOfLongestTitle")]
		int IndexOfLongestTitle { get; }

		// @property (nonatomic) VLCAudioStereoMode audioStereoMode;
		[Export ("audioStereoMode", ArgumentSemantic.Assign)]
		VLCAudioStereoMode AudioStereoMode { get; set; }

		// @property (nonatomic) VLCAudioMixMode audioMixMode;
		[Export ("audioMixMode", ArgumentSemantic.Assign)]
		VLCAudioMixMode AudioMixMode { get; set; }

		// @property (readwrite) NSInteger currentAudioPlaybackDelay;
		[Export ("currentAudioPlaybackDelay")]
		nint CurrentAudioPlaybackDelay { get; set; }

		// @property (nonatomic) VLCAudioEqualizer * _Nullable equalizer;
		[NullAllowed, Export ("equalizer", ArgumentSemantic.Assign)]
		VLCAudioEqualizer Equalizer { get; set; }

		// @property (atomic, strong) VLCMedia * _Nullable media;
		[NullAllowed, Export ("media", ArgumentSemantic.Strong)]
		VLCMedia Media { get; set; }

		// -(void)play;
		[Export ("play")]
		void Play ();

		// -(void)pause;
		[Export ("pause")]
		void Pause ();

		// -(void)stop;
		[Export ("stop")]
		void Stop ();

		// -(void)gotoNextFrame;
		[Export ("gotoNextFrame")]
		void GotoNextFrame ();

		// -(void)fastForward;
		[Export ("fastForward")]
		void FastForward ();

		// -(void)fastForwardAtRate:(float)rate;
		[Export ("fastForwardAtRate:")]
		void FastForwardAtRate (float rate);

		// -(void)rewind;
		[Export ("rewind")]
		void Rewind ();

		// -(void)rewindAtRate:(float)rate;
		[Export ("rewindAtRate:")]
		void RewindAtRate (float rate);

		// -(void)jumpWithOffset:(int)offset;
		[Export ("jumpWithOffset:")]
		void JumpWithOffset (int offset);

		// -(void)jumpBackward:(double)interval;
		[Export ("jumpBackward:")]
		void JumpBackward (double interval);

		// -(void)jumpForward:(double)interval;
		[Export ("jumpForward:")]
		void JumpForward (double interval);

		// -(void)extraShortJumpBackward;
		[Export ("extraShortJumpBackward")]
		void ExtraShortJumpBackward ();

		// -(void)extraShortJumpForward;
		[Export ("extraShortJumpForward")]
		void ExtraShortJumpForward ();

		// -(void)shortJumpBackward;
		[Export ("shortJumpBackward")]
		void ShortJumpBackward ();

		// -(void)shortJumpForward;
		[Export ("shortJumpForward")]
		void ShortJumpForward ();

		// -(void)mediumJumpBackward;
		[Export ("mediumJumpBackward")]
		void MediumJumpBackward ();

		// -(void)mediumJumpForward;
		[Export ("mediumJumpForward")]
		void MediumJumpForward ();

		// -(void)longJumpBackward;
		[Export ("longJumpBackward")]
		void LongJumpBackward ();

		// -(void)longJumpForward;
		[Export ("longJumpForward")]
		void LongJumpForward ();

		// -(void)performNavigationAction:(VLCMediaPlaybackNavigationAction)action;
		[Export ("performNavigationAction:")]
		void PerformNavigationAction (VLCMediaPlaybackNavigationAction action);

		// -(BOOL)updateViewpoint:(float)yaw pitch:(float)pitch roll:(float)roll fov:(float)fov absolute:(BOOL)absolute;
		[Export ("updateViewpoint:pitch:roll:fov:absolute:")]
		bool UpdateViewpoint (float yaw, float pitch, float roll, float fov, bool absolute);

		// @property (nonatomic) float yaw;
		[Export ("yaw")]
		float Yaw { get; set; }

		// @property (nonatomic) float pitch;
		[Export ("pitch")]
		float Pitch { get; set; }

		// @property (nonatomic) float roll;
		[Export ("roll")]
		float Roll { get; set; }

		// @property (nonatomic) float fov;
		[Export ("fov")]
		float Fov { get; set; }

		// @property (readonly, getter = isPlaying, atomic) BOOL playing;
		[Export ("playing")]
		bool Playing { [Bind ("isPlaying")] get; }

		// @property (readonly, atomic) VLCMediaPlayerState state;
		[Export ("state")]
		VLCMediaPlayerState State { get; }

		// @property (atomic) double position;
		[Export ("position")]
		double Position { get; set; }

		// @property (readonly, getter = isSeekable, atomic) BOOL seekable;
		[Export ("seekable")]
		bool Seekable { [Bind ("isSeekable")] get; }

		// @property (readonly, atomic) BOOL canPause;
		[Export ("canPause")]
		bool CanPause { get; }

		// @property (readonly, copy, atomic) NSArray * _Nullable snapshots;
		[NullAllowed, Export ("snapshots", ArgumentSemantic.Copy)]
		//[Verify (StronglyTypedNSArray)]
		NSImage[] Snapshots { get; }

		// @property (readonly, atomic) NSImage * _Nullable lastSnapshot;
		[NullAllowed, Export ("lastSnapshot")]
		NSImage LastSnapshot { get; }

		// -(void)startRecordingAtPath:(NSString * _Nonnull)path;
		[Export ("startRecordingAtPath:")]
		void StartRecordingAtPath (string path);

		// -(void)stopRecording;
		[Export ("stopRecording")]
		void StopRecording ();

		// -(BOOL)setRendererItem:(VLCRendererItem * _Nullable)item;
		[Export ("setRendererItem:")]
		bool SetRendererItem ([NullAllowed] VLCRendererItem item);
	}

	// // @interface Tracks (VLCMediaPlayer)
	// [Category]
	// [BaseType (typeof(VLCMediaPlayer))]
	// interface VLCMediaPlayer_Tracks
	// {
	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaPlayerTrack *> * _Nonnull audioTracks;
	// 	[Export ("audioTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaPlayerTrack[] AudioTracks { get; }

	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaPlayerTrack *> * _Nonnull videoTracks;
	// 	[Export ("videoTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaPlayerTrack[] VideoTracks { get; }

	// 	// @property (readonly, copy, nonatomic) NSArray<VLCMediaPlayerTrack *> * _Nonnull textTracks;
	// 	[Export ("textTracks", ArgumentSemantic.Copy)]
	// 	VLCMediaPlayerTrack[] TextTracks { get; }

	// 	// -(void)deselectAllAudioTracks;
	// 	[Export ("deselectAllAudioTracks")]
	// 	void DeselectAllAudioTracks ();

	// 	// -(void)deselectAllVideoTracks;
	// 	[Export ("deselectAllVideoTracks")]
	// 	void DeselectAllVideoTracks ();

	// 	// -(void)deselectAllTextTracks;
	// 	[Export ("deselectAllTextTracks")]
	// 	void DeselectAllTextTracks ();
	// }

	// @interface VLCMediaPlayerChapterDescription : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaPlayerChapterDescription
	{
		// @property (readonly, nonatomic) VLCTime * _Nonnull timeOffset;
		[Export ("timeOffset")]
		VLCTime TimeOffset { get; }

		// @property (readonly, nonatomic) VLCTime * _Nonnull durationTime;
		[Export ("durationTime")]
		VLCTime DurationTime { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) int chapterIndex;
		[Export ("chapterIndex")]
		int ChapterIndex { get; }

		// @property (readonly, nonatomic) int titleIndex;
		[Export ("titleIndex")]
		int TitleIndex { get; }

		// @property (readonly, nonatomic) NSUrl * _Nullable mediaURL;
		[NullAllowed, Export ("mediaURL")]
		NSUrl MediaURL { get; }

		// @property (readonly, getter = isCurrent, nonatomic) BOOL current;
		[Export ("current")]
		bool Current { [Bind ("isCurrent")] get; }

		// -(void)setCurrent;
		[Export ("setCurrent")]
		void SetCurrent ();
	}

	// @interface VLCMediaPlayerTitleDescription : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCMediaPlayerTitleDescription
	{
		// @property (readonly, nonatomic) VLCTime * _Nonnull durationTime;
		[Export ("durationTime")]
		VLCTime DurationTime { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) VLCMediaPlayerTitleType titleType;
		[Export ("titleType")]
		VLCMediaPlayerTitleType TitleType { get; }

		// @property (readonly, copy, nonatomic) NSArray<VLCMediaPlayerChapterDescription *> * _Nonnull chapterDescriptions;
		[Export ("chapterDescriptions", ArgumentSemantic.Copy)]
		VLCMediaPlayerChapterDescription[] ChapterDescriptions { get; }

		// @property (readonly, nonatomic) int titleIndex;
		[Export ("titleIndex")]
		int TitleIndex { get; }

		// @property (readonly, nonatomic) NSUrl * _Nullable mediaURL;
		[NullAllowed, Export ("mediaURL")]
		NSUrl MediaURL { get; }

		// @property (readonly, getter = isMenu, nonatomic) BOOL menu;
		[Export ("menu")]
		bool Menu { [Bind ("isMenu")] get; }

		// @property (readonly, getter = isCurrent, nonatomic) BOOL current;
		[Export ("current")]
		bool Current { [Bind ("isCurrent")] get; }

		// -(void)setCurrent;
		[Export ("setCurrent")]
		void SetCurrent ();

		// -(void)navigateActivate;
		[Export ("navigateActivate")]
		void NavigateActivate ();

		// -(void)navigateUp;
		[Export ("navigateUp")]
		void NavigateUp ();

		// -(void)navigateDown;
		[Export ("navigateDown")]
		void NavigateDown ();

		// -(void)navigateLeft;
		[Export ("navigateLeft")]
		void NavigateLeft ();

		// -(void)navigateRight;
		[Export ("navigateRight")]
		void NavigateRight ();

		// -(void)navigatePopup;
		[Export ("navigatePopup")]
		void NavigatePopup ();
	}

	// @interface VLCMediaThumbnailer : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCMediaThumbnailer
	{
		// +(VLCMediaThumbnailer * _Nonnull)thumbnailerWithMedia:(VLCMedia * _Nonnull)media andDelegate:(id<VLCMediaThumbnailerDelegate> _Nonnull)delegate;
		[Static]
		[Export ("thumbnailerWithMedia:andDelegate:")]
		VLCMediaThumbnailer ThumbnailerWithMedia (VLCMedia media, VLCMediaThumbnailerDelegate @delegate);

		// +(VLCMediaThumbnailer * _Nonnull)thumbnailerWithMedia:(VLCMedia * _Nonnull)media delegate:(id<VLCMediaThumbnailerDelegate> _Nonnull)delegate andVLCLibrary:(VLCLibrary * _Nullable)library;
		[Static]
		[Export ("thumbnailerWithMedia:delegate:andVLCLibrary:")]
		VLCMediaThumbnailer ThumbnailerWithMedia (VLCMedia media, VLCMediaThumbnailerDelegate @delegate, [NullAllowed] VLCLibrary library);

		// -(void)fetchThumbnail;
		[Export ("fetchThumbnail")]
		void FetchThumbnail ();

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCMediaThumbnailerDelegate Delegate { get; set; }

		// @property (readwrite, nonatomic, weak) id<VLCMediaThumbnailerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readwrite, nonatomic) VLCMedia * _Nonnull media;
		[Export ("media", ArgumentSemantic.Assign)]
		VLCMedia Media { get; set; }

		// @property (assign, readwrite, nonatomic) CGImageRef _Nullable thumbnail;
		[NullAllowed, Export ("thumbnail", ArgumentSemantic.Assign)]
		CGImage Thumbnail { get; set; }

		// @property (assign, readwrite, nonatomic) CGFloat thumbnailHeight;
		[Export ("thumbnailHeight")]
		nfloat ThumbnailHeight { get; set; }

		// @property (assign, readwrite, nonatomic) CGFloat thumbnailWidth;
		[Export ("thumbnailWidth")]
		nfloat ThumbnailWidth { get; set; }

		// @property (assign, readwrite, nonatomic) float snapshotPosition;
		[Export ("snapshotPosition")]
		float SnapshotPosition { get; set; }
	}

	// @protocol VLCMediaThumbnailerDelegate
	[Protocol, Model]
    [BaseType (typeof (NSObject))]
	interface VLCMediaThumbnailerDelegate
	{
		// @required -(void)mediaThumbnailerDidTimeOut:(VLCMediaThumbnailer * _Nonnull)mediaThumbnailer;
		//[Abstract]
		[Export ("mediaThumbnailerDidTimeOut:")]
		void MediaThumbnailerDidTimeOut (VLCMediaThumbnailer mediaThumbnailer);

		// @required -(void)mediaThumbnailer:(VLCMediaThumbnailer * _Nonnull)mediaThumbnailer didFinishThumbnail:(CGImageRef _Nonnull)thumbnail;
		//[Abstract]
		[Export ("mediaThumbnailer:didFinishThumbnail:")]
		void MediaThumbnailer (VLCMediaThumbnailer mediaThumbnailer, CGImage thumbnail);
	}

	// @protocol VLCRendererDiscovererDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCRendererDiscovererDelegate
	{
		// @required -(void)rendererDiscovererItemAdded:(VLCRendererDiscoverer * _Nonnull)rendererDiscoverer item:(VLCRendererItem * _Nonnull)item;
		[Abstract]
		[Export ("rendererDiscovererItemAdded:item:")]
		void RendererDiscovererItemAdded (VLCRendererDiscoverer rendererDiscoverer, VLCRendererItem item);

		// @required -(void)rendererDiscovererItemDeleted:(VLCRendererDiscoverer * _Nonnull)rendererDiscoverer item:(VLCRendererItem * _Nonnull)item;
		[Abstract]
		[Export ("rendererDiscovererItemDeleted:item:")]
		void RendererDiscovererItemDeleted (VLCRendererDiscoverer rendererDiscoverer, VLCRendererItem item);
	}

	// @interface VLCRendererDiscovererDescription : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCRendererDiscovererDescription
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull longName;
		[Export ("longName")]
		string LongName { get; }

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name longName:(NSString * _Nonnull)longName;
		[Export ("initWithName:longName:")]
		NativeHandle Constructor (string name, string longName);
	}

	// @interface VLCRendererDiscoverer : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCRendererDiscoverer
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSArray<VLCRendererItem *> * _Nonnull renderers;
		[Export ("renderers", ArgumentSemantic.Copy)]
		VLCRendererItem[] Renderers { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCRendererDiscovererDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCRendererDiscovererDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nullable)initWithName:(NSString * _Nonnull)name;
		[Export ("initWithName:")]
		NativeHandle Constructor (string name);

		// -(BOOL)start;
		[Export ("start")]
		//[Verify (MethodToProperty)]
		bool Start ();

		// -(void)stop;
		[Export ("stop")]
		void Stop ();

		// +(NSArray<VLCRendererDiscovererDescription *> * _Nullable)list;
		[Static]
		[NullAllowed, Export ("list")]
		//[Verify (MethodToProperty)]
		VLCRendererDiscovererDescription[] List { get; }
	}

	// @interface VLCRendererItem : NSObject
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface VLCRendererItem
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull iconURI;
		[Export ("iconURI")]
		string IconURI { get; }

		// @property (readonly, assign, nonatomic) int flags;
		[Export ("flags")]
		int Flags { get; }
	}

	// // @interface VLCStreamOutput : NSObject
	// [BaseType (typeof(NSObject))]
	// interface VLCStreamOutput
	// {
	// 	// -(instancetype)initWithOptionDictionary:(NSDictionary *)dictionary __attribute__((objc_designated_initializer)) __attribute__((deprecated("")));
	// 	[Export ("initWithOptionDictionary:")]
	// 	[DesignatedInitializer]
	// 	NativeHandle Constructor (NSDictionary dictionary);

	// 	// +(instancetype)streamOutputWithOptionDictionary:(NSDictionary *)dictionary __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("streamOutputWithOptionDictionary:")]
	// 	VLCStreamOutput StreamOutputWithOptionDictionary (NSDictionary dictionary);

	// 	// +(id)rtpBroadcastStreamOutputWithSAPAnnounce:(NSString *)announceName __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("rtpBroadcastStreamOutputWithSAPAnnounce:")]
	// 	NSObject RtpBroadcastStreamOutputWithSAPAnnounce (string announceName);

	// 	// +(id)rtpBroadcastStreamOutput __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("rtpBroadcastStreamOutput")]
	// 	[Verify (MethodToProperty)]
	// 	NSObject RtpBroadcastStreamOutput { get; }

	// 	// +(id)ipodStreamOutputWithFilePath:(NSString *)filePath __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("ipodStreamOutputWithFilePath:")]
	// 	NSObject IpodStreamOutputWithFilePath (string filePath);

	// 	// +(instancetype)streamOutputWithFilePath:(NSString *)filePath __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("streamOutputWithFilePath:")]
	// 	VLCStreamOutput StreamOutputWithFilePath (string filePath);

	// 	// +(id)mpeg2StreamOutputWithFilePath:(NSString *)filePath __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("mpeg2StreamOutputWithFilePath:")]
	// 	NSObject Mpeg2StreamOutputWithFilePath (string filePath);

	// 	// +(id)mpeg4StreamOutputWithFilePath:(NSString *)filePath __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("mpeg4StreamOutputWithFilePath:")]
	// 	NSObject Mpeg4StreamOutputWithFilePath (string filePath);
	// }

	// // @interface VLCStreamSession : VLCMediaPlayer
	// [BaseType (typeof(VLCMediaPlayer))]
	// interface VLCStreamSession
	// {
	// 	// +(instancetype)streamSession __attribute__((deprecated("")));
	// 	[Static]
	// 	[Export ("streamSession")]
	// 	VLCStreamSession StreamSession ();

	// 	// @property (nonatomic, strong) VLCStreamOutput * streamOutput __attribute__((deprecated("")));
	// 	[Export ("streamOutput", ArgumentSemantic.Strong)]
	// 	VLCStreamOutput StreamOutput { get; set; }

	// 	// @property (readonly, nonatomic) BOOL isComplete __attribute__((deprecated("")));
	// 	[Export ("isComplete")]
	// 	bool IsComplete { get; }

	// 	// @property (readonly, nonatomic) NSUInteger reattemptedConnections __attribute__((deprecated("")));
	// 	[Export ("reattemptedConnections")]
	// 	nuint ReattemptedConnections { get; }

	// 	// -(void)startStreaming __attribute__((deprecated("")));
	// 	[Export ("startStreaming")]
	// 	void StartStreaming ();

	// 	// -(void)stopStreaming __attribute__((deprecated("")));
	// 	[Export ("stopStreaming")]
	// 	void StopStreaming ();
	// }

	// @interface VLCTime : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCTime
	{
		// +(VLCTime * _Nonnull)nullTime;
		[Static]
		[Export ("nullTime")]
		//[Verify (MethodToProperty)]
		VLCTime NullTime { get; }

		// +(VLCTime * _Nonnull)timeWithNumber:(NSNumber * _Nullable)aNumber;
		[Static]
		[Export ("timeWithNumber:")]
		VLCTime TimeWithNumber ([NullAllowed] NSNumber aNumber);

		// +(VLCTime * _Nonnull)timeWithInt:(int)aInt;
		[Static]
		[Export ("timeWithInt:")]
		VLCTime TimeWithInt (int aInt);

		// +(int64_t)clock;
		[Static]
		[Export ("clock")]
		// [Verify (MethodToProperty)]
		long Clock { get; }

		// +(int64_t)delay:(int64_t)ts;
		[Static]
		[Export ("delay:")]
		long Delay (long ts);

		// -(instancetype _Nonnull)initWithNumber:(NSNumber * _Nullable)aNumber;
		[Export ("initWithNumber:")]
		NativeHandle Constructor ([NullAllowed] NSNumber aNumber);

		// -(instancetype _Nonnull)initWithInt:(int)aInt;
		[Export ("initWithInt:")]
		NativeHandle Constructor (int aInt);

		// @property (readonly, nonatomic) NSNumber * _Nullable value;
		[NullAllowed, Export ("value")]
		NSNumber Value { get; }

		// @property (readonly) NSString * _Nonnull stringValue;
		[Export ("stringValue")]
		string StringValue { get; }

		// @property (readonly) NSString * _Nonnull verboseStringValue;
		[Export ("verboseStringValue")]
		string VerboseStringValue { get; }

		// @property (readonly) NSString * _Nonnull minuteStringValue;
		[Export ("minuteStringValue")]
		string MinuteStringValue { get; }

		// @property (readonly) int intValue;
		[Export ("intValue")]
		int IntValue { get; }

		// @property (readonly) NSString * _Nonnull subSecondStringValue;
		[Export ("subSecondStringValue")]
		string SubSecondStringValue { get; }

		// -(NSComparisonResult)compare:(VLCTime * _Nonnull)aTime;
		[Export ("compare:")]
		NSComparisonResult Compare (VLCTime aTime);

		// -(BOOL)isEqual:(id _Nullable)object;
		[Export ("isEqual:")]
		bool IsEqual ([NullAllowed] NSObject @object);

		// -(NSUInteger)hash;
		[Export ("hash")]
		nuint Hash { get; }
	}

	// @protocol VLCTranscoderDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface VLCTranscoderDelegate
	{
		// @optional -(void)transcode:(VLCTranscoder * _Nonnull)transcoder finishedSucessfully:(BOOL)success;
		[Export ("transcode:finishedSucessfully:")]
		void FinishedSucessfully (VLCTranscoder transcoder, bool success);
	}

	// @interface VLCTranscoder : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCTranscoder
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		VLCTranscoderDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<VLCTranscoderDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(BOOL)reencodeAndMuxSRTFile:(NSString * _Nonnull)srtPath toMP4File:(NSString * _Nonnull)mp4Path outputPath:(NSString * _Nonnull)outPath;
		[Export ("reencodeAndMuxSRTFile:toMP4File:outputPath:")]
		bool ReencodeAndMuxSRTFile (string srtPath, string mp4Path, string outPath);
	}

	// @interface VLCVideoLayoutManager : NSObject
	[BaseType (typeof(NSObject))]
	interface VLCVideoLayoutManager
	{
		// +(id _Nonnull)layoutManager;
		[Static]
		[Export ("layoutManager")]
		//[Verify (MethodToProperty)]
		VLCVideoLayoutManager LayoutManager { get; }

		// @property (nonatomic) BOOL fillScreenEntirely;
		[Export ("fillScreenEntirely")]
		bool FillScreenEntirely { get; set; }

		// @property (nonatomic) CGSize originalVideoSize;
		[Export ("originalVideoSize", ArgumentSemantic.Assign)]
		CGSize OriginalVideoSize { get; set; }
	}

	// @interface VLCVideoLayer : CALayer
	[BaseType (typeof(CALayer))]
	interface VLCVideoLayer
	{
		// @property (readonly, nonatomic) BOOL hasVideo;
		[Export ("hasVideo")]
		bool HasVideo { get; }

		// @property (nonatomic) BOOL fillScreen;
		[Export ("fillScreen")]
		bool FillScreen { get; set; }
	}

	// @interface VLCVideoView : NSView
	[BaseType (typeof(NSView))]
	interface VLCVideoView
	{
		// @property (copy, nonatomic) NSColor * _Nonnull backColor;
		[Export ("backColor", ArgumentSemantic.Copy)]
		NSColor BackColor { get; set; }

		// @property (readonly, nonatomic) BOOL hasVideo;
		[Export ("hasVideo")]
		bool HasVideo { get; }

		// @property (nonatomic) BOOL fillScreen;
		[Export ("fillScreen")]
		bool FillScreen { get; set; }
	}
}

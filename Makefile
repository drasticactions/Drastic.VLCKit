MAC_SDK=macosx15.1
IPHONE_SDK=iphoneos18.1
ROOT=$(PWD)
FRAMEWORKS=$(ROOT)/Frameworks
FRAMEWORKS_MAC=$(FRAMEWORKS)/mac
FRAMEWORKS_IOS=$(FRAMEWORKS)/ios
BINDING_PROJECT_MACOS=$(ROOT)/binding/mac
BINDING_PROJECT_IOS=$(ROOT)/binding/ios
MAC_HEADERS=$(FRAMEWORKS_MAC)/VLCKit.framework/Versions/A/Headers
IOS_HEADERS=$(FRAMEWORKS_MAC)/VLCKit.framework/Versions/A/Headers

pack:
	rm -rf nupkg
	mkdir nupkg
	dotnet pack src/VLCKit.iOS/VLCKit.iOS.csproj -c Release -o $(ROOT)/nupkg
	dotnet pack src/VLCKit.macOS/VLCKit.macOS.csproj -c Release -o $(ROOT)/nupkg

ios_xcframework:
	rm -rf $(FRAMEWORKS)/VLCKit.iOS.xcframework
	xcodebuild -create-xcframework -framework $(FRAMEWORKS)/tvos/VLCKit.framework -framework $(FRAMEWORKS)/tvossimulator/VLCKit.framework  -framework $(FRAMEWORKS)/ios/VLCKit.framework -framework $(FRAMEWORKS)/iossimulator/VLCKit.framework -output $(FRAMEWORKS)/VLCKit.iOS.xcframework

sharpie_mac:
	rm -rf $(BINDING_PROJECT_MACOS)
	sharpie bind --sdk=$(MAC_SDK) --output="$(BINDING_PROJECT_MACOS)" --namespace="VLCKit" --scope="$(MAC_HEADERS)" $(MAC_HEADERS)/*.h

sharpie_ios:
	rm -rf $(BINDING_PROJECT_IOS)
	sharpie bind --sdk=$(IPHONE_SDK) --output="$(BINDING_PROJECT_IOS)" --namespace="VLCKit" --scope="$(IOS_HEADERS)" $(IOS_HEADERS)/*.h
build:
	xbuild UnitySettings.csproj /verbosity:minimal /p:Configuration=Release
	cp bin/UnitySettings.dll lib
	cp bin/UnitySettings.dll.mdb lib
	cp bin/UnitySettings.dll example/Assets

unit:
	xbuild UnitySettings-Test.csproj /verbosity:minimal
	mono --debug bin/test/UnitySettings-Test.exe

copy_unity_dll:
	cp /Applications/Unity/Unity.app/Contents/Managed/UnityEngine.dll lib/UnityEngine.dll

all: build unit

build:
	msbuild UnitySettings.csproj /verbosity:minimal /p:Configuration=Release

unit:
	msbuild UnitySettings-Test.csproj /verbosity:minimal
	mono --debug bin/test/UnitySettings-Test.exe

copy_unity_dll:
	cp /Applications/Unity/Unity.app/Contents/Managed/UnityEngine.dll libs/UnityEngine.dll

all: build unit

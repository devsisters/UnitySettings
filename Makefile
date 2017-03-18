build:
	xbuild UnitySettings.csproj /verbosity:minimal /p:Configuration=Release
	cp bin/UnitySettings.dll lib
	cp bin/UnitySettings.dll.mdb lib

unit:
	xbuild UnitySettings-Test.csproj /verbosity:minimal
	mono --debug bin/test/UnitySettings-Test.exe

all: build unit

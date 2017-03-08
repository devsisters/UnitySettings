build:
	xbuild UnitySettings.csproj /verbosity:minimal
	cp bin/Debug/UnitySettings.dll lib
	cp bin/Debug/UnitySettings.dll.mdb lib

unit:
	xbuild UnitySettings-Test.csproj /verbosity:minimal
	mono --debug bin/Debug/UnitySettings-Test.exe

all: build unit

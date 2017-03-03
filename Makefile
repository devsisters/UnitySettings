build:
	xbuild UnityDashboard.csproj /verbosity:minimal
	cp bin/Debug/UnityDashboard.dll lib
	cp bin/Debug/UnityDashboard.dll.mdb lib

unit:
	xbuild UnityDashboard-Test.csproj /verbosity:minimal
	mono --debug bin/Debug/UnityDashboard-Test.exe

all: build unit

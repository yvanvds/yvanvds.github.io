// NativeWindowsApp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Counter.h"
#include "PlatformID.h"
#include <iostream>

using namespace std;
using namespace DemoTools;

int main()
{
	cout << "DemoTools output: " << endl;

	// check the platform
	PlatformID ID;
	cout << ID.Get() << endl;
	cout << endl;

	// check the main counter
	MainCounter() = MainCounter() + 10;
	cout << "This should be 10: " << MainCounter().Get() << endl;

	// add a new counter
	Counter c;
	c = c + 5;
	cout << "This should be 5: " << c.Get() << endl;

	// merge counters
	MainCounter() = MainCounter() + c;
	cout << "This should be 15: " << MainCounter().Get() << endl;

	// reset counter
	MainCounter().Reset();
	cout << "This should be 0: " << MainCounter().Get() << endl;

	cin.get();

	return 0;
}
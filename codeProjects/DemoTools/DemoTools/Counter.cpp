#include "Counter.h"

using namespace DemoTools;

Counter::Counter() : counter(0) {}

void Counter::Reset()
{
	counter = 0;
}

int Counter::Get() const
{
	return counter;
}

void Counter::Increase()
{
	counter++;
}

Counter & DemoTools::Counter::operator+(int value)
{
	counter += value;
	return *this;
}

Counter & DemoTools::Counter::operator+(const Counter & other)
{
	counter += other.counter;
	return *this;
}


Counter & DemoTools::MainCounter() {
	static Counter s;
	return s;
}
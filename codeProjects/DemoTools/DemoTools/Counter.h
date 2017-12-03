#pragma once
#include "PreProcessor.h"

namespace DemoTools {

	class API Counter
	{
	public:
		Counter();

		void Reset();
		void Increase();
		int  Get() const;

		Counter & operator+(int value);
		Counter & operator+(const Counter & other);

	private:
		int counter;
	};

	API Counter & MainCounter();
}
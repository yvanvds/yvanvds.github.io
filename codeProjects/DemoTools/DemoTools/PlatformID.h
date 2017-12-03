#pragma once

#include "PreProcessor.h"

namespace DemoTools {
	class PlatformIDImpl;

	class API PlatformID
	{
	public:
		PlatformID();
		~PlatformID();

		const char * Get();

	private:
		PlatformIDImpl * pimpl;
	};
}
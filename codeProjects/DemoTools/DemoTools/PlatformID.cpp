#include "PlatformID.h"
#include <string>

using namespace DemoTools;

namespace DemoTools {
	class PlatformIDImpl {
	public:
		std::string content;
	};
}

PlatformID::PlatformID() : pimpl(new PlatformIDImpl())
{
#ifdef _WINDOWS
	pimpl->content = "Running on Windows";
#elif defined _ANDROID
	pimpl->content = "Running on Android";
#elif defined _IOS
	pimpl->content = "Running on iOS (Urgh!)";
#else
	pimpl->content = "Running on undefined Platform";
#endif
}

PlatformID::~PlatformID()
{
	delete pimpl;
}

const char * PlatformID::Get()
{
	return pimpl->content.c_str();
}
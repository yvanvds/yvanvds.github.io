#pragma once

#ifdef _WINDOWS
#ifdef _USRDLL
#define API __declspec (dllexport)
#elif defined _IMPORTDEMOTOOLS 
#define API __declspec (dllimport)
#endif
#endif

#ifndef API
#define API
#endif
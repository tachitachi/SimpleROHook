#pragma once

void DebugLogger(const char* format, ...);
void DebugLoggerWithLogWindow(const char* format, ...);
void DebugLoggerForInit(const char* format, ...);

void DisableDebugLoggerForInit();

#define DEBUG_LOGGING_NORMAL(a) DebugLoggerWithLogWindow a
#define DEBUG_LOGGING_DETAIL(a) DebugLoggerForInit a
#define DEBUG_LOGGING_MORE_DETAIL(a) DebugLoggerForInit a

#ifndef DEBUG_LOGGING_NORMAL
	#define DEBUG_LOGGING_NORMAL(a)
#endif
#ifndef DEBUG_LOGGING_DETAIL
	#define DEBUG_LOGGING_DETAIL(a)
#endif
#ifndef DEBUG_LOGGING_MORE_DETAIL
	#define DEBUG_LOGGING_MORE_DETAIL(a)
#endif

void CreateTinyConsole(void);
void ReleaseTinyConsole(void);


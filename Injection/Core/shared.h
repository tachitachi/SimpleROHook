#pragma once

#define SHAREDMEMORY_OBJECTNAME _T("SimpleROHook1011")

enum COPYDATAENTRY{
	COPYDATA_NPCLogger = 127
};

typedef struct _StSHAREDMEMORY{
	HWND	g_hROWindow;

	DWORD	executeorder;


	BOOL	write_packetlog;
	BOOL	freemouse; 
	int		ground_zbias;
	int		alphalevel;
	BOOL	m2e;
	BOOL	bbe;
	BOOL	deadcell;
	BOOL	chatscope;
	BOOL	castrange;
	BOOL	fix_windowmode_vsyncwait;
	BOOL	show_framerate;
	BOOL	objectinformation;
	BOOL	_44khz_audiomode;
	int		cpucoolerlevel;
	BOOL	chainload;

	WCHAR	configfilepath[MAX_PATH];
	WCHAR	musicfilename[MAX_PATH];
	WCHAR	proxyfilepath[MAX_PATH];

}StSHAREDMEMORY;

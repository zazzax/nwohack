#include "Misc/stdafx.h"
#include "Main/DllMain.h"
#include "Main/Functions.h"

HINSTANCE g_mainInstance;
HANDLE g_mainThread;

/// <summary>
/// Dll execution entry point.
/// </summary>
BOOL APIENTRY DllMain( HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved )
{
	if( dwReason == DLL_PROCESS_ATTACH )
	{
		g_mainInstance = hInstance;
		DisableThreadLibraryCalls( g_mainInstance );

		HANDLE g_mainThread = CreateThread( nullptr, 0,
			reinterpret_cast<LPTHREAD_START_ROUTINE>( MainThread ),
			nullptr, 0, nullptr );

		if( g_mainThread != nullptr )
		{
			CloseHandle( g_mainThread );
			return TRUE;
		}
	}

	return FALSE;
}

/// <summary>
/// Main execution thread.
/// </summary>
BOOL WINAPI MainThread( LPVOID lpParam )
{
	std::wstring swCurrentPath = Functions::GetModuleFilePath( g_mainInstance );
	std::wstring swHackPath = swCurrentPath + L"Hack.dll";

	if( !Functions::FileExists( swHackPath ) )
	{
		MessageBoxA( nullptr, "Could not find \"Hack.dll\" for execution!", "Error!", MB_OK );
		FreeLibraryAndExitThread( g_mainInstance, EXIT_FAILURE );
		return EXIT_FAILURE;
	}

	DWORD dwResult = EXIT_FAILURE;

	ICLRRuntimeHost *pClrHost = { 0 };
	CorBindToRuntime( nullptr, L"wks", CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (PVOID*) &pClrHost );

	pClrHost->Start();
	pClrHost->ExecuteInDefaultAppDomain( swHackPath.c_str(), L"Hack.Program", L"Initialize", L"", &dwResult );
	
	return dwResult;
}
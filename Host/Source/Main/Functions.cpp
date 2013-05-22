#include "Misc/stdafx.h"
#include "Main/Functions.h"

namespace Functions
{
	/// <summary>
	/// Gets the specified module's file path.
	/// </summary>
	std::wstring GetModuleFilePath( const HMODULE hModule )
	{
		std::wstring swPath;
		swPath.resize( MAX_PATH );
		GetModuleFileName( hModule, &swPath[0], MAX_PATH );
		swPath.resize( swPath.find_last_of( L"\\" ) + 1 );
		
		return swPath;
	}

	/// <summary>
	/// Gets the specified directory's content.
	/// </summary>
	std::list<std::wstring> GetDirectoryFiles( const std::wstring &swDirectoryMask )
	{
		std::list<std::wstring> liResult;
		WIN32_FIND_DATAW findData = { 0 };

		HANDLE hFindFile = FindFirstFileW( swDirectoryMask.c_str(), &findData );
		if( hFindFile != INVALID_HANDLE_VALUE )
		{
			do
			{
				liResult.push_back( findData.cFileName );
			} while( FindNextFileW( hFindFile, &findData ) != FALSE );

			FindClose( hFindFile );
		}

		return liResult;
	}

	bool FileExists( const std::wstring &swFile )
	{
		int nTopDirectory = swFile.find_last_of( L"\\" ) + 1;

		std::wstring swFilePath = swFile;
		swFilePath.resize( nTopDirectory );

		std::wstring swFileName = std::wstring( &swFile[nTopDirectory] );

		std::list<std::wstring> liDirectoryFiles = GetDirectoryFiles( swFilePath + L"*" );
		for( auto iter = liDirectoryFiles.begin(), end = liDirectoryFiles.end(); iter != end; ++iter )
		{
			if( *iter == swFileName )
				return true;
		}
		
		return false;
	}
}
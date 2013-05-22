#pragma once

namespace Functions
{
	std::wstring GetModuleFilePath( const HMODULE hModule );
	std::list<std::wstring> GetDirectoryFiles( const std::wstring &swDirectoryMask );
	bool FileExists( const std::wstring &swFile );
}
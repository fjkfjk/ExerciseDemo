// HelloWDMApp.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <Windows.h>

int _tmain(int argc, _TCHAR* argv[])
{
	HANDLE hDevice = CreateFile(L"\\\\.\\HelloWDM",
		GENERIC_READ,
		0,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL);

	if (hDevice == INVALID_HANDLE_VALUE)
	{
		printf("Open file failed.\n");
	}
	else
	{
		printf("Open file succeed.\n");

		CloseHandle(hDevice);
	}

	return 0;
}


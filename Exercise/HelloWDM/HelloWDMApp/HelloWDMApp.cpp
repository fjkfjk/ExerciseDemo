// HelloWDMApp.cpp : �������̨Ӧ�ó������ڵ㡣
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


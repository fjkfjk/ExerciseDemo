// IPPLnTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "ipp.h"
#include "ipps.h"

#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <time.h>

#include <ppl.h>
#include "tbb/tbb.h"

IppStatus ln32f(void) {
	Ipp32f x[4] = { -1, (float)IPP_E, 0, (float)(exp(1.234567)) };
	IppStatus st = ippsLn_32f_I(x, 4);
	printf("Ln = %f,%f,%f,%f\n", x[0], x[1],x[2],x[3]);
	return st;
}

float SrcData[100000000];
float SrcData2[100000000];

float DstData[100000000];
float DstData2[100000000];

static void GenerateSrcData()
{
	for (int i = 0; i < sizeof(SrcData) / sizeof(SrcData[0]); i++)
	{
		SrcData[i] = (float)rand() * 255.0f / RAND_MAX;
	}

	memcpy_s(SrcData2, sizeof(SrcData2), SrcData, sizeof(SrcData));
}

static int StdLn()
{
	clock_t start, finish;

	start = clock();

	for (int i = 0; i < sizeof(SrcData) / sizeof(SrcData[0]); i++)
	{
		DstData[i] = log(SrcData[i]);
	}

	finish = clock();

	return finish - start;
}

static int PPL_StdLn()
{
	clock_t start, finish;

	start = clock();

	concurrency::parallel_for(0, (int)(sizeof(SrcData) / sizeof(SrcData[0])), [&](int i) {
		DstData[i] = log(SrcData[i]);
	});

	finish = clock();

	return finish - start;

}
static int TBB_StdLn()
{
	clock_t start, finish;

	start = clock();

	tbb::parallel_for(0, (int)(sizeof(SrcData) / sizeof(SrcData[0])), [&](int i) {
		DstData2[i] = log(SrcData[i]);
	});

	finish = clock();

	return finish - start;

}

static int StdLn_I()
{
	clock_t start, finish;

	start = clock();

	for (int i = 0; i < sizeof(SrcData) / sizeof(SrcData[0]); i++)
	{
		SrcData[i] = log(SrcData[i]);
	}

	finish = clock();

	return finish - start;
}
static int IppLn()
{
	clock_t start, finish;

	start = clock();

	ippsLn_32f(SrcData, DstData2, sizeof(SrcData) / sizeof(SrcData[0]));

	finish = clock();

	return finish - start;
}
static int IppLn_I()
{
	clock_t start, finish;

	start = clock();

	ippsLn_32f_I(SrcData2, sizeof(SrcData2) / sizeof(SrcData2[0]));

	finish = clock();

	return finish - start;
}
static int PPL_IppLn_I()
{
	clock_t start, finish;
	int size = sizeof(SrcData) / sizeof(SrcData[0]);

	start = clock();

	concurrency::parallel_for(0, 1, [&](int i) {
		ippsLn_32f_I(&SrcData[i*size / 2], size / 2);
	});

	finish = clock();

	return finish - start;
}

static int TBB_IppLn_I()
{
	clock_t start, finish;
	int size = sizeof(SrcData) / sizeof(SrcData[0]);

	start = clock();

	tbb::parallel_for(0, 1, [&](int i) {
		ippsLn_32f_I(&SrcData[i*size / 2], size/2);
	});

	finish = clock();

	return finish - start;
}

int main(int argc, char* argv[])
{
	const IppLibraryVersion *lib;
	Ipp64u fm;
	IppStatus status;

	status = ippInit();            //IPP initialization with the best optimization layer
	if (status != ippStsNoErr) {
		printf("IppInit() Error:\n");
		printf("%s\n", ippGetStatusString(status));
		return -1;
	}

	//Get version info
	lib = ippiGetLibVersion();
	printf("%s %s\n", lib->Name, lib->Version);

	//Get CPU features enabled with selected library level
	fm = ippGetEnabledCpuFeatures();
	printf("SSE    :%c\n", (fm >> 1) & 1 ? 'Y' : 'N');
	printf("SSE2   :%c\n", (fm >> 2) & 1 ? 'Y' : 'N');
	printf("SSE3   :%c\n", (fm >> 3) & 1 ? 'Y' : 'N');
	printf("SSSE3  :%c\n", (fm >> 4) & 1 ? 'Y' : 'N');
	printf("SSE41  :%c\n", (fm >> 6) & 1 ? 'Y' : 'N');
	printf("SSE42  :%c\n", (fm >> 7) & 1 ? 'Y' : 'N');
	printf("AVX    :%c\n", (fm >> 8) & 1 ? 'Y' : 'N');
	printf("AVX2   :%c\n", (fm >> 15) & 1 ? 'Y' : 'N');
	printf("----------\n");
	printf("OS Enabled AVX :%c\n", (fm >> 9) & 1 ? 'Y' : 'N');
	printf("AES            :%c\n", (fm >> 10) & 1 ? 'Y' : 'N');
	printf("CLMUL          :%c\n", (fm >> 11) & 1 ? 'Y' : 'N');
	printf("RDRAND         :%c\n", (fm >> 13) & 1 ? 'Y' : 'N');
	printf("F16C           :%c\n", (fm >> 14) & 1 ? 'Y' : 'N');

	ln32f();

	//LN函数性能测试
	srand(0);
	GenerateSrcData();

	int t = 0;

	//t = StdLn();
	//printf("StdLn Time = %d\n",t);

	//t = PPL_StdLn();
	//printf("PPL_StdLn Time = %d\n", t);

	//t = TBB_StdLn();
	//printf("TBB_StdLn Time = %d\n", t);

	//t = StdLn_I();
	//printf("StdLn_I Time = %d\n", t);

	//t = IppLn();
	//printf("IppLn Time = %d\n", t);

	//t = IppLn_I();
	//printf("IppLn_I Time = %d\n", t);

	t = PPL_IppLn_I();
	printf("PPL_IppLn_I Time = %d\n", t);

	//t = TBB_IppLn_I();
	//printf("TBB_IppLn_I Time = %d\n", t);

	//for (int i = 0; i < sizeof(DstData) / sizeof(DstData[0]); i++)
	//{
	//	if (fabs(DstData[i] - DstData2[i]) > 1e-6)
	//	{
	//		printf("Error: %d, %f, %f\n", i, DstData[i], DstData2[i]);
	//	}
	//}

	return 0;
}

/*
输出：
ippIP AVX2 (l9) 8.2.0 (r43166)
SSE    :Y
SSE2   :Y
SSE3   :Y
SSSE3  :Y
SSE41  :Y
SSE42  :Y
AVX    :Y
AVX2   :Y
----------
OS Enabled AVX :Y
AES            :Y
CLMUL          :Y
RDRAND         :Y
F16C           :Y
Ln = -1.#IND00,1.000000,-1.#INF00,1.234567
StdLn Time = 203
IppLn Time = 156
StdLn_I Time = 140
IppLn_I Time = 78
PPL_StdLn = 234
TBB_StdLn = 172(VC)/140(IC)
PPL_IppLn_I = 31(2, VC)/47(2,IC)
TBB_IppLn_I = 47(2,VC)/31(2, IC)/62(4, IC)/78(8, IC)
*/

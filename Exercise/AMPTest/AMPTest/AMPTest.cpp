// AMPTest.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

// AMPTest1.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#include <amp.h>
#include <iostream>

using namespace Concurrency;

void default_properties() {
	accelerator default_acc;

	std::wcout << "The default accelerator is " << default_acc.device_path << "\n";
	std::wcout << "dedicated_memory:" << default_acc.dedicated_memory << "\n";
	std::wcout << std::endl;
}

void list_all_accelerators()
{
	std::vector<accelerator> accs = accelerator::get_all();
	for (unsigned int i = 0; i < accs.size(); i++) {
		std::wcout << "Device: " << accs[i].device_path << "\n";
		std::wcout << "dedicated_memory:" << accs[i].dedicated_memory << "\n";
		std::wcout << (accs[i].supports_cpu_shared_memory ?
			"CPU shared memory: true" : "CPU shared memory: false") << "\n";
		std::wcout << (accs[i].supports_double_precision ?
			"double precision: true" : "double precision: false") << "\n";
		std::wcout << (accs[i].supports_limited_double_precision ?
			"limited double precision: true" : "limited double precision: false") << "\n";
		std::wcout << std::endl;
	}

	std::wcout << std::endl;
}

void pick_with_most_memory()
{
	std::vector<accelerator> accs = accelerator::get_all();
	accelerator acc_chosen = accs[0];
	for (unsigned int i = 0; i < accs.size(); i++) {
		if (accs[i].dedicated_memory > acc_chosen.dedicated_memory) {
			acc_chosen = accs[i];
		}
	}

	std::wcout << "The accelerator with the most memory is "
		<< acc_chosen.device_path << "\n";
}

int main()
{
	default_properties();
	list_all_accelerators();
	pick_with_most_memory();
}

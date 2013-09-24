#include <iostream>
#include <cstring>
#include <stdlib.h>
#include <string.h>

using namespace std;

int usage()
{
}

int main(int argc, char **argv)
{
	int report=0;
	//no input then output usages
	if (argc < 2)
	{
		usage();
	}
	
	string command = argv[1];
	argc --;
	argv ++;
	
	if (command == "set"){
		printf("set");
	}else if (command =="clean"){
		printf("clean");
	}else if (command =="add"){
		printf("add");
	}
	return report;
}

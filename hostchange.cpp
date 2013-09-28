/*---------------------------------
  This file is the main entrance 
  -------------------------------*/
#include <iostream>
#include <cstring>
#include <stdlib.h>
#include <string.h>
#include "data.h"

using namespace std;

int usage()
{
	cout<<"A small program to get hosts from Internet and change local hosts into those."<<endl;
	cout<<"[usage]:"<<endl;
	cout<<"		hostchange update"<<endl;
	cout<<"		hostchange set [hostname]"<<endl;
	cout<<"		hostchange clean"<<endl<<endl;
	cout<<"update	:	Get hosts file from Internet and storage it into local (/var/local/hostchange/data)."<<endl;
	cout<<"set	:	Change your hosts file into Internet's hosts."<<endl;
	cout<<"			Now avaliable hosts file: [smarthost_us smarthost_beijing imoutohost 9host]."<<endl;
	cout<<"clean	:	Clean your local hosts file."<<endl;
	cout<<"about	:	Show version and local hosts update dates."<<endl;
}

int usage_set()
{
	cout<<"select one host . please input \"hostchange set [hostname]\""<<endl;
	cout<<"1. smarthost_us"<<endl;
	cout<<"2. smarthost_beijing"<<endl;
	cout<<"3. imoutohost"<<endl;
	cout<<"4. 9host"<<endl;
}

int about(datafile a)
{
	cout<<"==============================================="<<endl;
	cout<<"===hostchange (linux version) version: 1.0.0==="<<endl;
	cout<<"==============================================="<<endl<<endl;
	cout<<"Provied by Hao Qiao(https://plus.google.com/u/0/109339235070869782796/posts). "<<endl;
	cout<<"Connect me at Google+ if you have any problem."<<endl<<endl;
	cout<<"Thanks to all of my friends."<<endl;
	cout<<"Thanks to Emptyhua (https://github.com/emptyhua). "<<endl<<endl;;
	cout<<"Hosts comes from:"<<endl;
	cout<<"----------------------------------------------"<<endl;
	cout<<"- 1.Smarthost: https://github.com/emptyhua "<<endl;
	cout<<"-     Smarthost_US update time:		"<<a.smarthost_us.substr(8,11)<<endl;
	cout<<"-     Smarthost_beijing update time:	"<<a.smarthost_beijing.substr(8,11)<<endl;
	cout<<"-"<<endl;
	cout<<"- 2.imoutohost: https://plus.google.com/u/0/100484131192950935968/posts"<<endl;
	cout<<"-     imoutohost update time:		"<<a.imoutohost.substr(135,135).substr(10,10)<<endl;
	cout<<"-"<<endl;
	cout<<"- 3.9host:  http://moe9.tk/Xction/9Hosts/"<<endl;
	cout<<"-     9host update time:		20"<<a.ninehost.substr(10,10).substr(0,8)<<endl;
	cout<<"----------------------------------------------"<<endl;
	cout<<"Thanks to them!"<<endl;
}


int main(int argc, char **argv)
{
	datafile mydata;
	int report=0;
	//no input then output usages
	if (argc < 2)
	{
		usage();
		return 0;
	}

	string command = argv[1];
	argc --;
	argv ++;

	if (command == "update"){
		mydata.loadfile();
		report=mydata.update();
		mydata.savefile();
	}else if (command =="set"){
		mydata.loadfile();
		if (argc < 2)
		{
			usage_set();
			return 0;
		}
		string name=argv[1];
		if (name=="smarthost_us"){
			mydata.sethosts(mydata.smarthost_us);
		}else if (name=="smarthost_beijing"){
			mydata.sethosts(mydata.smarthost_beijing);
		}else if (name=="imoutohost"){
			mydata.sethosts(mydata.imoutohost);
		}else if (name=="9host"){
			mydata.sethosts(mydata.ninehost);
		}else{
			usage_set();
			return 0;
		}
	}else if (command =="clean"){
		report=mydata.sethosts("");
	}else if (command =="about"){
		report=mydata.loadfile();
		about(mydata);
	}else if (command =="help"){
		usage();
	}else{
		cout<<"Error input.please input \"hostchange help\" for help"<<endl;
	}
	return report;
}

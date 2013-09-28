/*--------------------------------
  This is about the data file
  datafile save in /root/.hostchange/data
  ------------------------------*/
#include <iostream>
#include <fstream>
#include <string>
#include <list>
#include <sys/stat.h>
#include <sys/types.h>
#include <dirent.h>
#include <stdio.h>
#include <stdlib.h>
#include "data.h"
extern "C"
{
#include "http_client.h"
}

using namespace std;


//constructor
datafile::datafile()
{
	datapath="/var/local";
	datapath+="/hostchange/";
	dataname=datapath+"data";
	for(int i=0;i<datanumber;i++)length[i]=0;
	version=dv;
}
datafile::~datafile()
{
}
//reset function,clean all data.
bool datafile::reset()
{
	smarthost_beijing.clear();
	smarthost_us.clear();
	imoutohost.clear();
	ninehost.clear();
	for(int i=0;i<datanumber;i++)length[i]=0;
	version=dv;
	return true;
}

int datafile::savefile()
{
	/*=========search if the floder exits========*/
	DIR *dir=NULL;
	dir = opendir(datapath.c_str());
	if(dir == NULL)
	{
		clog<<"No floder found. Create the floder in user/share/hostchange/"<<endl;
		mkdir(datapath.c_str(), S_IRWXU | S_IRWXG |S_IRWXO);
	}else{
		closedir(dir);
	}
	/*==========save the data file=============*/
	ofstream file;
	file.open(dataname.c_str(),ios::binary);
	if(!file.is_open())
	{
		clog<<"Error open data file.Check if you run in root/sudo"<<endl;
		exit(2001);
	}
	file.write((char*)&version,sizeof(int));
	for(int i=0;i<datanumber;i++)file.write((char*)&length[i],sizeof(int));
	file.write(smarthost_us.c_str(),smarthost_us.size());
	file.write(smarthost_beijing.c_str(),smarthost_beijing.size());
	file.write(imoutohost.c_str(),imoutohost.size());
	file.write(ninehost.c_str(),ninehost.size());
	file.close();
	cout<<"Save file done"<<endl;
	return 1;
}
int datafile::checkfile()
{
	/*============load data file===========*/
	ifstream file;
	file.open(dataname.c_str(),ios::binary);
	if(!file.is_open())
	{
		return 2002;
	}
	version=-1;
	file.read((char*)&version,sizeof(int));
	if(version!=dv)
	{
		file.close();
		return 2003;
	}
	file.close();
	return 0;
}
int datafile::loadfile()
{
	int report=checkfile();
	reset();
	ifstream file;
	file.open(dataname.c_str(),ios::binary);
	file.read((char*)&version,sizeof(int));
	switch(report)
	{
		case 2002:
			clog<<"Error load localdatafile. Check if you are runing in root/sudo.If this is your first time to run the program.Please update first."<<endl;
			exit(2002);
			break;
		case 2003:
			clog<<"Error data file version. Please update"<<endl;
			exit(2001);
			break;
	}
	for(int i=0;i<datanumber;i++)file.read((char*)&length[i],sizeof(int));
	int cache=0;
	for(int i=0;i<datanumber;i++)if(length[i]>cache)cache=length[i];
	const int bufferlength=cache;
	char *buffer = new char[bufferlength];
	file.read(buffer,length[0]);
	smarthost_us+=buffer;
	file.read(buffer,length[1]);
	smarthost_beijing+=buffer;
	file.read(buffer,length[2]);
	imoutohost+=buffer;
	file.read(buffer,length[3]);
	ninehost+=buffer;
	file.close();
	delete buffer;
	return 0;
}

int datafile::update()
{
	if(checkfile()==0)loadfile();
	HttpClient *client;
	client=HttpClient_New();
	string url;
	const char *error=new char();
	/*================Download host===============*/
	//Smarthost_Beijing
	url="https://smarthosts.googlecode.com/svn/trunk/hosts";
	HttpClient_Init(client);
	HttpClient_Get(client,url.c_str());
	error=HttpClient_GetError(client);
	if(error!=0)
	{
		clog<<"SmartHost_Beijing Update Error"<<endl;
		clog<<error<<endl;
		//exit(3001);
	}else{
		cout<<"SmartHost_Beijing Update OK"<<endl;
		smarthost_beijing.clear();
		smarthost_beijing+=HttpClient_ResponseText(client);
	}
	//Smarthost_US
	url="https://smarthosts.googlecode.com/svn/trunk/hosts_us";
	HttpClient_Init(client);
	HttpClient_Get(client,url.c_str());
	error=HttpClient_GetError(client);
	if(error!=0)
	{   
		clog<<"SmartHost_Beijing Update Error"<<endl;
		clog<<error<<endl;
		//exit(3001);
	}else{
		cout<<"SmartHost_US Update OK"<<endl;
		smarthost_us.clear();
		smarthost_us+=HttpClient_ResponseText(client);
	}
	//imoutohost
	url="https://imoutohost.googlecode.com/git/imouto.host.txt";
	HttpClient_Init(client);
	HttpClient_Get(client,url.c_str());
	error=HttpClient_GetError(client);
	if(error!=0)
	{   
		clog<<"Imoutohost Update Error"<<endl;
		clog<<error<<endl;
		//exit(3001);
	}else{
		cout<<"Imoutohost Update OK"<<endl;
		imoutohost.clear();
		imoutohost+=HttpClient_ResponseText(client);
	}
	//ninehost
	url="http://moe9.tk/Xction/9Hosts/Static/Linux";
	HttpClient_Init(client);
	HttpClient_Get(client,url.c_str());
	error=HttpClient_GetError(client);
	if(error!=0)
	{   
		clog<<"9host Update Error"<<endl;
		clog<<error<<endl;
		//exit(3001);
	}else{
		cout<<"9host Update OK"<<endl;
		ninehost.clear();
		ninehost+=HttpClient_ResponseText(client);
	}
	/*=================================================*/
	length[0]=smarthost_us.size();
	length[1]=smarthost_beijing.size();
	length[2]=imoutohost.size();
	length[3]=ninehost.size();
	return 0;
}
int datafile::sethosts(string hosts)
{
	ofstream file;
	file.open("/etc/hosts");
	if(!file.is_open())
	{
		clog<<"Error open local hosts file. Check if you are root/sudo"<<endl;
		exit(4001);
	}
	file<<hosts<<endl;
	file<<"#generate by hostchange"<<endl;
	file.close();
	cout<<"Set OK"<<endl;
	return 0;
}

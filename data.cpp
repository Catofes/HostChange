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

using namespace std;

static int dv = 2;

class datafile
{
	public:
		
		int version;
		list<string> smarthost_beijing;
		list<string> smarthost_us;
		list<string> imoutohost;
		list<string> ninehost;
		
		datafile();
		~datafile();
		bool reset();
};
//constructor
datafile::datafile()
{
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
	version=dv;
}

class core {
	public:
		datafile thedata;
		int dataversion;
		string datapath;
		
		core();
		~core();
		int savedata();
		int loaddata();
		int sethost(list<string>);
		int update();
};

core::core()
{
	dataversion=dv;
	datapath="/root/.hostchange/data";
}

int core::savedata()
{
	//search the floder
	DIR *dir=NULL;
	dir = opendir("/root/.hostchange/");
	if(dir == NULL)
	{   
		cout<<"No floder found. Create the floder in /root/.hostchange/"<<endl;
		mkdir("/root/.hostchange/", S_IRWXU | S_IRWXG | S_IROTH | S_IXOTH);
	}else{
		closedir(dir);
	}   
	fstream file;
	file.open(datapath.c_str(),ios::binary);
	if(!file.is_open())cout<<"Error open data file.Check if you run in root/sudo"<<endl;
	else
	{

		cout<<"Save Data OK";
	}
}

int main()
{
	return 0;
}


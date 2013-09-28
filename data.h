/*-------------------datafile----------------*/

#include<string>

using namespace std;

const int dv = 2;//the version of the datafile
const int datanumber=4;//the number of the hosts source.


class datafile
{
	public:

		int version;//datafile version.
		int length[datanumber];//the datafile have $datanumber different hosts.
		/*============the host data=============*/
		string smarthost_us;
		string smarthost_beijing;
		string imoutohost;
		string ninehost;
		/*======================================*/
		string datapath;
		string dataname;

		datafile();
		~datafile();
		
		int checkfile();
		int savefile();
		int loadfile();
		int update();
		int about();
		int sethosts(string hosts);
		bool reset();
};


HostChange for Linux

Host 文件更新切换小程序

Host数据来源 
SmartHost (https://code.google.com/p/smarthosts/)
Imouto.host (https://code.google.com/p/imoutohost/)
9host (http://moe9.tk/Xction/9Hosts/)

运行需求： root 权限

安装说明： 

cd ~
mkdir hostchange
git clone git@github.com:Catofes/HostChange.git
cd hostchange
git checkout linuxversion
make
sudo make install

卸载说明：

make uninstall

使用说明：

[usage]:
		hostchange update
		hostchange set [hostname]
		hostchange clean

update	:	Get hosts file from Internet and storage it into local (/var/local/hostchange/data).
set	:	Change your hosts file into Internet's hosts.
			Now avaliable hosts file: [smarthost_us smarthost_beijing imoutohost 9host].
clean	:	Clean your local hosts file.
about	:	Show version and local hosts update dates.

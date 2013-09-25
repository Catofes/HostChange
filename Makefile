all:hostchange

hostchange:
	g++ -c hostchange.cpp -o hostchange.o
	g++ -c data.cpp -o data.o
	gcc -c http_client.c -o http_client.o
	g++ data.o http_client.o hostchange.o -o hostchange -lcurl -lm
	rm data.o http_client.o hostchange.o -f
	chmod 755 hostchange
install:
	rm /var/local/hostchange -rf
	rm /usr/bin/hostchange -f
	mkdir /var/local/hostchange
	chmod 777 /var/local/hostchange
	cp hostchange /usr/bin/
uninstall:
	rm /var/local/hostchange -rf
	rm /usr/bin/hostchange -f
clean:
	rm ./hostchange


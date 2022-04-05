# NitroSocket
The high performance socket library

Welcome to the NitroSocket wiki!

The high performance socket library Include samples, simulators, and tools to help test/QA

This is a project to help developers using sockets connections.

We have the class TCPConnection and UDPConnection, with the messageCutter Interface can receive hundreds messages per second.

TCPConnection can keep connections alive, manager new connections and close others. Using Semaphore and Thread to do the job.

TCPConnection can managers and send acknowledgment message, just by put in the array list the ack message, and set to true the ack message property.

The MessageCutter is the class know how the message begins and ends, cut, separated, discard any wrongs messages and put the result in a List.

SNMPConnection can manager snmp messages, GET, REQUESTS, Trap's, SNMPv1 & SNMPv2c & SNMPv3. Convert OID and Values to a HashTable with key and value and put the IP inside as OID. (please any question let me know, this is a complex part and I am still working in a version with more docs and simulators to help.

Simulator you can use to simulate the clients, send a thousands messages, is good for test, QA SimulatorReceive is to you check the code, how its works, is good for test, QA also.

Please any questions, problems send a message at andrenitroweb@gmail.com. I will be glad to help you.

Andre Fernandes andrenitroweb@gmail.com

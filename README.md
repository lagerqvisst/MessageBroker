This application integrates a RabbitMQ server, an API to generate random users, and a SQL database through Entity Framework. 
See youtubevideo for live demo and more details about the project. 

The purpose of this project is to learn and explore about messagebrokers. 

The workflow below shows how the program simulates multiple user sign ups to a service. The RabbitMQ server receives 60 new users. 
Through two receving servers with simulated variant latancy, the new users are recevied and processed by being added to a local SQL server. 

1
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/f557f4e9-74a8-4a0d-ac21-e15ad7f249aa)


2
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/6666bd23-c6c8-4150-992e-96b8d3c1f067)



3
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/d104d692-df65-471f-904f-6da3259eee21)



4
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/d5f89658-ca3f-4f69-b77f-80e8ec201b17)



5
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/2f7f855e-1547-4b84-a84f-081d1d59374f)


6
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/6cdc4c11-d4fc-46ac-869e-88003b78afc1)


7
![image](https://github.com/lagerqvisst/MessageBroker/assets/108764890/aa04c275-49ef-471d-ad8b-1bc0f2c97749)

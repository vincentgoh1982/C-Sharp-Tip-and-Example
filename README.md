<p align="center">
  <img height="200" src="icon.png" alt="Vincent logo">
  <h1 align="center">C-Sharp-Tip-and-Example</h1>
</p>
<p align="center">Description: For easy to understand by using example and grab code instantly. For Learning purpose.
</p>

---

## Table of Contents
- [RuntimeInitializeOnLoadMethodAttribute](#runtimeInitializeOnLoadMethodAttribute)
- [Delegate Event Handler](#delegate-event-handler)
- [Coroutines and Async](#coroutines-and-async)
- [Interface](#interface)
- [AMVC](#amvc)
- [Mirror](#mirror)
- [OSM Data](#osm-data)
- [Combine Unity Meshes](#combine-unity-meshes)
- [Object Pooling](#object-pooling)
- [Work with TCP Sockets in Python](#work-with-tcp-sockets-in-python)
- [Unlock all the extra CPU power](#unlock-all-the-extra-cpu-power)
- [Link all material to common material](#link-all-material-to-common-material)
- [Serilog](#serilog)
- [UDP](#udp-network)
- [Queue and Addressable](#queue-and-addressable-unity)
- [Script Execution Order](#script-execution-order-unity)
- [Json within Json](#json-within-Json)
---
## Json within Json

### Description: 
Create Json with a type of animal information and a string of Json inside. The string of json contain different properties of the animal's attribute. One example is the generating the json which contains json string. The other example is to read the json's object to find the type of animal and convert the json string into object according to the type of animal.

### Description:
Link: | Description:
------------ | -------------
[GenerateJsonFile.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/JsonWithinJson/GenerateJsonFile.cs)|Generate the json's properties and save it into files.
[Model_Animal.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/JsonWithinJson/Model_Animal.cs)|Animal's properties class.
[ReadJsonFile.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/JsonWithinJson/ReadJsonFile.cs)|Read the json's properties from files.
---

## Script Execution Order unity

### Description: 
Executes the list of script from top to bottom and start individual scripts in a loop while still allowing each script to have its own custom initialization logic.

### Description:
Link: | Description:
------------ | -------------
[BaseScript.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Virtual_Base/BaseScript.cs)|BaseScript inherit from MonoBehaviour so that it can be attached to GameObjects.
[CatScript.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Virtual_Base/CatScript.cs)|CatScript inherit from the BaseScript and override the MonoBehaviourVirtualStart method with their own implementation.
[DogScript.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Virtual_Base/DogScript.cs)|DogScript inherit from the BaseScript and override the MonoBehaviourVirtualStart method with their own implementation.
[ScriptManager.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Virtual_Base/ScriptManager.cs)|ScriptManager is a manager script that will handle the loop to start each script.
---
## Queue and Addressable unity

### Description: 
The C# queue is a FIFO queue collection class in the System. Collection namespace. Storing elements in FIFO style (First In, First Out), the C# queue contains elements in precisely the order they were added, with objects added at one end and removed from the opposite end. Addressable enable loading assets on-demand, reducing initial loading times and memory usage. Assets can be updated independently, and only the necessary changes are downloaded, minimizing the size of updates for players. Assets can be hosted remotely, allowing developers to deliver updates without requiring users to download an entire game package. Addressable provide a way to manage asset dependencies, allowing developers to load assets and their associated resources efficiently.
(https://www.linkedin.com/pulse/asset-bundle-vs-addressable-gamecrio/

### How to load addressable assets from a remote server:
(https://gamedevbeginner.com/addressable-assets-in-unity/)
1. Create a new Addressables Asset profile, where the Remote Load Path is the location of your files.
2. Select Build Remote Catalog in the addressable system settings.
3. If you will need to update your built content, not replace it. To do this, instead of building new asset data, select Update a Previous Build.

![Image of Queue](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Queue_system.PNG)
![Image of Addressable unity](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Addressable_Group.PNG)
![Image of Addressable unity](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/AddressablesIP_Address.png)
![Image of Addressable unity](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/AddressablesRemoteSetting.png)


### Description:
Link: | Description:
------------ | -------------
[FakeJsonData.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Queue_System/FakeJsonData.cs)|FakeJsonData is replicate a server sending json file to the device.
[Queue_system.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Queue_System/Queue_system.cs)|Manage all the elements or gameobject's action in FIFO style. Addressables asset will load from the remote server at the start of the application.
[Vehicle_Movement.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Queue_System/Vehicle_Movement.cs)|Moving gameobject in a queue system (FIFO style).
[Model_Vehicle.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Queue_System/Model_Vehicle.cs)|Vehicle's properties.
[ChangeAddressablesIPAddress.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example?tab=readme-ov-file#queue-and-addressable-unity)|Change the addressable remote ip address on realtime from reading the json file.
---

---
## UDP Network

### Description: 
UDP continues to be a staple for online operations. This is because it is ideal for real-time data transmissions, where the loss of a few packets does not matter. TCP will take cognizance if a single packet is lost. It will restart the connection and retransmit the data, which will freeze the game. 

![Image of UDP Interface](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/UDP.PNG)

### Description:
Link: | Description:
------------ | -------------
[ChatPacket.css](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/ChatPacket.cs)|ChatPacket is a model class which added public properties for PACKET_TYPE and message.
[PrintStringEventArgs.c](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/PrintStringEventArgs.cs)|PrintStringEventArgs is an event handler which used to update messages to the UI.
[UDPAsynchronousChatClient.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/UDPAsynchronousChatClient.cs)|UDPAsynchronousChatClient is an asynchronous(non blocking) udp controller which the application is not block during the network IO and it will stays responsive for all messages from different users. When handling the completion event of SocketAsyncEventArgs, instead of directly calling Unity API methods, enqueue the action using the dispatcher.
[UDPChatGeneral.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/UDPChatGeneral.cs)|UDPChatGeneral check the eventhandler.
[UDPInteractiveBtn.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/UDPInteractiveBtn.cs)|Unity interface button and text.
[UDP_Server_Client_code folder](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/UDP_Server_Client_code)|Visual studio code for client and server. 
[Publish_UDP_Server folder](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/Publish_UDP_Server)|Published UDP Server application for testing.
[UnityMainThreadDispatcher.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/UDP_Client_Chat/UnityMainThreadDispatcher%20.cs)|A dispatcher allows you to queue actions to be executed on Unity's main thread.
---
## Serilog

### Description: 
Serilog uses what are called sinks to send your logs to a text file, database, or log management solutions, or potentially dozens of other places, all without changing your code. It will store individual days of error, which is easy for developer to debug the problem.

### Order of execution for event functions:
![Image of execution for event function](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/LogsButtons.png)

### Generated Log Files:
![Generated Log Files](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Logs.png)

Link: | Description:
------------ | -------------
[Serilog_ExampleScript.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Serilog/Serilog_ExampleScript.cs)| To generate an error and record it into text file.

---
## RuntimeInitializeOnLoadMethodAttribute

### Description:
RuntimeInitializeOnLoadMethodAttribute executed after Awake.

### Order of execution for event functions:
![Image of execution for event function](https://docs.unity3d.com/uploads/Main/monobehaviour_flowchart.svg)

Link: | Description:
------------ | -------------
[InitializeLoad.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Initialize/InitializeLoad.cs)| Allow a runtime class method to be initialized when a game is loaded at runtime without action from the user. Add any "Service" component to the prefab. 
  Null | Examples: Input, Saving, Sound, Config, Asset Bundles, Advertisements

## Delegate Event Handler

### Description:
Events are very useful in developing loosely coupled system. An event based system has two parts:

Title: | Description:
------------ | -------------
Publisher| responsible for creating and invoking events.
Subscriber| responsible for subscribing the events. When an event is invoked then all the subscribed method are also invoked.

Link: | Description:
------------ | -------------
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/DelegateHandlerPublisher_PressHere.png)| 1. Press the button to view the result. 
[DelegateHandler.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Delegate_event/DelegateHandler.cs) | A delegate is an object which refers to a method or you can say it is a reference type variable that can hold a reference to the methods. Delegates in C# are similar to the function pointer in C/C++. It provides a way which tells which method is to be called when an event is triggered.
[ObjectController.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Delegate_event/ObjectController.cs) | Event is triggered, which calls the function.

---

## Coroutines and Async

### Description:
Coroutines are useful for executing methods over a number of frames.
Async methods are useful for executing methods after a given task has finished.
Async methods can commonly used to wait for I/O operations to complete.
Coroutines can be used to move an object each frame.

Link: | Description:
------------ | -------------
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Async.png) | 1. Press the button to view the result.
[Async.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Coroutine_Async/Async.cs)| help to divide your logic into awaitable tasks, where you can perform some long running operations such as reading large file, doing an API call, downloading a resource from web or performing a complex calculation without having to block the execution of your application on UI or service.
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Coroutines.png)| 1. Press the button to view the result.
[Coroutine.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Coroutine_Async/Coroutine.cs)|used in Unity to stop the execution until sometime or certain condition is met, and continues from where it had left off."

---

## Interface

### Description:
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/diagram_interface.png)

An Interface provides a contract specifying how to create an Object, without caring about the specifics of how they do the things. An Interface is a reference type and it included only abstract members such as Events, Methods, Properties etc. and it has no implementations for any of its members.

Link: | Description:
------------ | -------------
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Normal.png)| 1. Press the button to view the result. 
[Normal](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/Assets/Project/Script/C%23%20Interface/Normal) | The Normal method: If user want to send them SMS instead of E-mail or both. Need to change the method inside the class, it need to be recompiled and redeployed it.
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Interface.png)| 1. Press the button to view the result. 
[Interface](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/Assets/Project/Script/C%23%20Interface/Interface)| To reduce the impact of change in our software. Using Open for extension and Closed for modification prinicple(OCP). Interface is just a role in a contract(Open for extension).

---

## AMVC
### Description:
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/diagram_AMVC.png)

The Model-View-Controller pattern (MVC) splits the software into three major components: Models (Data CRUD), Views (Interface/Detection) and Controllers (Decision/Action). MVC is flexible enough to be implemented even on top of ECS or OOP.

![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/AMVC_List.png)

Link: | Description:
------------ | -------------
[BounceApplication.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Application/BounceApplication.cs)| Single entry point to your application and container of all critical instances and application-related data.
[BounceController.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Controller/BounceController.cs)| Controllers (Decision/Action) to controls the application workflow.
[BounceModel.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Model/BounceModel.cs)| Models (Data CRUD) contains all data related to the application.
[BounceView.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/View/BounceView.cs)| Views (Interface/Detection) contains all views related to the application.
[BallView.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/View/BallView.cs)| Views to describes the Ball view and its features.

---

## Mirror
### Description: 
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/PhotonVsUnet.png)

Mirror is successor to UNET API, but uses TCP. It makes testing and small LAN games convenient. 

Link: | Description:
------------ | -------------
[SceneApplication.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Application/SceneApplication.cs)| Single entry point to your application and container of all critical instances and application-related data.
[PlayerController.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/PlayerController.cs)| Player's information share with the server
[SceneScript.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/SceneScript.cs)| A scene networked object all can access and adjust.
[SceneReference.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/SceneReference.cs)| NetworkIdentity scene object gets disabled, as they are disabled until a player is in ‘ready’ status (ready status is usually set when player spawns).The workaround is to have the GameObject.Find() get the non-networked scene object, which will have those Network Identity scene object as pre-set variables. "sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;"
[ChatBehavior.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/ChatBehavior.cs)| Chat script which require to hide the Canvas until the method OnStartAuthority()

### Mirror API Library:  
API: | Description:
------------ | ------------- 
[Command]|It tells the server what a client wants to change.
[ClientRpc]|It tells the clients what the server has decided to do.
[SyncVar(hook = nameof(Method))]|It fires for changed values
ClientCallback|It inform the user that the server is not active.   
[SyncVar]|It automatically synchronize variables from Server->Client.
OnStartLocalPlayer|It is called on the machine that is the local player.
OnStartClient|It called by the NetworkManager and it works like any other hook.
OnStartAuthority |It called on clients for behaviours that have authority, based on context and hasAuthority.
[Client]|It informs the user that the server is not active. Client-only code
  
## OSM Data
### Description: 
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/OSMDataTree.png)
  
Open Street Map(OSM) as a gigantic database of all the things in the world.
  
Link: | Description:
------------ | -------------  
[Application_Tree.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Application/Application_Tree.cs)| Single entry point to your application and container of all critical instances and application-related data.
[Controller_OSMRequestArea.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Controller/Controller_OSMRequestArea.cs)| Get information from OSM data and plant the tree according to the OSM data and green color of the tile.
[I_OSMInterface.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Controller/I_OSMInterface.cs)| To reduce the impact of change in our software. Using Open for extension and Closed for modification prinicple(OCP). Interface is just a role in a contract(Open for extension).
[I_ColorTileTree.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Controller/I_ColorTileTree.cs)| Create a list of longitiude and latitude value within the map.
[I_OSMTree.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Controller/I_OSMTree.cs)| Create a list of longitiude and latitude value from OSM data of tree.
[OSMAddInterface.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Controller/OSMAddInterface.cs)| Get a list of interface scripts.  
[Model_Tree.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/Model/Model_Tree.cs)| Models (Data CRUD) contains all data related to the application.
[View_Tree.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/OSM_Data/View/View_Tree.cs)| Views (Interface/3D objects) contains all views related to the application.
 ![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/OSMData.png)| 1. Press the button to loading tree. 2. Press the button to stop loading tree. 
 
## Combine Unity Meshes
### Description: 
Simplify huge number of game objects which are generated by data or code. To make it into a smaller hierarchies which benefit from multithreading to refresh the Transforms in your scene. Complex hierarchies incur unnecessary Transform computations and more cost to garbage collection.
  
Link: | Description:
------------ | -------------   
[Application_CombineMeshes.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Combine_Meshes_Export_Mesh/Application/Application_CombineMeshes.cs)| Single entry point to your application and container of all critical instances and application-related data.
[Controller_CombineMeshes.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Combine_Meshes_Export_Mesh/Controller/Controller_CombineMeshes.cs)| Convert all the building mesh into one building mesh.
[Model_CombineMeshes.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Combine_Meshes_Export_Mesh/Model/Model_CombineMeshes.cs)| Models (Data CRUD) contains all data related to the application.
[View_CombineMeshes.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Combine_Meshes_Export_Mesh/View/View_CombineMeshes.cs)| (Interface/3D objects) contains all views related to the application.
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/CombineMeshes.png)| 1. Press Button Combine Mesh to combine all the building meshes into one building mesh
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Export%20FBX.png)| 1. Go to Assets menu -> FBX Exporter -> and you have three options (described below). This code comes from other programmer.

---
  
## Object Pooling
### Description:  
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/NonVsPooling.png)  
Using object pooling for ephemeral objects is faster than creating and destroying them, because it makes memory allocation simpler and removes dynamic memory allocation overhead and Garbage Collection, or GC.
 
Link: | Description:
------------ | -------------   
[Application_Pooling.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Pooling/Application/Application_Pooling.cs)| Single entry point to your application and container of all critical instances and application-related data.  
[Controller_Pooling.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Pooling/Controller/Controller_Pooling.cs)|Reusing game object from a “pool”
[Model_Pooling.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Pooling/Model/Model_Pooling.cs)| Models (Data CRUD) contains all data related to the application.
[View_Pooling.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Pooling/View/View_Pooling.cs)| (Interface/3D objects) contains all views related to the application.  
[Ball_Pooling.prefab](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Art/Prefab/Ball_Pooling.prefab)| Prefab for bullets
[Application_Instantiate.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Instantiate/Application/Application_Instantiate.cs)| Single entry point to your application and container of all critical instances and application-related data.  
[Controller_Instantiate.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Instantiate/Controller/Controller_Instantiate.cs)| Creating new objects and destroying old ones during gameplay.
[Model_Instantiate.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Instantiate/Model/Model_Instantiate.cs)| Models (Data CRUD) contains all data related to the application.
[View_Instantiate.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Object_Instantiate/View/View_Instantiate.cs)| (Interface/3D objects) contains all views related to the application.  
[Ball_Instantiate.prefab](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Art/Prefab/Ball_Instantiate.prefab)| Prefab for bullets 

---
  
## Work with TCP Sockets in Python 
### Description: 
A network socket is an endpoint of an interprocess communication across a computer network. The Python Standard Library has a module called socket which provides a low-level internet networking interface. This interface is common across different programming languages since it uses OS-level system calls. 

### Socket methods: 
Socket method: | Description:
------------ | -------------   
bind(), listen() and accept()| They are specific for server sockets. 
connect()|It is specific for client sockets. 
send() and recv()|They are common for both types.

### Python server and client:
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/PythonServer.png) 

The socket library is a low-level networking interface that allows us to make a network endpoint for two way communication.
 
Link: | Description:
------------ | -------------   
[FinalServer.py](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Python_Communication_server/Python_server/FinalServer.py)| TCP-socket server show the server's ip address and the server's port number to user. When there is client connected to the server, it will show the client's ip address and the client's port number.
[FinalClient.py](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Python_Communication_server/Python_server/FinalClient.py)| TCP-socket client
 
### C# unity: 
Link: | Description:
------------ | -------------   
[Application_ServerResponse.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Python_Communication_server/Application/Application_ServerResponse.cs)|Single entry point to your application and container of all critical instances and application-related data.  
[Controller_ServerResponse.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Python_Communication_server/Controller/Controller_ServerResponse.cs)|Connect to the TCP-socket server and send message
[Model_ServerResponse.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Python_Communication_server/Model/Model_ServerResponse.cs)| IP address and Port number for the server

---
  
## Unlock all the extra CPU power
### Description:  
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/MainThreadCommand.png) 

The Unity API can mostly only be used from the main thread. Provides an efficient allocation free async/await integration for Unity. To unlock all that extra CPU power!


### Worker Thread to Main Thread:
 Link: | Description:
------------ | -------------
[Application_MainThread.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Call_Function_in_Main_Thread/Application/Application_MainThread.cs)|Single entry point to your application and container of all critical instances and application-related data.
[Controller_Network.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Call_Function_in_Main_Thread/Controller/Controller_Network.cs)|Connect to the TCP-socket server and send message or receive message
[Controller_OtherToMainThread.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Call_Function_in_Main_Thread/Controller/Controller_OtherToMainThread.cs)|From worker thread to main thread to pan the map according to the information received.
 ![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/MainThreadCommand2.png)
[Model_Network.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Call_Function_in_Main_Thread/Model/Model_Network.cs)|IP address and Port number for the server and lerp duration value for animation.
[View_MainThread.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Call_Function_in_Main_Thread/View/View_MainThread.cs)|Online map 3D game object

---
  
## Link all material to common material
### Description:  
A Material_to_material.cs unity editor help to link the gameobject's material to the common material inside the resources folder. So that all the GameObjects are sharing the same Material can be batched together. Therefore, if you want to achieve good batching, you should aim to share Materials among as many different GameObjects as possible. Batching is the act of grouping tasks together, so you do them all at once, instead of switching between tasks that take place in different programs or areas. Please refer to the Unity Manual regardling [Draw call Batching](https://docs.unity3d.com/Manual/DrawCallBatching.html).

![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/MaterialToCommonMaterial.png) 

Step: | Description:
------------ | -------------
1|Go to Window and find materialDetector.
2|Click the materialDetector, the unity editor will appear.
3|Create a new folder for the new FBX and select the folder
4|Click "Create material and texture folder" button to generate the material and texture folder.
5|Drag the new FBX into the unity scene
6|Drag the new FBX toward the "2. Drag the game object to here." information
7|It will shows all the gameobject's information and number of gameobject's child.
8|Click "Linking individual material to common material" button to link all the material to the gameobject.
  
Link: | Description:
------------ | -------------
[Material_to_material.cs](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Editor/Material_to_material.cs)|An unity editor help to link according from the gameobject's material to the common material inside the resources folder.
[Final_Toyota_Bus_Material.fbx](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Art/MiniBus/Final_Toyota_Bus_Material.fbx)|Example 3D model which shows a gameobject contains many material in itself.
[LinkMaterials.unity](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Scene/12_Link_Multi_Materials/LinkMaterials.unity)|Example scene.
---

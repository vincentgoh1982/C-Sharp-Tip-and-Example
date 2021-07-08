<p align="center">
  <img height="200" src="icon.png" alt="Vincent logo">
  <h1 align="center">C-Sharp-Tip-and-Example</h1>
</p>
<p align="center">Description: For easy to understand by using example and grab code instantly.
</p>

---

## Table of Contents
- [RuntimeInitializeOnLoadMethodAttribute](#runtimeInitializeOnLoadMethodAttribute)
- [Delegate Event Handler](#delegate-event-handler)
- [Coroutines and Async](#coroutines-and-async)
- [Interface](#interface)
- [AMVC](#amvc)
- [Mirror](#mirror)

---

## RuntimeInitializeOnLoadMethodAttribute

### Description:
RuntimeInitializeOnLoadMethodAttribute executed after Awake.

### Order of execution for event functions:
![Image of execution for event function](https://docs.unity3d.com/uploads/Main/monobehaviour_flowchart.svg)

Link: | Description:
------------ | -------------
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Initialize/InitializeLoad.cs | Allow a runtime class method to be initialized when a game is loaded at runtime without action from the user. Add any "Service" component to the prefab. 
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
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Delegate_event/DelegateHandler.cs | A delegate is an object which refers to a method or you can say it is a reference type variable that can hold a reference to the methods. Delegates in C# are similar to the function pointer in C/C++. It provides a way which tells which method is to be called when an event is triggered.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Delegate_event/ObjectController.cs | Event is triggered, which calls the function.

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
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Coroutine_Async/Async.cs| help to divide your logic into awaitable tasks, where you can perform some long running operations such as reading large file, doing an API call, downloading a resource from web or performing a complex calculation without having to block the execution of your application on UI or service.
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Coroutines.png)| 1. Press the button to view the result.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Coroutine_Async/Coroutine.cs|used in Unity to stop the execution until sometime or certain condition is met, and continues from where it had left off."

---

## Interface

### Description:
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/diagram_interface.png)

An Interface provides a contract specifying how to create an Object, without caring about the specifics of how they do the things. An Interface is a reference type and it included only abstract members such as Events, Methods, Properties etc. and it has no implementations for any of its members.

Link: | Description:
------------ | -------------
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Normal.png)| 1. Press the button to view the result. 
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/Assets/Project/Script/C%23%20Interface/Normal | The Normal method: If user want to send them SMS instead of E-mail or both. Need to change the method inside the class, it need to be recompiled and redeployed it.
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/Interface.png)| 1. Press the button to view the result. 
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/tree/main/Assets/Project/Script/C%23%20Interface/Interface| To reduce the impact of change in our software. Using Open for extension and Closed for modification prinicple(OCP). Interface is just a role in a contract(Open for extension).

---

## AMVC
### Description:
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/diagram_AMVC.png)

The Model-View-Controller pattern (MVC) splits the software into three major components: Models (Data CRUD), Views (Interface/Detection) and Controllers (Decision/Action). MVC is flexible enough to be implemented even on top of ECS or OOP.

![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/AMVC_List.png)

Link: | Description:
------------ | -------------
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Application/BounceApplication.cs| Single entry point to your application and container of all critical instances and application-related data.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Controller/BounceController.cs| Controllers (Decision/Action) to controls the application workflow.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/Model/BounceModel.cs| Models (Data CRUD) contains all data related to the application.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/View/BounceView.cs| Views (Interface/Detection) contains all views related to the application.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/MVC/View/BallView.cs| Views to describes the Ball view and its features.

## Mirror
### Description: 
![GitHub Logo](https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Document/Images/PhotonVsUnet.png)

Mirror is successor to UNET API, but uses TCP. It makes testing and small LAN games convenient. 

Link: | Description:
------------ | -------------
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/PlayerController.cs| Player's information share with the server
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/SceneScript.cs| A scene networked object all can access and adjust.
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/SceneReference.cs| NetworkIdentity scene object gets disabled, as they are disabled until a player is in ‘ready’ status (ready status is usually set when player spawns).The workaround is to have our GameObject.Find() get the non-networked scene object, which will have those Network Identity scene object as pre-set variables. "sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;"
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Mirror/Controller/ChatBehavior.cs| Chat script which require to hide the Canvas until the method OnStartAuthority()

### Mirror API Library:  
API: | Description:
------------ | ------------- 
[Client]| | It informs the user that the server is not active. client-only code
[Command] | It tells the server what a client wants to change.
[ClientRpc] | It tells the clients what the server has decided to do.
[SyncVar(hook = nameof(Method))]| It fires for changed values
ClientCallback|It inform the user that the server is not active.   
[SyncVar]| It automatically synchronize variables from Server->Client.
OnStartLocalPlayer | It is called on the machine that is the local player.
OnStartClient| It called by the NetworkManager and it works like any other hook.
OnStartAuthority | It called on clients for behaviours that have authority, based on context and hasAuthority.

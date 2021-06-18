<p align="center">
  <img height="200" src="icon.png" alt="Vincent logo">
  <h1 align="center">C-Sharp-Tip-and-Example</h1>
</p>
<p align="center">Description: For easy to understand by using example and grab code instantly.
</p>

## Table of Contents
- [Delegate Event Handler](#delegate-event-handler)
------
# RuntimeInitializeOnLoadMethodAttribute(Unity)

### Description:
RuntimeInitializeOnLoadMethodAttribute executed after Awake.

### Order of execution for event functions:
![Image of execution for event function](https://docs.unity3d.com/uploads/Main/monobehaviour_flowchart.svg)

Link: | Description:
------------ | -------------
https://github.com/vincentgoh1982/C-Sharp-Tip-and-Example/blob/main/Assets/Project/Script/Initialize/InitializeLoad.cs | Allow a runtime class method to be initialized when a game is loaded at runtime without action from the user. Add any "Service" component to the prefab. 
  Null | Examples: Input, Saving, Sound, Config, Asset Bundles, Advertisements

------------
# Delegate Event Handler(C#)

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


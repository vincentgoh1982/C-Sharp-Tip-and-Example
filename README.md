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

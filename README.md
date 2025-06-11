# ROGStrixScopeRx
https://github.com/user-attachments/assets/55d70b24-942e-4e27-bc37-c79c8b009037
# Introduction

Service that connects the Asus ROG Scrope RX keyboard and controls the rgb lights on the keys as quickly as possible, whilst reflecting it in a web UI.

## Goals Primary

The major goals of this project include:

-  ✅ **Cross-Platform:** Our aim is to ensure support for multiple platforms, ensuring accessibility for every user, regardless of their OS. This can be anything, windows, linux, x86, arm, Lxc, vm, docker.. "Anything" cabable of running dotnet

-  ✅ **Run as service** Install as a system service in windows. For linux a service can be created on SystemD. Its better to have the Host manage the service. 

-  ✅ **SignalR** The data is pushed from backend to frontend via Signalr, but this could also have been **Mqtt**
broker. 

-  ✅ **Completely uncoupled** Models are all POCO (DTO) with primitive types ONLY. Most Dtos will be JSON strings. 

## Goals Secondary

Optional goals:

-  🌐 **usbhdi / libhdi** The OS dll is used to hookup the usb driver to the keyboard. Make this more bulletproof (rpi support?)

-  🌐 **UI Controls** Controls that can set modes / feedback from the backend etc.. 

# Project Structure
## Libraries
- :book:
- :computer: 





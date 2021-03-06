# skimur

<img src="resources/logo-small.jpg" align="right" style="width:40%;">

This is the primary codebase that powers [skimur.com](http://www.skimur.com).

Come chat with us! 
[![Join the chat at https://gitter.im/skimur/skimur](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/skimur/skimur?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Description

Skimur is a next-gen media aggregation platform.

## Quickstart

Setting up a dev environment has been streamlined so that anybody can quickly begin contributing. You do need to install two items though.

- Vagrant - https://www.vagrantup.com/
- VirtualBox - https://www.virtualbox.org/

Once installed, open a command prompt and cd into the directory where you will develop. Then, run these commands.

```> git clone https://github.com/skimur/skimur```

This will initialize the source code locally.

```> vagrant up```

This will create and configure a virtual machine dynamically with all the dependencies installed and configuired that are required to run skimur.

**You are all set!** Now open the project up (```src/Skimur.sln```) and run it! Currently, VS2013+ is required. Skimur is a distributed system. So, in order for the web application to fully function, every worker process (```*.Worker.Cons```) needs to be running.

*NOTE:* All though we currently require you to develop with Visual Studio on Windows, future versions will support the new [.NET Core](https://github.com/dotnet/core) which runs on Windows/Mac/Linux. At that time, development will take place using either Visual Studio (Windows), or [Visual Studio Code](https://www.visualstudio.com/en-us/products/code-vs.aspx) (Windows/Mac/Linux). This will also open up support for possible future [Docker](https://www.docker.com/) support!

## Dependencies

Skimur uses .NET 4.5. Future versions will run on .NET Core (Windows/Mac/Linux).

- Postgresql
- Redis
- RabbitMQ
- TODO: Document source dependencies.

## Contributors

- Paul Knopf ([@theonlylawislov](http://twitter.com/theonlylawislov))
- Richard Cruz

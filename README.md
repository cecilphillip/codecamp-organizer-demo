South Florida Code Camp 2016 Demo
=================================


The demo in this repo were created for my talk at [South Florida Code Camp](http://fladotnet.com/codecamp/) 2016 "Get going with ASP .NET Core". Feel free to fork, submit an issue if you have questions, or even send a pull request :D.

Getting Setup
-------------

######ASP .NET
The [Get ASP .NET Home](https://get.asp.net/) repo on Github should have what you need to get ASP.NET Core  installed on your machine. The samples were written with RC1.


######Javascript
Some of the samples use Ecmascript 6 syntax so the project is setup to be transpiled using Babel. To get these working, you'll need to have NodeJS and NPM installed on your path. Navigate to the Demos folder from the command line and enter the following:

* npm install -g jspm
* jspm install

######RethinkDB
The data for this project is stored in a RethinkDB database. I'd recommended spinning up a Linux VM and installing Rethinkdb from the distribution's respective package manager if you're on windows. If you're on OSX, you can always grab it from Homebrew by doing a ```brew install rethinkdb``` 

Another option you have is implementing your own IDataStore and registering it in the DI system. That actually might be a good exercise for you :)

Now you should be all set. Happy Coding



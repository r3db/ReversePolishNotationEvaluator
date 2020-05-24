# Reverse Polish Notation Technical Assessment.

This is my attempt at a Reverse Polish Notation Calculator/Evaluator.<br>

## What's missing?

- A few stack `commands` due to lack of time.
- Network byte order conversions.
- `DisplayMode`, (I actually forgot about that one).
- Did not implement variable nor macro support, (although I would love to, I didn't find enough information on these two, syntax wise)

## Requirements

- .Net Core 3.1 SDK, (https://dotnet.microsoft.com/download/dotnet-core/3.1).

## How to Build?

If you're using Visual Studio, just...
- Open the solution and press `F5`.
- `[Ctrl-R], A` to run all the tests.

![image](https://user-images.githubusercontent.com/9978724/82752765-4e371f00-9db8-11ea-8a25-2b4d8180f17e.png)

If you prefer to do it like real men do you can use the command line ;-)<br>
Here's how:

- Go to the `Calculator.sln` directory and `dotnet build`.
- `dotnet test` to run all tests.

You should see this:<br>
![image](https://user-images.githubusercontent.com/9978724/82753011-3a8cb800-9dba-11ea-8036-92f9dbaffa91.png)

## How to Run?

Go to `Calculator/Calculator.Cli/bin/Debug/netcoreapp3.1/publish` and just `.\rpn.exe` the crap out of it. ;)<br>
In some platforms you "may" have to `dotnet rpn.dll`, MacOS comes to mind.

## How to Use?

We can use it interactively or in "one shot" computations.<br>
Just like your reference implementation.

## Notes

- Most of the commands are implemented,
- Made everything as testable and modular as possible,
- Very little is tested, (lack of time), so... bugs are inevitable,
- I'm using C#, therefore... I went the easy route of keeping the calculator type-safe instead of using dynamic-binding and automatic type-casting, (as your python implementation probably does), this means you cannot add an `int` with a `bool`,
- I'm not checking for errors or invalid instructions, in fact I always assume whatever you write is valid, I only do a bit of type-checking

And that's it.<br>
I saw this centipede thing and I thought it was something about writing an html game.<br>
So I went it the "coolest" option the calculator...<br>

But now I see it's about a freaking emulator :D, I should have taken a look at the readme first, (I only had time for a project).

In any case, I've done similar..ish work on that front, in case you're curious...<br>
- https://github.com/r3db/AgeOfEmpires
- https://github.com/r3db/Disassembler

**Age of Empires**<br>
It's a file parser if you will, it will load/extract images/sprites/animations and metadata from Age of Empires resources.<br>
It deals with stuff like palletes, (which I'm pretty sure the emulator also deals with),<br>
In a nutshell it extracts game resources.<br>

**Disassembler**<br>
It reads the `PECOFF` file format and most of it's associated information:
- Dll Imports,
- Dll Exports,
- Sections,
- Disassembles .Net Core Metadata and Code,
- Disassembles x86/x64 code, (this one is at the early stages)

What I have not done is emulate/run these instructions.<br>
Hope this sheds some light on the type of developer I am.<br>

Cheers :)
# WindowsWindowManager
Library For Managing Windows in the Windows envirorment

This is just a personal project for more intuitive methods than pure pinvoke

# Current Status

- Solution has another project listed not in the repo I use for testing.
- Very much a Work in progress

## issues
- Nuget Package comments not present.

## Completed Features
- Minimizing/maximizing windows based off process or handle
- Move windows based on x y coordinates
- Hiding and Showing windows



# Example Usage

(Will eventually have real examples in another repo)


```
Process paintProcess = Process.Start(PaintPath);

WWM.waitForWindow(paintProcess);

WWM.showWindowMaximized(paintProcess);

WWM.showWindowMinimized(paintProcess);

WWM.showWindowMinimized(paintProcess);

WWM.showWindowNormal(paintProcess);

WWM.moveWindowTo(paintProcess,200,200);

WWM.printRect(WWM.getWindowRect(paintProcess));
```

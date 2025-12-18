# Hot Reload 

## Context:

All test cases are executed on the Reload ASP.NET Core Web API project while the application is running in JetBrains Rider with .NET 10.

## Launch Instructions

### Prerequisites
Before running the project, ensure the following are installed:

.NET SDK 10.x

JetBrains Rider

### Supported OS
Windows 

macOS

### Verify the .NET SDK installation:
```
dotnet --version
```

### Clone the Repository
```
git clone https://github.com/kimbyb/reload.git
cd Reload
``` 

### To Launch from Reload Folder
Press "Play" button from the Rider UI 

Navigate to ```/api/weather``` to see the UI render
## Test Cases
### P0 — Core Hot Reload Functionality

| ID    | Scenario                    | File                   | Change                                                             | Expected Result                                                            |
|-------| --------------------------- | ---------------------- |--------------------------------------------------------------------|----------------------------------------------------------------------------|
| HR-01 | Method body text change     | `WeatherController.cs` | Change return string                                               | Change applied immediately without restart; browser refresh shows new text |
| HR-02 | Logic change in controller  | `WeatherController.cs` | Uncomment Condiitonal logic                                        | New logic executed immediately; no state loss                              |
| HR-03 | Service method logic change | `TimeService.cs`       | Modify `GetMessage()` implementation                               | Updated service logic reflected immediately                                |
| HR-04 | Route change                | `WeatherController.cs` | Change `[Route("api/weather")]` value to `[Route("api/weather1")]` | Old link is unavailable, new link renders                                  |
| HR-05 | Multiple rapid edits        | `WeatherController.cs` | Several quick saves                                                | Hot Reload triggers consistently; no duplicate or missed reloads           |

### P0 — Unsupported Changes & User Feedback (UX-Critical)

| ID    | Scenario                   | File                   | Change                                      | Expected Result                                                                                                                                  |
|-------|----------------------------|------------------------|---------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| HR-06 | DI lifetime change         | `Program.cs`           | `AddSingleton` → `AddScoped`                | Change not applied; Message   _Program.cs(10, 42): [CS1002] ; expected_ is displayed                                                             |
| HR-07 | Async conversion           | `WeatherController.cs` | Convert `Get()` to `async Task<string>`     | Restart required; Messsage _Making a method asynchronous requires restarting the application because is not supported by the runtime_. displayed |
| HR-08 | Startup logic modification | `Program.cs`           | Uncomment HR-O8 line                        | No runtime effect; Rider shows that restart is required                                                                                          |

### P0 — Multi-File & Dependency Changes

| ID    | Scenario                      | File(s)                                  | Change                                                | Expected Result                                                                                                                |
|-------|-------------------------------| ---------------------------------------- |-------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
| HR-09 | Controller + service change   | `WeatherController.cs`, `TimeService.cs` | Modify both files before save                         | Both changes applied consistently _                                                                                            |
| HR-10 | Interface contract change     | `ITimeService.cs`                        | Modify method signature                               | Reload fails or restart required; clear feedback                                                                               |
| HR-11 | Method rename                 | `TimeService.cs`                         | Change `GetMessage()`  tp `GetMessage(string prefix)` | Error. Message   _ITimeService.cs(5, 23): [ENC0009] Updating the type of method requires restarting the application._ displayed |
| HR-12 | Cross-file consistency        | Multiple                                 | Change dependent code                                 | No partial or stale behavior                                                                                                   |


###  P1 — Stability & Recovery

| ID    | Scenario                       | File                   | Change                      | Expected Result                         |
|-------| ------------------------------ | ---------------------- | --------------------------- | --------------------------------------- |
| HR-13 | Syntax error during Hot Reload | `WeatherController.cs` | Introduce compilation error | Clear error shown; app not restarted    |
| HR-14 | Recovery after failed reload   | `WeatherController.cs` | Fix syntax error            | Hot Reload resumes normally             |
| HR-15 | Repeated failed reloads        | Any                    | Multiple invalid edits      | Rider remains responsive; no IDE freeze |

###  P1 — Debugger & Edit-and-Continue

| ID    | Scenario                              | File                                      | Change                                      | Expected Result                            |
|-------|---------------------------------------| ----------------------------------------- |---------------------------------------------| ------------------------------------------ |
| HR-16 | Breakpoint before reload              | `WeatherController.cs`                    | Modify method body while paused             | Execution continues with updated code      |
| HR-17 | Step into reloaded code               | `WeatherController.cs` → `TimeService.cs` | Reload service method                       | Debugger steps into updated implementation |
| HR-18 | Variable inspection after reload      | `WeatherController.cs`                    | Change logic affecting variables            | Variables reflect updated logic correctly  |
| HR-19 | Variable inspection and change        | `WeatherController.cs`                    | Evaluate exptession and change the variable | Variables reflect updated logic correctly  |

###  P1 — State Preservation

| ID    | Scenario                         | File             | Change                                 | Expected Result                        |
|-------| -------------------------------- | ---------------- | -------------------------------------- | -------------------------------------- |
| HR-20 | Static state preservation        | `TimeService.cs` | Add static counter; reload method body | Counter value preserved across reloads |
| HR-21 | Multiple reloads without restart | Any              | Repeated supported changes             | Application state remains intact       |

### P1 — Routing & HTTP Semantics

| ID    | Scenario             | File                   | Change                                      | Expected Result                                                                          |
|-------| -------------------- | ---------------------- |---------------------------------------------|------------------------------------------------------------------------------------------|
| HR-22 | Add new endpoint     | `WeatherController.cs` | Add new `[HttpGet]` action                  | Endpoint available                                                                       |
| HR-23 | Change HTTP verb     | `WeatherController.cs` | `[HttpGet]` → `[HttpPost]`                  | Old route persists; restart requested                                                    |
| HR-24 | Change response type | `WeatherController.cs` | Comment old `[HttpGet]` and upcomment HR-24 | Reload applies; response serialized correctly _PS: did not actually happen. Needed reload_ |

### P1 — Generics 

| ID    | Scenario                        | File                    | Change Description                             | Expected Result                          |
|-------| ------------------------------- |-------------------------| ---------------------------------------------- |------------------------------------------|
| HR-25 | Generic method body change      | `WeatherController.cs`  | Modify logic inside a generic method           | Change applied via Hot Reload            |
| HR-26 | Generic type parameter rename   | `GenericCalculator.cs`  | Rename generic type parameter (`T` → `TValue`) | Restart needed. Message displayed        |
| HR-27 | Add generic constraint          | `GenericCalculator.cs`  | Add constraint (e.g. `where T : class`)        | Restart required. Message displayed      |
| HR-28 | Remove generic constraint       | `GenericCalculator.cs`  | Remove existing constraint                     | Restart required, no crash               |
| HR-29 | Change generic method signature | `WeatherController.cs`  | Modify generic parameters or return type       | Restart required. Message displayed      |
| HR-30  | Generic usage change            | Consumer code           | Change how generic method/type is instantiated | Change applied, see change (if visible)  |

### P1 — Lambdas 

| ID    | Scenario                             | File                   | Change Description               | Expected Result                                        |
|-------| ------------------------------------ |------------------------| -------------------------------- |--------------------------------------------------------|
| HR-31 | Lambda body logic change             | `WeatherController.cs` | Modify lambda expression logic   | Change applied via Hot Reload, see change (if visible) |
| HR-32 | Replace lambda with method reference | `WeatherController.cs` | Convert lambda to named method   | Restart needed. Message displayed                      |
| HR-33 | Lambda capture change                | `LambdaProcessor.cs`   | Modify captured variables        | Change applied                                         |
| HR-34 | Add lambda parameter                 | `LambdaProcessor.cs`   | Modify lambda parameter list     | Restart needed. Message displayed   |
| HR-35 | Change lambda return type            | `LambdaProcessor.cs`   | Modify inferred return type      | Restart needed. Message displayed        |

###  P2 — IDE Feedback & Developer Experience

| ID    | Scenario                 | Area              | Observation Focus                 | Expected Result                                           |
|-------| ------------------------ | ----------------- | --------------------------------- |-----------------------------------------------------------|
| HR-36 | Status bar messaging     | Rider UI          | Reload success / failure messages | Clear, timely, accurate                                   |
| HR-37 | Console logging          | Run/Debug Console | Hot Reload logs                   | Meaningfull and consistent                                |
| HR-38 | Run vs Debug consistency | Rider             | Same change in both modes         | Behavior consistent across modes (same logs messages etc) |

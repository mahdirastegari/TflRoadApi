
## TfL Road Status Console Application

This project is a simple console application that interacts with the Transport for London (TfL) open data REST API to retrieve the status of major roads.

### Overview

TfL maintains an open data REST API at [TfL API](https://api.tfl.gov.uk). You will need to register for a developer key at [TfL Developer Portal](https://api-portal.tfl.gov.uk/) to use the API. After obtaining your keys, please update the app settings in `./TfLRoadAPI/appsettings.json`.
```json
{
  "AppConfig": {
    "BaseApiAddress": "https://api.tfl.gov.uk",
    "AppId": "Your App Id",
    "DeveloperKey": "Your Developer Key"
  }
}
```
If you wish to test in a different API environment, you can modify the `BaseAPIAddress` in the setting.

### Project Structure:
We followed the Clean Architecture pattern for this project. The API is located in the bottom layers. To avoid coupling the app directly to the API, we introduced a service called `RoadService`, which converts the API output to the project's DTO (Data Transfer Object).

When the application processes the arguments, it follows these steps:

1.  **Argument Validation:** Check whether the argument is valid.
    
2.  **Call MainService:** Initiate the main service process.
    
3.  **Call RoadService:** Use the `RoadService` to handle the data conversion.
    
4.  **Call API:** Fetch data from the API.

### Using Polly for HTTP Exceptions
We are using the `Polly` package to manage HTTP exceptions, such as timeouts and circuit breaks. This helps handle network issues during API calls. Polly will retry the request three times with an exponential wait time between attempts.

### Using HttpMessageHandler for AppId,Key
We are using `DelegateHandler` to manage the TfL App ID and Developer key, which we have named `TflApiIdKeyQueryParameterHandler`. This is a common approach for handling authorization, adding default HTTP headers, and similar tasks.

### Exit codes:
The application will exit with the following codes:
-   **0: NoIssue** - The application was completed with no issues.
-   **1: InvalidArguments** - The argument passed to the application is invalid.
-   **2: InvalidRoadId** - The provided Road ID is invalid when checked with the API.
-   **3: ApiError** - There was a problem fetching the API results.
-   **4: UnhandledException** - An unhandled exception occurred during execution.

### How to build
Please call the following command in the main repository folder:
```
dotnet build
```

### How to debug

You can also debug the app by changing the launch profiles available in the repository. You can find two sample profiles in the project in `TflRoad/Properties/lunchSettings.json`:

```json
{
  "profiles": {
    "ValidRoad": {
      "commandName": "Project",
      "commandLineArgs": "A2"
    },
    "InvalidRoad": {
      "commandName": "Project",
      "commandLineArgs": "A233"
    }
  }
}
```

### How to run the Tests:
There are two types of tests available for this project 1-UnitTests and 2-Behavioural Tests, which you can run by calling the following command:
```
dotnet test
```
#### UnitTest
The project Unit-Tests can be found in the `/TflRoad.UnitTests` or in the Visual Studio solution address `Tests/TflRoad.UnitTests`. To run the Unit-Tests, you can call the command:
```
dotnet test TflRoad.UnitTests
```

#### Behavioural Tests
We have the Behavioural tests for this application which can be found in `/TflRoad.AcceptanceTests`  or in the Visual Studio solution address `Tests/TflRoad.AcceptanceTests`. To run the Behavioural-Tests, you can call the command:
```
dotnet test TflRoad.AcceptanceTests
```
The behavioural test will run the main app and check four different scenarios:

1.  **Valid Road ID**: Ensuring the application correctly processes and displays the status for a valid road ID.
    
2.  **Invalid Road ID**: Verifying the application returns an informative error message for an invalid road ID.
    
3.  **No Arguments Passed**: Checking the application's response when no arguments are provided.
    
4.  **Too Many Arguments Passed**: Testing how the application handles cases where too many arguments are supplied.

We are using `SpecFlow`  to develop our tests by starting with a new driver to monitor our `RoadStatus.exe`  console application.

### Examples

```sh
PS C:\> .\RoadStatus.exe A2
The status of the A2 is as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays
PS C:\> echo $lastexitcode
0
```

```sh
PS C:\> .\RoadStatus.exe A233
A233 is not a valid road
PS C:\> echo $lastexitcode
2
```

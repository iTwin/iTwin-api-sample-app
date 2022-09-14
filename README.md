# iTwin API Sample App

Copyright © Bentley Systems, Incorporated. All rights reserved.

An iTwin sample application that demonstrates how to create, query and update an iTwin using the iTwin API.

This application contains sample code that should not be used in a production environment. It contains no retry logic and minimal logging/error handling.


## Prerequisites

* [Git](https://git-scm.com/)
* Visual Studio 2019/2022 or [Visual Studio Code](https://code.visualstudio.com/)
* [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0/)


## Development Setup (Visual Studio 2019 or Visual Studio 2022)

1. Clone Repository

2. Open iTwinSampleApp.sln and Build

3. (Optional) Put breakpoint in Program.cs

4. Run to debug

5. It will require a user token. 

   * Go to the iTwin API [developer portal](https://dev-developer.bentley.com/apis/itwins/operations/create-itwin/)
   * Click the TryIt Button
   * In the popup window, select authorizationCode in the Bentley OAuth2 Service dropdown
   * This will popup another window that will require you to login.
   * After you login, the Authorization header will be populated. Copy the entire string and paste into the command window for the iTwin Sample App.
   * Press Enter

6. You can now step through the code that will create and manage an iTwin.

## Contributing to this Repository

For information on how to contribute to this project, please read [CONTRIBUTING.md](CONTRIBUTING.md) for contribution guidelines, [GETTINGSTARTED.md](GETTINGSTARTED.md) for information on working with the documentation in this repository and [PULL_REQUESTS.md](PULL_REQUESTS.md) for information on how to create a pull request.

In the future, [HELPWANTED.md](HELPWANTED.md) may contain a list of contributions we would like to see to this project.

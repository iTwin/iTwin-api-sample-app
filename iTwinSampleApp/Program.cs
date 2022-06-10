// See https://aka.ms/new-console-template for more information
using iTwinSampleApp;

Console.ForegroundColor = ConsoleColor.White;
Console.Clear();
Console.WriteLine("*****************************************************************************************");
Console.WriteLine("*           iTwin Platform Sample App                                                   *");
Console.WriteLine("*****************************************************************************************\n");

// Retrieve the token using the TryIt button. https://developer.bentley.com/api-groups/administration/apis/projects/operations/create-project/
Console.WriteLine("\n\nCopy and paste the Authorization header from the 'Try It' sample in the APIM front-end:  ");
string authorizationHeader = Console.ReadLine();
Console.Clear();

Console.ForegroundColor = ConsoleColor.White;
Console.Clear();
Console.WriteLine("*****************************************************************************************");
Console.WriteLine("*           iTwin Platform Sample App                                                   *");
Console.WriteLine("*****************************************************************************************\n");

await using var iTwinMgr = new iTwinManager(token: authorizationHeader);

// Execute iTwin workflow.  This will create/update/query an iTwin.
await iTwinMgr.iTwinManagementWorkflow();

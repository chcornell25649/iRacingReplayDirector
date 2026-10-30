// See https://aka.ms/new-console-template for more information
using SVappsLAB.iRacingTelemetrySDK;
using System.Reflection;

Console.WriteLine("=== Testing SDK v2.6.0 API ===");
Console.WriteLine();

var sdkAssembly = typeof(TelemetryClient<>).Assembly;
Console.WriteLine($"SDK Assembly: {sdkAssembly.FullName}");
Console.WriteLine();

// Check if TelemetrySessionInfo exists
Console.WriteLine("=== Checking for TelemetrySessionInfo ===");
var telemetrySessionInfoType = sdkAssembly.GetType("SVappsLAB.iRacingTelemetrySDK.TelemetrySessionInfo");
if (telemetrySessionInfoType != null)
{
    Console.WriteLine($"TelemetrySessionInfo EXISTS");
    Console.WriteLine($"Properties of TelemetrySessionInfo:");
    foreach (var prop in telemetrySessionInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
    {
        Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
    }
}
else
{
    Console.WriteLine($"TelemetrySessionInfo DOES NOT EXIST");
}

Console.WriteLine();

// Check SessionInfo type
Console.WriteLine("=== Checking for SessionInfo ===");
var sessionInfoType = sdkAssembly.GetType("SVappsLAB.iRacingTelemetrySDK.SessionInfo");
if (sessionInfoType != null)
{
    Console.WriteLine($"SessionInfo EXISTS");
    Console.WriteLine($"Properties of SessionInfo:");
    foreach (var prop in sessionInfoType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
    {
        Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
    }
    
    // Check if there's a Session property
    var sessionProp = sessionInfoType.GetProperty("Sessions");
    if (sessionProp != null)
    {
        Console.WriteLine();
        Console.WriteLine($"Sessions property type: {sessionProp.PropertyType}");
        var genericArgs = sessionProp.PropertyType.GetGenericArguments();
        if (genericArgs.Length > 0)
        {
            Console.WriteLine($"Sessions element type: {genericArgs[0].FullName}");
            Console.WriteLine($"Properties of Session element type:");
            foreach (var prop in genericArgs[0].GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
            }
        }
    }
}
else
{
    Console.WriteLine($"SessionInfo DOES NOT EXIST");
}

Console.WriteLine();

// Check what SessionData yields
Console.WriteLine("=== Checking SessionData Stream ===");
var clientType = typeof(ITelemetryClient<>).MakeGenericType(typeof(DynamicTelemetryData));
var sessionDataProp = clientType.GetProperty("SessionData");
if (sessionDataProp != null)
{
    var returnType = sessionDataProp.PropertyType;
    Console.WriteLine($"SessionData return type: {returnType}");
    
    if (returnType.IsGenericType)
    {
        var genericArgs = returnType.GetGenericArguments();
        if (genericArgs.Length > 0)
        {
            var yieldType = genericArgs[0];
            Console.WriteLine($"SessionData yields: {yieldType.FullName}");
            
            // Check properties of the yield type
            Console.WriteLine($"Properties of yield type:");
            foreach (var prop in yieldType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
            }
        }
    }
}

Console.WriteLine();
Console.WriteLine("=== Checking ITelemetryClient<TelemetryData> Methods ===");
// Use DynamicTelemetryData since TelemetryData is generated
var dynamicType = typeof(DynamicTelemetryData);
var iTelemetryClientType = typeof(ITelemetryClient<>).MakeGenericType(dynamicType);
Console.WriteLine($"Methods on ITelemetryClient<DynamicTelemetryData>:");
foreach (var method in iTelemetryClientType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
{
    if (method.Name.Contains("Monitor") || method.Name.Contains("Connect") || method.Name.Contains("Pause") || method.Name.Contains("Start"))
    {
        var parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
        Console.WriteLine($"  {method.Name}({parameters}) => {method.ReturnType.Name}");
    }
}

Console.WriteLine();
Console.WriteLine("=== Checking TelemetryHandlers<DynamicTelemetryData> Properties ===");
var handlersType = typeof(TelemetryHandlers<>).MakeGenericType(dynamicType);
Console.WriteLine($"Properties on TelemetryHandlers<DynamicTelemetryData>:");
foreach (var prop in handlersType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
{
    Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
}

Console.WriteLine();
Console.WriteLine("Hello, World!");

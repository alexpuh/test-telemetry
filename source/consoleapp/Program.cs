using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

// Create ActivitySource for tracing
var activitySource = new ActivitySource("ConsoleApp");

// Configure OpenTelemetry
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("consoleapp"))
    .AddSource("ConsoleApp")
    .AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:4317");
    })
    .Build();

// Start a span for the main processing
using var activity = activitySource.StartActivity("ProcessingOperation", ActivityKind.Internal);

activity?.SetTag("operation.type", "main");
activity?.AddEvent(new ActivityEvent("Start processing"));

// Simulate some work
await Task.Delay(100);

activity?.AddEvent(new ActivityEvent("End processing"));
activity?.SetStatus(ActivityStatusCode.Ok);
activity?.SetTag("telemetry.exported", "true");
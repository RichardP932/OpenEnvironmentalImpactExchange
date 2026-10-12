using System.Text.Json;
using Json.Schema;

var schemaPath = Path.Combine(
    AppContext.BaseDirectory,
    "Schemas",
    "record.schema.json"
);

Console.WriteLine("Open Environmental Impact Exchange Validator");
Console.WriteLine();

if (!File.Exists(schemaPath))
{
    Console.Error.WriteLine($"Schema not found: {schemaPath}");
    return;
}

var schemaJson = await File.ReadAllTextAsync(schemaPath);

using var schemaDocument = JsonDocument.Parse(schemaJson);
var schema = JsonSchema.FromText(schemaJson);

var examplesDirectory = Path.Combine(
    AppContext.BaseDirectory,
    "Examples"
);

var exampleFiles = Directory.GetFiles(
    examplesDirectory,
    "*.json"
).OrderBy(Path.GetFileName);

foreach (var examplePath in exampleFiles)
{
    Console.WriteLine();
    Console.WriteLine($"Validating: {Path.GetFileName(examplePath)}");

    var exampleJson = await File.ReadAllTextAsync(examplePath);

    using var exampleDocument = JsonDocument.Parse(exampleJson);

    var results = schema.Evaluate(
        exampleDocument.RootElement,
        new EvaluationOptions
        {
            OutputFormat = OutputFormat.List,
            RequireFormatValidation = true
        }
    );

    Console.WriteLine(
        results.IsValid
            ? "Validation PASSED"
            : "Validation FAILED"
    );
}

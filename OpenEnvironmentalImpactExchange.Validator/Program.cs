
using System.Text.Json;
using Json.Schema;

Console.WriteLine("Open Environmental Impact Exchange Validator");
Console.WriteLine();

var schemaPath = Path.Combine(
    AppContext.BaseDirectory,
    "Schemas",
    "record.schema.json"
);

if (!File.Exists(schemaPath))
{
    Console.Error.WriteLine($"Schema not found: {schemaPath}");
    Environment.ExitCode = 2;
    return;
}

var schemaJson = await File.ReadAllTextAsync(schemaPath);

using var schemaDocument = JsonDocument.Parse(schemaJson);
var schema = JsonSchema.FromText(schemaJson);

var examplesDirectory = Path.Combine(
    AppContext.BaseDirectory,
    "Examples"
);

IEnumerable<string> exampleFiles;

if (args.Length > 0)
{
    exampleFiles = new[] { args[0] };
}
else
{
    exampleFiles = Directory.GetFiles(
        examplesDirectory,
        "*.json"
    ).OrderBy(Path.GetFileName);
}

var hasValidationFailures = false;

foreach (var examplePath in exampleFiles)
{
    if (!File.Exists(examplePath))
    {
        Console.Error.WriteLine($"File not found: {examplePath}");
        Environment.ExitCode = 2;
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Validating: {Path.GetFileName(examplePath)}");

    var exampleJson = await File.ReadAllTextAsync(examplePath);

    JsonDocument exampleDocument;

    try
    {
        exampleDocument = JsonDocument.Parse(exampleJson);
    }
    catch (JsonException ex)
    {
        Console.Error.WriteLine(
            $"Invalid JSON in {Path.GetFileName(examplePath)}: {ex.Message}"
        );

        Environment.ExitCode = 2;
        return;
    }

    using (exampleDocument)
    {
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

        if (!results.IsValid)
        {
            hasValidationFailures = true;

            Console.WriteLine("Validation errors:");

            foreach (var detail in results.Details ?? [])
            {
                if (detail.IsValid || detail.Errors is null)
                {
                    continue;
                }

                // Ignore errors from the unsuccessful alternative when
                // climateAssessment: null has satisfied the oneOf schema.
                if (detail.EvaluationPath.ToString().StartsWith(
                        "/properties/climateAssessment/oneOf/1",
                        StringComparison.Ordinal) == true &&
                    (results.Details ?? []).Any(parent =>
                        parent.InstanceLocation.ToString() == "/climateAssessment" &&
                        parent.EvaluationPath.ToString() == "/properties/climateAssessment" &&
                        parent.IsValid))
                {
                    continue;
                }

                foreach (var error in detail.Errors)
                {
                    Console.WriteLine(
                        $"  {detail.InstanceLocation}: {error.Value}"
                    );
                }
            }
        }
    }
}

Environment.ExitCode = hasValidationFailures ? 1 : 0;

const fs = require("fs");
const path = require("path");
const Ajv2020 = require("ajv/dist/2020").default;
const addFormats = require("ajv-formats");

const root = path.resolve(__dirname, "../../OpenEnvironmentalImpactExchange.Validator");

const schema = JSON.parse(
    fs.readFileSync(`${root}/schemas/record.schema.json`, "utf8")
);

const ajv = new Ajv2020({ allErrors: true });
addFormats(ajv);

const validate = ajv.compile(schema);

const examples = [
    ["invalid-date.json", false],
    ["invalid-incomplete-coordinates.json", false],
    ["invalid-unknown-type.json", false],
    ["valid-heatwave.json", true],
    ["valid-minimal-with-evidence.json", true]
];

let allCorrect = true;

for (const [filename, expected] of examples) {
    const record = JSON.parse(
        fs.readFileSync(path.join(root, "examples", filename), "utf8")
    );

    const actual = validate(record);
    const correct = actual === expected;

    console.log(
        `${filename}: ${actual ? "VALID" : "INVALID"} — ${correct ? "EXPECTED" : "UNEXPECTED"}`
    );

    if (!correct) {
        allCorrect = false;
        console.log(validate.errors);
    }
}

console.log(
    "\nInteroperability tests:",
    allCorrect ? "ALL PASSED" : "FAILED"
);

process.exitCode = allCorrect ? 0 : 1;
using System.Diagnostics;

var rootCommand = new RootCommand(
    "Generates cryptographic hashes of a specified file or " +
    "all files in a specified directory (glob '*.txt' pattern supported).")
{
    CreateHashCommand("md4", [], "MD4", new acryptohashnet.MD4()),
    CreateHashCommand("md5", [], "MD5", new acryptohashnet.MD5()),
    
    CreateHashCommand("sha1", ["sha"], "SHA1", new acryptohashnet.SHA1()),

    CreateHashCommand("sha2_256", ["sha2", "sha256"], "SHA2-256", new acryptohashnet.Sha2_256()),
    CreateHashCommand("sha2_512", ["sha512"], "SHA2-512", new acryptohashnet.Sha2_512()),

    CreateHashCommand("sha3_256", ["sha3"], "SHA3-256", new acryptohashnet.Sha3_256()),
    CreateHashCommand("sha3_512", [], "SHA3-512", new acryptohashnet.Sha3_512()),

    CreateHashCommand("keccak256", ["keccak"], "KECCAK-256", new acryptohashnet.Keccak256()),
    CreateHashCommand("keccak512", [], "KECCAK-512", new acryptohashnet.Keccak512())
};

rootCommand.Arguments.Add(new Argument<string>("path") { DefaultValueFactory = _ => Directory.GetCurrentDirectory() });
rootCommand.SetAction(parseResult =>
{
    GenerateHash(new acryptohashnet.Sha2_256(), parseResult.GetValue<string>("path") ?? Directory.GetCurrentDirectory());
});

rootCommand.Parse(args).Invoke();

Command CreateHashCommand(string commandName, string[] aliases, string hashAlgName, HashAlgorithm hashAlg)
{
    var pathArgument = new Argument<string>("path")
    {
        Description = "The path to the file or directory (glob pattern supported).",
        DefaultValueFactory = _ => Directory.GetCurrentDirectory()
    };

    var command = new Command(commandName, $"{hashAlgName} hash.");
    foreach (var alias in aliases)
    {
        command.Aliases.Add(alias);
    }
    command.Arguments.Add(pathArgument);

    command.SetAction(parseResult =>
    {
        GenerateHash(hashAlg, parseResult.GetValue<string>("path") ?? Directory.GetCurrentDirectory());
    });

    return command;
}

void GenerateHash(HashAlgorithm hashAlg, string pathArgument)
{
    Console.WriteLine($"# {hashAlg.GetType().Name} {pathArgument}");

    var fileName = Path.GetFileName(pathArgument);

    if (fileName.Contains("*") || fileName.Contains("?"))
    {
        Matcher matcher = new();
        matcher.AddInclude(fileName);

        var directoryPath = Path.GetDirectoryName(pathArgument);

        if (string.IsNullOrEmpty(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();
        }
        
        var matchingResult = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(directoryPath)));

        foreach (var filePath in matchingResult.Files.Select(x => Path.Combine(directoryPath, x.Path)).AsParallel())
        {
            GenerateHashForFile(hashAlg, filePath);
        }

        return;
    }

    if (Directory.Exists(pathArgument))
    {
        foreach (var filePath in Directory.EnumerateFiles(pathArgument).AsParallel())
        {
            GenerateHashForFile(hashAlg, filePath);
        }
    }
    else
    {
        GenerateHashForFile(hashAlg, pathArgument);
    }
}

void GenerateHashForFile(HashAlgorithm hashAlg, string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.Error.WriteLine($"[ERR] File not found: {filePath}");
        return;
    }    

    var fileName = Path.GetFileName(filePath);
    var fileExtension = Path.GetExtension(filePath);

    try
    {        
        var fileSize = new FileInfo(filePath).Length;
        var hashValue = HashFile(filePath, hashAlg);

        if (fileExtension.Equals(".exe", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".dll", StringComparison.OrdinalIgnoreCase))
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            Console.WriteLine($"{hashValue} # {fileName}, size: {fileSize} bytes, version: {fileVersionInfo.FileVersion ?? "N/A"}");
        }
        else
        {
            Console.WriteLine($"{hashValue} # {fileName}, size: {fileSize} bytes");
        }
    }
    catch(Exception ex)
    {
        Console.Error.WriteLine($"[ERR] {ex.Message}: {fileName}");
    }
}

string HashFile(string filePath, HashAlgorithm hashAlg)
{
    using var stream = File.OpenRead(filePath);
    return Hex(hashAlg.ComputeHash(stream));
}

string Hex(byte[] bytes) => string.Join("", bytes.Select(x => $"{x:x2}"));


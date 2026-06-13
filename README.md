# dotnet-hash
Simple C#/.Net implementation to generate cryptographic hashes for files and directories:

## How to run it

Ensure that .Net 10 or later is installed
```
dotnet --version
```

Clone repository and run `md5` or `sha2` or `sha3` command:
``` shell
git clone https://github.com/AndreyRusyaev/dotnet-hash
cd dotnet-hash
dotnet run sha3 *.cs
```

Output:
```shell
afcd67433f3d2d0b4c00b46ecd9deb09d867dae7165629297fd73e5803c44b0f: Program.cs
```

## List of supported algorithms

* MD4
* MD5
* SHA2 (256, 512 bits)
* SHA3 (256, 512 bits)

# Prerequisites
.Net 10.0 or higher.

[Install .NET on Windows, Linux, and macOS](https://learn.microsoft.com/en-us/dotnet/core/install/)

### Windows
``` shell
winget install Microsoft.DotNet.SDK.10
```

### MacOS
``` shell
brew install dotnet
```

### Ubuntu
``` shell
# Add Microsoft package manager feed
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# installation
sudo apt-get install -y dotnet-sdk-10.0
```

# Usage

```
Description:
  Generates cryptographic hashes of a specified file or all files in a specified directory (glob pattern supported).

Usage:
  dotnet-hash [command] [options]

Options:
  -?, -h, --help  Show help and usage information
  --version       Show version information

Commands:
  md4 <path>             MD4 hash.
  md5 <path>             MD5 hash.
  sha2, sha2_256 <path>  SHA2-256 hash.
  sha2_512 <path>        SHA2-512 hash.
  sha3, sha3_256 <path>  SHA3-256 hash.
  sha3_512 <path>        SHA3-512 hash.
```

# Examples

```shell
dotnet run sha2 .
```

Output:
``` shell
d0dc7f3a032885de8a4cc242dfa979eb79bdfb850ff522a65ee88f21cac1cd86: dotnet-hash.csproj
92fef3e5440f55034af30dcc85ea7f4d8963e41712a080f05c28404a3080ba9c: Program.cs
12592396e17eebb5ec800232b10e88a9f1845269f0123dccbc3cd36cf63a880e: README.md
```

OR

```shell
dotnet run md5 "C:\Windows\System32\notepad.exe"
```

Output:
``` shell
017b54a1f42119891b0a8439989cebc2: notepad.exe
```

OR

```shell
dotnet run sha3_256 "C:\Windows\System32\*.exe" > hashes.sha256
```

Output:
``` shell
hashes.sha3
```

OR

```shell
dotnet run sha2 "C:\Windows\System32" > hashes.sha256
```

Output:
``` shell
hashes.txt
```

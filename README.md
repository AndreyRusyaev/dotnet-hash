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
dotnet run
```

Output:
``` shell
# Sha2_256 C:\Git\dotnet-hash
13c01fc8694cc1cdd8d70e2fbecab7b6a6988335809916d0c13027f15a6377be # .gitignore
5483000df0d76a3e3536ab2841e95028db3879ad228194074d756ebdadb82dcb # dotnet-hash.csproj
928138a3864ce3f4ec0d3649093f3f04e657ac2dbcae4bc1e18244023f6020aa # Program.cs
62324a70ef27efe0345c385740f5c9888ecb71b86437744987d296eda8fc6667 # README.md
```

```shell
dotnet run -- sha2
```

Output:
``` shell
# Sha2_256 C:\Git\dotnet-hash
13c01fc8694cc1cdd8d70e2fbecab7b6a6988335809916d0c13027f15a6377be # .gitignore
5483000df0d76a3e3536ab2841e95028db3879ad228194074d756ebdadb82dcb # dotnet-hash.csproj
928138a3864ce3f4ec0d3649093f3f04e657ac2dbcae4bc1e18244023f6020aa # Program.cs
d9e0c9d6990070bcb86f74a1bb2728185ed739bb4be13f32fe7ebf23da2f08f3 # README.md
```

OR

```shell
dotnet run -- md5 "C:\Windows\System32\notepad.exe"
```

Output:
``` shell
# MD5 C:\Windows\System32\notepad.exe
017b54a1f42119891b0a8439989cebc2 # notepad.exe, size: 360448 bytes, version: 10.0.26100.8457 (WinBuild.160101.0800)
```

OR

```shell
dotnet run -- sha3_256 "C:\Windows\System32\*.exe" > hashes_system32_exe.sha3_256
```

Output:
``` shell
hashes.sha3_256
```

OR

```shell
dotnet run -- sha2 "C:\Windows\System32" > hashes_system32.sha2_256
```

Output:
``` shell
hashes.sha2_256
```

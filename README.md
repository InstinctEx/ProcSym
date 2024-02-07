# ProcSym Application

This application is designed to perform various file manipulation tasks, including renaming, creating symlinks, and more. It provides a user-friendly interface for users to interact with the functionality.

## Features

- **Administrative Privileges:** Ensure the application is run with administrator privileges.
- **File Operations:** Rename, create symlinks, and perform other file-related operations.
- **Process Management:** Kill specified processes during application closure.
- **Parallel Processing:** Utilizes parallel processing for certain tasks to improve performance.

## Getting Started

### Prerequisites

- .NET Framework
- Administrator privileges for certain operations

### Installation

1. Clone the repository to your local machine.

```bash
git clone https://github.com/InstinctEx/Procsym.git
```
2. Open the solution in Visual Studio or your preferred C# development environment.
3. Build the solution and run the application.

# Usage
1. Run the application as an administrator.
2. Add executable files using the "Open Folder" button.
3. Perform desired operations (rename, create symlinks, etc.) using the provided buttons.
4. Close the application to trigger cleanup processes.
![Screenshot](https://i.imgur.com/hVFP6Gn.png)

#  To Do
- Fix a loop somewhere that always runs for all selected files even if they are in the same directory ending up showing weird errors but the program works normally. 
- Fix program crashing sometimes when the files are in different directories. It appears to be random.
# Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

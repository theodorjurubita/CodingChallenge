*** CodingChallenge ***

1. Requirements

	A. On your system open a Command Prompt / PowerShell and run the command: >dotnet --version, you should have at least .NET 6 installed (https://dotnet.microsoft.com/en-us/download).

2. How to run it
	
	A. Go inside the "CodingChallenge" folder, where is the console application project.
	B. Run >dotnet build command.
	C. Run the program's specific commands using the following template: >dotnet run [arguments]
			-> Example >dotnet run --read-inline '{ "Type": "Mint", "TokenId": "0xD000000000000000000000000000000000000000", "Address": "0x1000000000000000000000000000000000000000" }'
		*Please note that the commands should be written on a single line as it may cause incorrect reading of arguments.
	D. Reading from a JSON file (e.g. >dotnet --read-file transactions.json) is possible if the file transactions.json exists inside the csproj folder.
		*Please note that reading from files was tested only on Windows, so it might not function well with other file systems of other operating systems.
		
3. Notes

	A. The program uses a SQLite database to persist data between program calls. The first time you run the program it should create a nftdatabase.db file in your system's local folder.
	B. The file transactions.json is already filled with the sample data from the pdf provided.
	C. The program is intended to handle only the commands specified in the pdf provided. Simpler approaches were chosen for brevity.
	D. Time spent on building this solution is around 4 hours.

# AdventOfCode

Repo for AoC, thusfar only 2020 complete (2019 half complete resides in seperate repo as of yet).

Also contains small CLI for getting things up and running.<br>
Example usage; `dotnet run -p .\CLI.csproj scaffold -y 2015 -d 2 -u -e 512` <br>
This command will create `Day02.cs` in the `AoC2015` project, 
as well as create `Day02test.cs` in `AoC2015Test` project with an expected value of 512 for part 1 (assuming an example was provided for the day). 

- `scaffold`, create .cs file for day with option for unit test file<br>
    -y           Required. The year, from 2015 to 2020<br>
    -d           Required. The day, from 1 to 25<br>
    -u           Creates a unit test file<br>
    -e           Sets the expected value for example part 1<br>

- `getinput`, retrieves input from site <br>
  -y           Required. The year, from 2015 to 2020<br>
  -d           Required. The day, from 1 to 25<br>
  -s           Required. The session token for adventofcode.com<br>

- `runday`, run an individual day<br>
  -y           Required. The year, from 2015 to 2020<br>
  -d           Required. The day, from 1 to 25<br>
  -p           Part one or two, or both if not specified<br>

 CLI made with [CommandLineParser](https://github.com/commandlineparser/commandline)
   

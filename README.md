# AdventOfCode

Repo for AoC, thusfar only 2020 complete (2019 half complete resides in seperate repo as of yet).

Also contains small CLI for getting things up and running.<br>
Example usage; `.\CLI.exe scaffold -y 2015 -d 2 -u -c hello:world` <br>
This command will create `Day02.cs` in the `AoC2015` project, 
as well as create `Day02test.cs` in `AoC2015Test` project with a single test case generated for part 1; `TestCase[("hello", "world")]`. 

Available commands and their flags.
- `scaffold`, create .cs file for day with option for unit test file<br>
    -y           Required. The year, from 2015 to 2020<br>
    -d           Required. The day, from 1 to 25<br>
    -u           Creates a unit test file<br>
    -e           Mutually exclusive with -c. Sets the expected value for example part 1 and reads input from file.<br>
    -c           Mutually exclusive with -e. Generate test cases for part 1 in the format of input:outcome.<br>

- `getinput`, retrieves input from site <br>
  -y           Required. The year, from 2015 to 2020<br>
  -d           Required. The day, from 1 to 25<br>
  -s           Required. The session token for adventofcode.com<br>

- `runday`, run an individual day<br>
  -y           Required. The year, from 2015 to 2020<br>
  -d           Required. The day, from 1 to 25<br>
  -p           Part one or two, or both if not specified<br>

 CLI made with [CommandLineParser](https://github.com/commandlineparser/commandline)
   

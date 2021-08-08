# tunein-mergejson-testapp
This is a simple console application, for testing existing API endpoint.
The result of the program - merged json object.
***
In order to test it with real api:
```sh
dotnet run --project .\MergeJson\MergeJson\MergeJson.csproj --launch-profile MergeJson
```
***
In order to test it with fake api:
```sh
dotnet run --project .\MergeJson\Tunein.API.Fake\ 
dotnet run --project .\MergeJson\MergeJson\MergeJson.csproj --launch-profile MergeJson.Dev
```
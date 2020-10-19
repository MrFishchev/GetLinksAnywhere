#HOW TO RUN

1. Open GetLinksAnywhere folder in power-shell

Run in Docker:
"docker-compose up -d"

Run as App:
"dotnet run --project GetLinksAnywhere"

2. Open http://localhost:5000/api - you will see the SwaggerUI


===========================================

#HOW TO RUN TESTS

1. Open root folder or project
2. Run "dotnet test" command


===========================================

#HOW TO SEND DATA TO THE API

1. Open http://localhost:5000/api
2. You can use swagger

Or use postman post request with raw text data on http://localhost:5000/api/processing

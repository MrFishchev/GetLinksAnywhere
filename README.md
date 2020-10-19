#HOW TO RUN

Open GetLinksAnywhere folder in power-shell
Run in Docker: "docker-compose up -d"

Run as App: "dotnet run --project GetLinksAnywhere"

Open http://localhost:5000/api - you will see the SwaggerUI
===========================================

#HOW TO RUN TESTS

Open root folder or project
Run "dotnet test" command
===========================================

#HOW TO SEND DATA TO THE API

Open http://localhost:5000/api
You can use swagger
Or use postman post request with raw text data on http://localhost:5000/api/processing

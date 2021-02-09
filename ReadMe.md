### About the task
#### Task requirements are listed below!!

Before I say anything else, I have to say that I have really enjoyed working on this task, because it had a lots of technologies that I have not used before.


* Aurelia is something that I have never heard of before. Also I haven't heard of FluentValidation

* Swashbuckle and serilog are also something new for me. I have heard about them, but this is the first time that I have used them

### About my solution

The task requirements have requested that I put models in the Domain proj, but that would cause cyclic dependencies, because the Domain has to reference the Data proj (so it can store the data) and the Data would have to reference the Domain (so it can now the model of  data to store).

I solved this by setting-up a separate proj (called Models) for models and interfaces. ( I usually like them in a separate proj). Another option was to have an entity model in the Data proj and have mapping logic in some third proj. I think that that was not necessary and it makes thing a bit more complex, without any need for complexity.

### Installation

 1. open .sln with Visual Studio
 2. open command prompt and set prompt path to folder Hahn\Hahn.ApplicationProcess.December2020.Web\ClientApp>
 3. run the "npm install" command
 4. build the app, then  
    (You might get a bunch of TypeScript warnings (or errors) but you can ignore them) 

5. run the app  
    If you get a pop-up in VS that says that build was not successful and asks you to start an earlier build, click No  and run the app again.  
    This will happen on first run and it can happen if you deleted "dist" folder in "wwwroot" folder. You might want to delete that folder, if you have made any changes to front end code. Any changes to front end code will not be registered until you delete the "dist" folder and rebuild the app.
### Usage instruction

App has only one page where you can enter the data about some applicant.  
If you click send you will have an option to stay on that page or open the view page that reads the stored entry from the database.

I have used onchange event to track the state of inputs, so I could disable the buttons, however it behaves as if I have used onblur.  
If you enter valid info in all the inputs, but it does not enable the Send button automatically, just press "tab" or click somewhere else with the mouse.


# THE TASK REQUIREMENTS

### Task:  
We want you to build an Web Solution with RestEndpoints, as well as an Userinterface / Form to apply Data. Therefore you will have to separate the business logic, the data / persistence layer and the web project.  
Provide an Readme.md how to build and start your application.

### SolutionName:

Hahn.ApplicatonProcess.Application

 

### Projects:

 

Hahn.ApplicatonProcess.December2020.Data - .Net 5.0 Class Library

Hahn.ApplicatonProcess.December2020.Web- .Net 5.0 Kestrel Host

Hahn.ApplicatonProcess.December2020.Domain – .Net 5.0 Class Library Containing Business Logic and Models

 
 
### Main model
The Object we want to handle is the class named Applicant with the following properties:

 

ID ( int )

Name ( string )

FamilyName ( string )

Address ( string )

CountryOfOrigin ( string )

EMailAdress ( string )

Age (int)

Hired (bool) – false if not provided.

 



 
### Validation
The object and the properties should be validated by fluentValidation ( nuget ) with the following rules:

 

Name – at least 5 Characters

FamilyName – at least 5 Characters

Adress – at least 10 Characters

CountryOfOrigin – must be a valid Country – therefore ask with an httpclient here https://restcountries.eu/rest/v2/name/aruba?fullText=true – ApiDescription: https://restcountries.eu/#api-endpoints-full-name if the country is found, the country is valid.

 

EmailAdress – must be an valid email (only check for valid syntax *@*.[valid topleveldomain])

Age – must be between 20 and 60

Hired – If provided should not be null

If the object is invalid ( on post and put ) – return  400 and an information what property does not fullyfy the requirements and which requirement is not fullyfied.

 


 ### API
 The Api should have the following actions:

 

- POST for Creating an Object – returning an 201 on successful creation of the object and the url were the object can be called

- GET with id parameter – to ask for an object by id

- PUT – to update the object with the given id

- DELETE – to delete the object with the given id

 The WebApi accepts and returns application/json data.

#### Swagger
Describe the API with swagger therefore use Swashbuckle v5 host the swaggerUI under [localhost]/swagger. Use https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters

 

Do not forget to provide example data in the SwaggerUI, so when someone click on try it out there is already useful valid data in the object that can be posted.                                    
### DB
To save the data use EntityFramework core 5.0 and EntityFramework in memory database.
### Logging
 

Use netcore logging to log each interaction with the API whereever it’s meaningful to do so and also implement this

 

    .AddFilter("Microsoft", LogLevel.Information)

    .AddFilter("System", LogLevel.Error)

 

Write the log to a serilog rolling file sink the name needs to be setable in the applicationsettings.json file. Don’t use serilog in Domain – if you want to log in domain project use https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/

 

## Frontend:

The included form must be an Aurelia ( http://aurelia.io/ ) application which uses the API to Post Data AND Validate all the inputs with the exact same parameters as the API does.

- use Typescript

- use Webpack

- Form can only be sent if the data is valid

- Use Boostrap for the UI

- Use aurelia-validation

- Use a Bootstrap FormRenderer

- invalid fields must be marked with a red border and an explanation why the date is invalid

- the form has two buttons - send and reset.

- clicking the reset button shows an aurelia-dialog - which ask if the user is really sure to reset all the data

- the reset button is only enabled if the user has typed in data -> if all fields are empty the reset button is not enabled.

- when the user has touched a field, but afterwards deleted all entries, the reset button is also not enabled.

- The send button is only active if all required fields are filled out and are valid.

- after sending the data, the aurelia router redirects to a view which confirms the sending.

- if the sending was not successful an error message is shown in a aurelia-dialog describing what went wrong.




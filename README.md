# A simple CRUD WebApp using ASP.NET MVC, ASP.NET WebAPI, and SQL Server

## Task Checklist
- [x] CRUD Operations for Projects (Both WebAPI and MVC)
- [x] CRUD Operations for Devices (Both WebAPI and MVC)
- [x] Device Assignment to Projects
- [ ] Project and Device Overview Window/Dashboard (No dashboard but list view implemented)
- [x] Authentication System
- [x] Chart Visualization for Devices

Demo: https://www.youtube.com/watch?v=R2gYYM6EMDM

## Tech Stack
- C# - .NET Framework 4.7.2 [ASP.Net MVC, WebAPI, Razor, Entity Framework 6)
- SQL Server v13.0.5026.0
- JavaScript/JQuery (Chartjs - for implementing graphs)


## Setup Instructions
> 1. Make sure to have the .NET and SQL versions the same as mentioned above (may cause issues if there is version mismatch)

> 2. Clone the repository on the local machine

`git clone https://github.com/parthpanchal3399/ProjectV.git`

> 3. Open the DBSetup_Script.sql file in SSMS and execute as it is. It will create the database used in the project with dummy data.
> 4. Open the .sln file in Visual Studio
> 5. Navigate to the web.config file under the project *ProjectV_WebAPI*. Under \<connectionStrings>, replace the 'data source' with the server name of your SQL server (can be found in SSMS).

![image](https://github.com/parthpanchal3399/ProjectV/assets/51913848/5cfc5f7d-de6c-4bfe-b749-8cd077709549)

> 6. Right click on *ProjectV_WebAPI* -> Go to Properties -> Web -> Copy the project URL. It should look something like this

![image](https://github.com/parthpanchal3399/ProjectV/assets/51913848/499452cd-16e3-4fb5-aee1-4115320d5b60)

> 7. Navigate to the web.config file under the project *ProjectV_MVC*. Under \<appSettings>, add the following and paste the copied URL into the value attribute

`<add key="WebApiEndpoint" value="PASTE_HERE"/>`

> 8. Build the entire solution. Make sure you don't get any errors. If you do, please contact me.
> 9. Before running, set both projects *ProjectV_WebAPI* and *ProjectV_MVC* as startup by right-clicking on solution -> Properties -> Common Properties -> Select "Start" as action for both projects.



## Using the application
### 1. WebAPI using Postman:
> Prerequisite: All API endpoints need the user to be authenticated. You can use the following credentials:
> 
> Username: parth | Password: pass123
> 
> Username: omkar | Password: pass123
> 
> Add these credentials in the Authorization tab of Postman (select Basic Authentication from the dropdown)


| Method | URL | Data | Description |
| ----------- | ----------- | ----------- | ----------- |
| GET | WebApiEndpoint/api/Projects/ | | Retrieve the list of all Projects
| GET | WebApiEndpoint/api/Projects/id | | Retrieve Project with ProjectID = id
| PUT | WebApiEndpoint/api/Projects/id | Project Object | Update Project with ProjectID = id
| POST | WebApiEndpoint/api/Projects/ | Project Object | Insert new Project
| DELETE | WebApiEndpoint/api/Projects/id | | Delete Project with ProjectID = id
| GET | WebApiEndpoint/api/Devices/ | | Retrieve the list of all Devices
| GET | WebApiEndpoint/api/Devices/id | | Retrieve Device with DeviceID = id
| PUT | WebApiEndpoint/api/Devices/id | Device Object | Update Device with DeviceID = id
| POST | WebApiEndpoint/api/Devices/ | Device Object | Insert new Device
| DELETE | WebApiEndpoint/api/Devices/id | | Delete Device with DeviceID = id
| POST | WebApiEndpoint/api/Devices/AddValue | ValueRec Object | Insert new Value for a Device
| DELETE | WebApiEndpoint/api/Devices/DeleteValue/id | | Delete Value with ValueID = id


> NOTE: Data to be passed as x-www-form-urlencoded


### 2. WebApp
> Login with the username and password as mentioned above. Navigate to different pages and test the functionalities. 

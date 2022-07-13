# Canine Cloud - Stepping to the cloud

## A. Scenario

It finally happened. After years of negligence the pre-Y2K server in the basement has died. With it, the website used by the neighbourhoods local dog shelter, 'Stockholm's Canine Cloud', is now gone.

The pictures, the website, all of it is now lost. But we can rebuild it. We have the technology. We can make it better than it was. Better, stronger, faster. We can remake it, in the cloud!

## B. What you will be working on

Today you will be creating the start of Canine Cloud, the replacement to the old static HTML only website previously used by the neighbouring dog shelter. 

We will work with this application during the week and refine it little by little. With the power of the cloud and quick turnarounds and deployments, we will use this domain as our learning ground.

The goal of today is to create a local WebAPI application with connection to Azure Tables and Azure Blob storage.

## C. Setup

Ensure that you have created an Azure account with a free subscription. At least one per mob. You will need to be able to set up storage like mentioned in the slides from this morning.

A word of caution! We are creating resources that are publicly available on the internet this time and with this, it's important to not make sensitive information such as connection strings and passwords public. One way of keeping things secure during development is to use ``dotnet user-secrets``  (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets).

To set this up, run the following commands:
```
dotnet user-secrets init
dotnet user-secrets set "KeyName" "<YourSecretKey>"
```

## D. Lab instructions

- Create a WebAPI application, to keep things simple
- Directly after you have created the empty application, move it to the cloud
- Create an Azure Table, this will be the main database used to store all the data about dogs.
- Connect the WebAPI it to the newly created Azure Table and setup a Dog model with fitting fields (name, birth-year etc.). The dog shelter is trusting you with the logic on what fields should be added!
- Create a blob container for you photos and write the boilerplate code needed to upload the photos to it.
  - You might find this [tutorial helpful](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images)
- Setup a controller so that you can GET, POST and DELETE dogs from the database.

Good luck and have fun!

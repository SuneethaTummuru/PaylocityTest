# Postman Collection for API Testing

## Overview
This Postman collection contains a set of API tests for validating the endpoints of employees in our Paylocity application. The tests include checks for status codes, response structure, and specific data validation of employees.

## Prerequisites
Before you can run the Postman collection, ensure you have the following installed:
- [Postman](https://www.postman.com/downloads/)

## Importing the Collection
1. Open Postman.
2. Click on the **Import** button in the top left corner.
3. Select the **Import File** tab.
4. Choose the JSON file of the Postman collection you want to import.
5. Click **Import**.

## Importing the Environment
1. Click on the **Environments** icon in the left panel.
2. Click on the **Import** button.
3. Choose the JSON file of the environment you want to import.
4. Click **Import**.

## Running the Collection
1. Select the imported collection from the left sidebar.
2. Click on the **Run** button to open the Collection Runner.
3. Choose the environment (if any) from the drop-down.
4. Click on **Run [Collection Name]**.
5. Username, password, token are defined as environment variables

## Tests Included in the Collection
1.Test suit and tear down has login and logout verifications
2.Add and modify the employee details on the benefit dashboard
### Status Code Tests
- **200 OK**: Ensures the endpoint returns a 200 status code.
- **Valid Response**: Checks if the response is valid, contains a body, and is in JSON format.

### Data Validation Tests
- **First value's firstName is 'Dawn'**: Validates that the `firstName` of the first object in the response is `Dawn`.
- **First value's id is '840b0d2a-14e5-4d6e-9786-61e98809bffa'**: Validates that the `id` of the first object in the response is `840b0d2a-14e5-4d6e-9786-61e98809bffa`.
- **One of the IDs is '840b0d2a-14e5-4d6e-9786-61e98809bffa'**: Checks if any object in the response array has an `id` of `840b0d2a-14e5-4d6e-9786-61e98809bffa`.

## Running the Tests from Command Line
You can also run the tests using Newman, the command-line collection runner for Postman.

1. Install Newman:
   ```bash
   npm install -g newman

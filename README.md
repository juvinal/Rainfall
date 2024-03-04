# Rainfall API

Welcome to the README documentation for the Rainfall API. This project provides a simple API endpoint that fetches rainfall readings from the Environment Agency Rainfall API.

## Table of Contents

1. [Getting Started](#getting-started)
   - [Clone Repository](#clone-repository)
   - [Run the Project](#run-the-project)
2. [API Endpoint](#api-endpoint)
   - [Endpoint URL](#endpoint-url)
   - [Request Parameters](#request-parameters)
   - [Response Codes](#response-codes)
3. [Validation](#validation)
   - [Fluent Validation](#fluent-validation)
   - [Unit Tests](#unit-tests)
4. [Additional Information](#additional-information)
   - [External API](#external-api)
   - [MediatR Pattern](#mediatr-pattern)
   - [Project Structure](#project-structure)

## Getting Started

### Clone Repository

To get started with the project, clone the repository using the following command:

```bash
git clone https://github.com/juvinal/Rainfall.git
```

### Run the Project

Once cloned, navigate to the project directory and run it:

```bash
cd /Rainfall.Api
dotnet run
```

The API will be available at http://localhost:3000 by default.

## API Endpoint

### Endpoint URL
The API exposes a single endpoint to fetch rainfall readings:
- **URL**: `/id/{stationId}/readings?count={count}`
### Request Parameters
- `stationId (required)`: The unique identifier of the station.
- `count (optional, default: 10)`: The number of readings to retrieve.
### Response Codes
- `200 OK`: The station is found, and readings are returned.

- `400 Bad Request`: Invalid request (e.g., invalid parameters).

- `404 Not Found`: Station not found or has no readings.

- `500 Internal Server Error`: An internal server error occurred.

## Validation

### Fluent Validation

Fluent Validation is used to validate input parameters. Validation rules include:

- `stationId` must be a valid string.
- `count` must be a positive integer.

## Unit Tests

Unit tests are available to validate the functionality of the validation and API handler. Run the tests using:

```bash
dotnet test
```

## Additional Information

### External API

This project interacts with the [Environment Agency Rainfall API](https://environment.data.gov.uk/flood-monitoring/id/stations/{}/readings?_sorted&_limit=10).

### MediatR Pattern

The project follows the MediatR pattern for better separation of concerns and maintainability.

### Project Structure

- **Controllers:** : Contains the API controller.

- **Handlers:** : Implements MediatR handlers.

- **Validators:** : Fluent Validation rules.

- **Tests:** : Unit tests.

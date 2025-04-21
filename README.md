## Description

This Tax Calculator application helps users calculate the amount of tax they owe based on their annual income and postal code. 
Each postal code is linked to a specific tax calculation method (**Flat Value** or **Flat Rate**), 
and the tax is calculated accordingly using predefined rules stored in a SQLite database.

## Features

The application supports two types of tax calculators, 
each associated with specific postal codes and configured via database seed data. 

**Flat Value**

Postal Codes: A100, A101

*Logic:
If Annual Income ≥ 200,000:
	Tax is a fixed flat amount (e.g., $10,000)

If Annual Income < 200,000:
	Tax is calculated as a percentage of income (e.g., 5%)
	
*Example:

Income = 250,000 ➝ Tax = 10,000 (flat value)

Income = 150,000 ➝ Tax = 7,500 (5% of income)

**Flat Rate**

Postal Codes: 7000, 7001, 7002,

*Logic:

The tax is a fixed percentage applied to the full annual income, regardless of amount.

*Example:

Income = 80,000 ➝ Tax = 14,000 (17.5% of income)

## How to use

Run the Application
Make sure both the API project and the Web project are running.

**Enter Tax Details
On the main page:

 - Input your annual income
 - Select a postal code (e.g., A100, 7000)
 - Click Calculate

**View Results
 - The calculated tax will be displayed instantly.
 - The result is stored in the system along with the input and timestamp.

**View History
 - Navigate to the History page to see a paginated list of past tax calculations.
 - The table supports server-side pagination to handle large datasets efficiently.

## Key Notes

All database logic is handled using Entity Framework Core.
EF will apply migrations and populate:

*Calculator types

*Calculator settings (thresholds, rates)

*Postal code mappings

**The application will not work unless both the API and Web project are running**

## Project Structure

- /Insomniac.Calculator.API       → Web API project
- /Insomniac.Calculator.Web       → MVC Web frontend
- /Insomniac.Calculator.Data      → EF Core DbContext
- /Insomniac.Calculator.Service   → Calculator logic, Factory, Interfaces
- /Insomniac.Calculator.Tests     → Unit tests

## Technologies Used

- ASP.NET Core MVC (Razor)
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- MediatR
- NUnit




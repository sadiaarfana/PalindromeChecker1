# PalindromeChecker
**************************************************************************************************************************************
**PALINDROME CHECKER **

**************************************************************************************************************************************
This project is built in .NET 6 Web App that checks if a user input string is a palindrome.
The solution includes:
• A front-end Web Application project named Palindrome.Web includes the front end UI using HTML,CSS, bootstrap library.
• The backend Web API project named Palindrome.API using dependency injection and Entity Framework.
• The solution used EF In-Memory Database to keep the persistent list for Palindrome. As long as the Web API running, it will 
  keep all successful palindrome strings in the list and show the list in Web Application "Palindrome List" page. 
**************************************************************************************************************************************
This application is built to consider the three criterias as mentioned above. There are lot of room for improvement like 
• Add authentication process in API
• Add proper logging and audit trail 
• Add proper relational database in SQL Server if required
**************************************************************************************************************************************
Please run Palindrome.API in local. This will use localhost as https://localhost:7133/
Then run Palindrome.Web in localhost. 
**************************************************************************************************************************************

 

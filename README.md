# TelepathyLabsTest
Sourcecode for both question 1 and question 2 with unit tests.

Prerequisites
-------------
-You need to have Visual Studio(Microsoft Visual Studio Professional 2019 or later) to Open / Debug / Run Unit tests

Notes
-----
- This source contains source code for Backend Engineer test_Solutions Team Test, Question 1 (Software Design and Implementation) and Question 2 (Algorithms)
- There is a Common API developed (.net core web api) for both questions. To open in Visual Studio, click CommonApi.sln
- To Open the source code for question 1, click Hotel.sln
- To Open the source code for question 1, click Calculator.sln

How to Run Projects
-------------------
- First run the common API. Open CommonApi.sln in Visual Studio and press 'F5' (to run in debug mode). If service is successfully started, you should see 'Service Started!' in your default browser.
- Run Hotel.sln in Visual Studio. This will open WPF based client UI for question 1 solution.
- Run Calculator.sln in Visual Studio. This will open WPF based client UI for question 2 solution.

How to Run Unit Tests
---------------------
- Keep Common API up and running. Integration Tests will connect to api while running tests.
- To run integration tests for Calculator, run test project 'XUnitTestsCalculator' (this project can be found under Calculator solution)
- To run integration tests for Hotel, run test project 'XUnitTestsHotel' (this project can be found under Hotel solution)
- To run unit tests for services run run test project 'XUnitTestServices' (this project can be found under Common API solution)
- How to run test projects -> Right click on the project under solution explorer and click 'Run Tests'

- K Dhanushka Gayanath -

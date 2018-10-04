# WorkforceManagement

Computer Controller uses the Employee and EmployeeComputer tables to run correctly. This controller allows you to Get a list of all computers. You are able to view a list of computers
by their purchase date, manufacturer, and make using the Index View. You can click on the make of the computer to view details about the computer using the details view. You are able to
delete a computer that is Not associated to an employee using the deleteConfirm view.

**Purpose: Get a list of computers, show details on the clicked computer, and delete a computer that is not registered to an employee

**How it fits in context of the project: Allows the controller to access required information and other controllers that might need this information

**Specific Feature affected: Delete Method will only let you delete an object that has No association to a Product.

**How to test (Be thorough!):

1. Checkout to my branch CompController

2. type start WorkforceManagement.sln

3. This will open the file inside Visual Studio and open ComputerController

4. Press IIS Express play button to start up the server, a terminal window will open inside the browser and let you know that it is listening on localhost:5000.

5. Open your browser and type in the url localhost:5000.

6. You will need to press Accept in the navbar.

7. Click on the Computer tab inside navbar.

8. You can click on the Make of a computer to open the details about the specified computer. (You will be able to see Purchase Date, Manufacturer, Make, Decommission Date, and Condition)

9. From the Computer homepage, you are also able to delete a computer as long as it has not been assigned to an employee.
   If the computer has not been assigned to an employee, a delete confirm window will pop up asking you to confirm that you want to delete.
   If the computer has been assigned to an employee, you will be redirected to an error page showing "You can not delete a computer that belongs to an employee."

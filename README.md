
System Requirement and Time Plan Documentation 



Problem Definition: 

The nursery needs a computerized system to manage the profile of the children their parents’ information, also the staff members and their assigned classes.



System Scenario: 

Users will login to the system using his/her username and 
password, then if the user is a staff, a new interface
with multiple tabs appears to him to choose one of the system 
functions like: Add the details of the day to each child's 
profile .
	
Interfaces appear if the user is an admin or a child's parent
If the user is a child's parent, he could view his child’s information.
 
Finally if the user is an admin, he could add or remove (room, staff member and child), move child from room to another room and approve requests.



System Objectives:

Facilitates the interaction between the administrators and the parents.
Offers an easy way for the parents to add features for their children.
Helps collecting feedback from parents and staff members to help the administrator improve the nursery’s services.
Helps the parents monitor their children’s behaviour and overall performance at the nursery.



System Users: 

Administrators
Parents
Staff



System Functions: 

Admins: 	
		•	Manage users’ information
		•	Manage classrooms
		•	Allocate a member of staff to a room
		•	Move staff around when needed (from a room to another)
		•	View parents’ feedback
		•	Accept/Reject registration requests (for both parents & staff)



Staff Members:  
	•	View their assigned room
	•	View profile of children in their assigned room
	•	Add details (feedback) of the day to each child’s profile
    
    
    
Parents: 
	•	View their child’s profile
•	Add or remove features introduced by the nursery 
	•	Pay fees
	•	Give feedback



User and System Requirements: 

Parent shall be able to register.
The system shall display a screen with two options either to sign in or sign up.
Parent shall be able to select to sign up.
The System shall display a screen to choose whether to view a Staff’s application or a Parent’s application form.
Parent shall be able to fill out the form and send the application to the system administrator to be reviewed.

User shall be able to register for a staff member position.
Following with segment 1.3, the user shall be able to select to sign up as a staff member.
The System shall display a staff member application form for the user to fill out.
The user shall be able to send the application to the system administrator to be reviewed.

The administrator shall be able to review all applications and requests.
After the system administrator is granted access to the system, the admin can select the requests tab to review all pending requests.
Requests are separated into two groups.
Parents’ applications
Staff’s applications
When viewing a parent’s application, the admin shall be able to accept, decline or keep the application pending.
Accepting a request adds the parent’s information to the database and creates an account on the system for the parent.
Declining a request deletes all the details regarding specified person.
Leaving the form without acting upon it keeps the application pending.
When reviewing a staff application, the admin shall be able to accept, decline or keep the application pending.
Accepting a request requires assignment to a room and adds the staff member’s information to the database and creates an account on the system for the staff member.
Declining a request deletes all the details regarding specified person.

Leaving the form without acting upon it keeps the application pending.
Parent shall be able to add child to his account.
The system shall view upon login a button that allows the parent to add a child to their profile.
The system shall view a child application form.
The parent shall be able to fill out the child application form and send it to the system administrator to be reviewed.


Parent shall be able to view their children’s information.
The system shall view a tab with the parent’s children.
The parent shall be able to choose their child’s profile to view.
The system shall display all the saved information about the parent’s child on the database.
The parent shall be able to add or remove features introduced by the nursery in their child’s profile.
On the child’s profile form, the parent can choose to manage the features.
The system shall display all available features for the parent to choose from.
The parent shall be able to check or uncheck features.
Any updates shall be sent to the system administrator to be reviewed.
Parent shall be able to pay the nursery fees.
The parent shall be able to choose to pay the fees from the system
The system shall check the parent’s credit card balance.
After verifying that the needed amount is available the system shall withdraw the amount from the balance and display that the withdrawal is successful.
If the needed amount is not available, the system shall display an error message notifying the parent that there’s not enough credit in their balance.
Parent shall be able to send feedback to the system administrator.
The parent shall be able to submit feedback.
The system shall send the feedback to the system administrator to be reviewed.
The system administrator shall view and reply to the feedback.
The staff member shall be able to view their assigned room
Upon login the system shall display the assigned room and all the children within this room.
The staff member shall be able to view any child’s profile and add details of the day.
The staff member shall be able to select any child from the assigned room.
The system shall display all the saved information about the child on the database.
The staff member shall be able to choose to add details of the current day to the child’s profile.
The system shall save the day’s details entry in the system database with the date and time.

Database Schema:



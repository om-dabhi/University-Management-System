# 🎓 University Management System

Welcome to the **University Management System** repository! This project aims to streamline university operations through a comprehensive management platform.

## ✨ Key Features

- **🔄 CRUD Operations**: Effortlessly manage **Courses**, **Branches**, **Students**, and **Faculty** with Create, Read, Update, and Delete functionalities.
- **🔐 User Authentication**: Secure login system with role-based access for:
  - 🧑‍🏫 Administrators
  - 👩‍🏫 Faculty
  - 👨‍🎓 Students
- **📊 Admin Dashboard**: An intuitive dashboard for administrators to view and manage real-time counts and statuses of courses, branches, students, and faculty.
- **🔒 Role-Based Access Control**: Implement on-roll and off-roll functionalities for students and faculty, ensuring proper access and data management.

## 🛠️ Technologies Used

- **Backend**: .NET Core
- **Database**: SQL
- **Frontend**: HTML/CSS, JavaScript

## 🌟 Impact

- **Efficiency**: Automated administrative tasks, enhancing operational efficiency.
- **Data Management**: Improved data management and accessibility for university staff and students.
- **Insights**: Provided real-time insights into university metrics through the admin dashboard.

## 🚀 Getting Started

1. **Clone the repository**:
    ```bash
    git clone https://github.com/om-dabhi/university-management-system.git
    ```
2. **Navigate to the project directory**:
    ```bash
    cd university-management-system
    ```
4. **Set up the database**:
   - Navigate to the `/sql` directory and execute the SQL script to create the necessary database schema.
    ```bash
    cd SQL
    sqlcmd -S your_server_name -i UMSDBScript.sql
    ```

---

Feel free to explore the repository, contribute, and open issues or pull requests if you have any suggestions or improvements!

Happy coding! 💻🚀

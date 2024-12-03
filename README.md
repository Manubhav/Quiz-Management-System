# Quiz Management System

## Overview

The **Quiz Management System** is a desktop application designed to manage and facilitate online quizzes. It allows teachers to create quizzes, assign them to students, track progress, upload study materials based on subjects and evaluate results. This system is built using C# and integrates with Firebase to store and retrieve data, making it scalable and flexible for future enhancements.

## Features

- **Teacher Panel**: Allows administrators to add new subjects, create quizzes, and assign them to students.
- **Student Panel**: Allows students to take quizzes, view results, and track their progress.
- **Question Management**: Admins can create, update, and delete questions for each quiz.
- **Firebase Integration**: Data is stored and fetched from Firebase for real-time updates.
- **Authentication**: Secure login for both students and administrators.
- **Result Analysis**: Students can view their scores and performance after completing quizzes.

## Tech Stack

- **Backend**: C#
- **Database**: Firebase (NoSQL)
- **Libraries**:
  - FirebaseAdmin
  - ExcelDataReader
  - Newtonsoft.Json
  - NUnit for testing

## Installation

### Prerequisites

- .NET 6.0 or later
- Visual Studio or another C# IDE
- Firebase Admin SDK (Firebase project configuration)

### Steps

1. Clone this repository to your local machine:
    ```bash
    git clone https://github.com/yourusername/quiz-management-system.git
    ```
2. Open the solution file (`Quiz Management System.sln`) in Visual Studio.
3. Install the required NuGet packages:
    - FirebaseAdmin
    - ExcelDataReader
    - Newtonsoft.Json
4. Configure Firebase credentials:
    - Create a Firebase project at [Firebase Console](https://console.firebase.google.com/)
    - Download the service account JSON file and replace the existing one in the `Resources` folder.
5. Build and run the application:
    - Press `F5` in Visual Studio to start the application.

## Usage

### Admin Features

- **Login**: Admins can log in using their credentials.
- **Create New Subject**: Add new subjects for quizzes.
- **Create New Question**: Add questions for a quiz under specific subjects.
- **View Results**: Admins can view student scores and performance analytics.

### Student Features

- **Login**: Students can log in using their credentials.
- **Take Quizzes**: Students can attempt quizzes assigned to them.
- **View Results**: After completing a quiz, students can view their scores and answers.

## File Structure

- `Quiz Management System.sln`: The solution file for Visual Studio.
- `Program.cs`: Entry point of the application.
- `FirebaseInit.cs`: Firebase configuration and initialization.
- `Forms/`: Contains the UI forms for the application.
- `Models/`: Contains C# model classes for Quiz, Question, Student, etc.
- `Interfaces/`: Contains interfaces for question and subject management.
- `Tests/`: Contains unit tests for different parts of the application.
- `Resources/`: Stores Firebase credentials and images used in the app.

## Contributing

We welcome contributions to improve the **Quiz Management System**! If you find a bug or have an idea for a new feature, feel free to create an issue or submit a pull request. Please ensure that any new code is well-tested and documented.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- Firebase for the real-time database.
- NUnit for unit testing framework.
- Visual Studio for the IDE.

## Contact

For any inquiries or support, please contact:  
**Email**: manubhavmehta@gmail.com  
**GitHub**: [https://github.com/Manubhav](https://github.com/Manubhav)

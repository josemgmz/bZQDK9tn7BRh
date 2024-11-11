# Welcome to the Card Matching Game Project

Welcome to the Card Matching Game Project! This project is a card matching game using poker cards, developed with several interesting features and architectural patterns to ensure a robust and maintainable codebase.

## Key Features

### Model-View-Controller (MVC) Pattern
The project follows a custom-developed Model-View-Controller (MVC) architectural pattern, which helps in separating the concerns of the application:
- **Model**: Manages the data and business logic.
- **View**: Handles the presentation layer and user interface (MonoBehaviour).
- **Controller**: Acts as an intermediary between the Model and View, handling user input and updating the Model and View accordingly.

### Dependency Injection
Dependency Injection is used to manage the services within the project, promoting loose coupling and enhancing testability. This allows for easy swapping of implementations and better management of dependencies.

### Event Bus System
An Event Bus system is implemented to facilitate communication between managers and game objects. This decouples the components, making the system more modular and easier to maintain.

### End-to-End Testing
The project includes comprehensive end-to-end tests to ensure the mechanics of the game work as expected. This includes:
- **Game Mechanics Tests**: Verifying the core mechanics of the card matching game.
- **Level Tests**: Ensuring the functionality and correctness of all 10 levels of the game.

### Addressables
The project utilizes Unity's Addressables system for efficient asset management. This allows for dynamic loading and unloading of assets, improving performance and memory usage.

## Getting Started

To get started with the project, follow the [Installation Instructions](articles/installation.md).

## Additional Resources

- [API Documentation](api/index.md)
- [License](articles/license.md)

We hope you enjoy working on this project and find the architecture and features beneficial for your development process. Happy coding!
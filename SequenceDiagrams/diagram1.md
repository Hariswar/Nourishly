```mermaid
sequenceDiagram
    actor Students/Staff
   
    box lightyellow Nourishly App
    participant AuthenticationSystem
    participant DiningSystem
    participant Database
    end


    %% Sign in
    Students/Staff->>AuthenticationSystem: Sign In
    activate AuthenticationSystem
    AuthenticationSystem-->>Students/Staff: Sign-In Successful
    deactivate AuthenticationSystem


    %% Get Dining Data
    activate DiningSystem
    AuthenticationSystem->>DiningSystem: Pass Student Data
    activate Database
    DiningSystem->>Database: Retrieve Student Data
    Database-->>DiningSystem: Return Student Data
    deactivate Database
    DiningSystem->>DiningSystem: Retrieve Available Dining Options
    DiningSystem-->>Students/Staff: Display Menu
    deactivate DiningSystem


    %% Apply Dining Filter + Display
    Students/Staff->>DiningSystem: Apply Dietary Filters (optional)
    activate DiningSystem
    DiningSystem->>DiningSystem: Filter Dining Options
    DiningSystem-->>Students/Staff: Display Filtered Menu
    deactivate DiningSystem


    %% Favorite Meals
    Students/Staff->>DiningSystem: Request to Favorite Meal
    activate DiningSystem
    DiningSystem->>DiningSystem: Update Meal as Favorite
    DiningSystem-->>Students/Staff: Confirm Meal Favorited
    deactivate DiningSystem
```

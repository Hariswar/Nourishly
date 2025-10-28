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


    %% Get User Data
    activate DiningSystem
    AuthenticationSystem->>DiningSystem: Pass Student Data
    activate Database
    DiningSystem->>Database: Retrieve Student Data
    Database-->>DiningSystem: Return Student Data
    deactivate Database
    DiningSystem->>DiningSystem: Retrieve Available Dining Options
    DiningSystem-->>Students/Staff: Display Menu
    deactivate DiningSystem


    %% Apply Diet Filters
    Students/Staff->>DiningSystem: Apply Dietary Filters
    activate DiningSystem
    DiningSystem->>DiningSystem: Filter Dining Options
    alt No Matches Found
        DiningSystem-->>Students/Staff: Display "No Items Found"
        DiningSystem-->>Students/Staff: Suggest Relaxing Filters / Similar Options
    else Matches Found
        DiningSystem-->>Students/Staff: Display Filtered Menu
    end
    deactivate DiningSystem
```

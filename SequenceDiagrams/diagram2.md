## Sequence Diagram #2: Generating / Displaying Nutrition Report
#### Description: This diagram shows the flow of a user generating their weekly nutrition report, and receiving suggestions to improve their diet.

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


    %% Get Nutrition Data
    activate DiningSystem
    AuthenticationSystem->>DiningSystem: Pass Student Data
    activate Database
    DiningSystem->>Database: Retrieve Student Nutrition Data
    Database-->>DiningSystem: Return Student Nutrition Data
    deactivate Database


    %% Display Nutrition Report
    DiningSystem->>DiningSystem: Compute Weekly Nutrition Metrics
    DiningSystem-->>Students/Staff: Display Weekly Nutrition Report
    deactivate DiningSystem


    %% Display Nutrition Suggestions
    Students/Staff->>DiningSystem: Request Diet Improvement Suggestions
    activate DiningSystem
    DiningSystem->>DiningSystem: Identify Macronutrient Gaps
    DiningSystem->>DiningSystem: Sort Meals High in Missing Nutrients
    DiningSystem-->>Students/Staff: Display Suggested Meals
    deactivate DiningSystem


    %% Favorite Meals
    Students/Staff->>DiningSystem: Request to Favorite Meal
    activate DiningSystem
    DiningSystem->>DiningSystem: Update Favorite Meal List
    DiningSystem-->>Students/Staff: Confirm Meal Favorited
    deactivate DiningSystem
```

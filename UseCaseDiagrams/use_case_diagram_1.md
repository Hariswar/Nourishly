 ```mermaid
 graph TD
  %% Actors
  subgraph actors
      A[College Students]
      B[Student Athletes]
      C[College Faculty]
  end


  %% Use Cases
  subgraph useCases
      U1((View Dining Menus))
      U2((Display Nutrition Details))
      U3((Filter for Dietary))
      U4((Sustainable Dining))
      U5((Favorite Menus))
      U6((Notifications for New meals))
      U7((Reorder Favorite Meals))


  end


  %% Actor associations
  A --> U1
  B --> U1
  C --> U1


  %% Use case relationships
  U1 -->|<<include>>| U2
  U1 -->|<<include>>| U3
  U1 -->|<<include>>| U4
  U1 -->|<<extend>>| U5
  U1 -->|<<extend>>| U6
  U1 -->|<<extend>>| U7

```
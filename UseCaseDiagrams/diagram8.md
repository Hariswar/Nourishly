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
      U1((Favorite Meals))
      U2((Filter For Dietary Needs))
      U3((Sustainable Dining))
      U4((Notifications for New Meals))
      U5((Reorder Favorite Meals))


  end


  %% Actor associations
  A --> U1
  B --> U1
  C --> U1


  %% Use case relationships
  U1 -->|<<include>>| U2
  U1 -->|<<include>>| U3
  U1 -->|<<extend>>| U4
  U1 -->|<<extend>>| U5
```
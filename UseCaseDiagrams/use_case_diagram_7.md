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
      U1((Sustainable Dining))
      U2((Display Nutrition Details))
      U3((Filter For Dietary Needs))
      U4((Sold Out Items))
      U5((Notifications for New Meals))
      U6((Favorite Meals))
      U7((Automatic Discount for Staff Members))


  end


  %% Actor associations
  A --> U1
  B --> U1
  C --> U1


  %% Use case relationships
  U1 -->|<<include>>| U2
  U1 -->|<<include>>| U3
  U1 -->|<<include>>| U4
  U1 -->|<<include>>| U5
  U1 -->|<<extend>>| U6
  U1 -->|<<extend>>| U7
```
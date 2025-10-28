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
      U3((Filter for Dietary Needs))
      U4((Suggestions for High Protein Meals))
      U5((Online Order System))


  end


  %% Actor associations
  A --> U3
  B --> U3
  C --> U3


  %% Use case relationships
  U3 -->|<<include>>| U1
  U3 -->|<<include>>| U2
  U3 -->|<<extend>>| U4
  U3 -->|<<extend>>| U5
  ```
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
      U4((Suggestions for High-Protein Meals))


  end


  %% Actor associations
  A --> U2
  B --> U2
  C --> U2


  %% Use case relationships
  U2 -->|<<include>>| U1
  U2 -->|<<include>>| U3
  U2 -->|<<extend>>| U4
  ```
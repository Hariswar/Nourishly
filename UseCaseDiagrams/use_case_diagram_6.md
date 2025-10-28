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
      U1((Tracking Meal Plan Balance ))
      U2((Automatic Discount for Staff Members))
      U3((Online Order System))
      U4((Feedback, Reviews and Ratings))


  end


  %% Actor associations
  A --> U1
  B --> U1
  C --> U1


  %% Use case relationships
  U1 -->|<<include>>| U3
  U1 -->|<<include>>| U4
  U1 -->|<<extend>>| U2
```